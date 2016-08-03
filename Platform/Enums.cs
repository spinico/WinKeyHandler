namespace WinKeyHandler
{
    using System;

    [Flags()]
    enum WM : Int32
    {
        KEYDOWN = 0x0100,
    }

    enum WH : Int32 // WINDOWHOOK
    {
        JOURNALRECORD   = 0,
        JOURNALPLAYBACK = 1,
        KEYBOARD        = 2,
        GETMESSAGE      = 3,
        CALLWNDPROC     = 4,
        CBT             = 5,
        SYSMSGFILTER    = 6,
        MOUSE           = 7,
        HARDWARE        = 8,
        DEBUG           = 9,
        SHELL           = 10,
        FOREGROUNDIDLE  = 11,
        CALLWNDPROCRET  = 12,
        KEYBOARD_LL     = 13,
        MOUSE_LL        = 14
    }

    [Flags]
    enum LLKHF : UInt32 // KBDLLHOOKSTRUCTFlags
    {
        EXTENDED = 0x01,
        INJECTED = 0x10,
        ALTDOWN  = 0x20,
        UP       = 0x80,
    }

    enum VK : Int32 // Virtual-Key Codes
    {
        SPACE = 0x20,
        HOME  = 0x24,
        LEFT  = 0x25,
        UP    = 0x26,
        RIGHT = 0x27,
        DOWN  = 0x28,
        LWIN  = 0x5B, // Left Windows key (Microsoft Natural keyboard) 
        RWIN  = 0x5C, // Right Windows key (Natural keyboard)
        KEY_D = 0x44,
        KEY_M = 0x4D
    }
}
