namespace WinKeyHandler
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    internal class WinKeyPressedEventArgs : EventArgs
    {
        public VK Key { get; private set; }

        public WinKeyPressedEventArgs(VK key)
        {
            Key = key;
        }
    }

    internal static class Keyboard
    {
        const int HC_ACTION = 0;
        static IntPtr hHook = IntPtr.Zero;
        static HookProc hookProc;

        public static event EventHandler WinKeyPressed = delegate { };

        #region P/Invoke declarations

        delegate IntPtr HookProc(int nCode, IntPtr wParam, [In] IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr SetWindowsHookEx(WH idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(VK vKey);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        #endregion P/Invoke declarations

        static Keyboard()
        {
            // Keep a reference to prevent 
            // delegate from garbage collection
            hookProc = OnKeyPressed;
        }

        public static void Hook()
        {
            using (var process = Process.GetCurrentProcess())
            {
                using (var module = process.MainModule)
                {
                    IntPtr hMod = GetModuleHandle(module.ModuleName);
                    hHook = SetWindowsHookEx(WH.KEYBOARD_LL, hookProc, hMod, 0);
                }
            }
        }

        public static void Unhook()
        {
            if (hHook != IntPtr.Zero)
            {
                UnhookWindowsHookEx(hHook);

                hHook = IntPtr.Zero;
            }
        }

        private static IntPtr OnKeyPressed(int nCode, IntPtr wParam, IntPtr lParam)
        {
            var keyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));

            if (nCode == HC_ACTION && (WM)wParam == WM.KEYDOWN)
            {
                if (IsWinKey())
                {
                    WinKeyPressed(null, new WinKeyPressedEventArgs(keyInfo.VkCode));
                }

                // Ignore the key press
                //return (IntPtr)1;
            }

            return CallNextHookEx(hHook, nCode, wParam, lParam);
        }

        private static bool IsWinKey()
        {            
            return GetAsyncKeyState(VK.LWIN) < 0 ||
                   GetAsyncKeyState(VK.RWIN) < 0;
        }

    }
}
