namespace WinKeyHandler
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    struct KBDLLHOOKSTRUCT
    {
        public readonly VK VkCode;
        public readonly uint ScanCode;
        public readonly LLKHF Flags;
        public readonly uint Time;
        public readonly UIntPtr Extra;
    }

}
