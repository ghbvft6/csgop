using System;
using System.Runtime.InteropServices;

namespace CSGOP.OS {
    class Devices {

        private static readonly Devices instance = new Devices();

        private Devices() { }

        internal static Devices Instance {
            get {
                return instance;
            }
        }

        private class Static {
            [DllImport("user32.dll")]
            public static extern bool keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
            [DllImport("user32.dll")]
            public static extern bool mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInfo);
            [DllImport("user32.dll")]
            public static extern bool GetAsyncKeyState(int key);
            [DllImport("user32.dll")]
            public static extern bool SetCursorPos(int x, int y);
        }

        public bool keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo) {
            return Static.keybd_event(bVk, bScan, dwFlags, dwExtraInfo);
        }
        public bool mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInfo) {
            return Static.mouse_event(dwFlags, dx, dy, dwData, dwExtraInfo);
        }
        public bool GetAsyncKeyState(int key) {
            return Static.GetAsyncKeyState(key);
        }
        public bool SetCursorPos(int x, int y) {
            return Static.SetCursorPos(x, y);
        }
    }
}
