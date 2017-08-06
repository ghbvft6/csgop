using System;
using System.Runtime.InteropServices;

namespace CSGOP.OS {
    class Kernel {

        private static readonly Kernel instance = new Kernel();

        private Kernel() { }

        internal static Kernel Instance {
            get {
                return instance;
            }
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
    }
}
