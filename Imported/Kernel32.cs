using System;
using System.Runtime.InteropServices;

namespace csgop.Imported {
    class Kernel32 {

        private static readonly Kernel32 instance = new Kernel32();

        private Kernel32() { }

        internal static Kernel32 Instance {
            get {
                return instance;
            }
        }

        public struct RECT {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        private class Static {
            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool AllocConsole();
            [DllImport("kernel32.dll")]
            public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
            [DllImport("kernel32.dll")]
            public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, uint nSize, out uint lpNumberOfBytesRead);
            [DllImport("kernel32.dll")]
            public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, uint nSize, out uint lpNumberOfBytesRead);
            [DllImport("user32.dll")]
            public static extern bool keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
            [DllImport("user32.dll")]
            public static extern bool mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInfo);
            [DllImport("user32.dll")]
            public static extern bool GetAsyncKeyState(int key);
            [DllImport("user32.dll")]
            public static extern bool SetCursorPos(int x, int y);
            [DllImport("user32.dll")]
            public static extern bool GetClientRect(IntPtr hwnd, out RECT rect);
            [DllImport("user32.dll")]
            public static extern IntPtr GetForegroundWindow();
            [DllImport("User32.dll")]
            public static extern bool SetForegroundWindow(IntPtr hwnd);
            [DllImport("dwmapi.dll")]
            public static extern bool DwmExtendFrameIntoClientArea(IntPtr hwnd, ref int[] margins);
        }

        public bool AllocConsole() {
            return Static.AllocConsole();
        }
        public IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId) {
            return Static.OpenProcess(dwDesiredAccess, bInheritHandle, dwProcessId);
        }
        public bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, uint nSize, out uint lpNumberOfBytesRead) {
            return Static.ReadProcessMemory(hProcess, lpBaseAddress, lpBuffer, nSize, out lpNumberOfBytesRead);
        }
        public bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, uint nSize, out uint lpNumberOfBytesRead) {
            return Static.WriteProcessMemory(hProcess, lpBaseAddress, lpBuffer, nSize, out lpNumberOfBytesRead);
        }
        public bool keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo) {
            return Static.keybd_event(bVk, bScan, dwFlags, dwExtraInfo);
        }
        public bool mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInfo)
        {
            return Static.mouse_event(dwFlags, dx, dy, dwData, dwExtraInfo);
        }
        public bool GetAsyncKeyState(int key) {
            return Static.GetAsyncKeyState(key);
        }
        public bool SetCursorPos(int x, int y) {
            return Static.SetCursorPos(x, y);
        }
        public bool GetClientRect(IntPtr hwnd, out RECT rect) {
            return Static.GetClientRect(hwnd, out rect);
        }
        public IntPtr GetForegroundWindow() {
            return Static.GetForegroundWindow();
        }
        public bool SetForegroundWindow(IntPtr hwnd) {
            return Static.SetForegroundWindow(hwnd);
        }
        public bool DwmExtendFrameIntoClientArea(IntPtr hwnd, ref int[] margins) {
            return Static.DwmExtendFrameIntoClientArea(hwnd, ref margins);
        }
    }
}
