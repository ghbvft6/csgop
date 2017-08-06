using System;
using System.Runtime.InteropServices;

namespace CSGOP.OS {
    class Windows {

        private static readonly Windows instance = new Windows();

        private Windows() { }

        internal static Windows Instance {
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
            [DllImport("user32.dll")]
            public static extern bool GetClientRect(IntPtr hwnd, out RECT rect);
            [DllImport("user32.dll")]
            public static extern IntPtr GetForegroundWindow();
            [DllImport("User32.dll")]
            public static extern bool SetForegroundWindow(IntPtr hwnd);
            [DllImport("dwmapi.dll")]
            public static extern bool DwmExtendFrameIntoClientArea(IntPtr hwnd, ref int[] margins);
            [DllImport("user32.dll")]
            public static extern UInt32 GetWindowLong(IntPtr hwnd, int index);
            [DllImport("user32.dll")]
            public static extern bool SetWindowLong(IntPtr hwnd, int index, IntPtr newlong);
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
        public UInt32 GetWindowLong(IntPtr hwnd, int index) {
            return Static.GetWindowLong(hwnd, index);
        }
        public bool SetWindowLong(IntPtr hwnd, int index, IntPtr newlong) {
            return Static.SetWindowLong(hwnd, index, newlong);
        }
    }
}
