using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace csgop.src {
    class Kernel32 {
        private class Static {
            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool AllocConsole();
            [DllImport("kernel32.dll")]
            public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
            [DllImport("kernel32.dll")]
            public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out uint lpNumberOfBytesRead);
        }
        public bool AllocConsole() {
            return Static.AllocConsole();
        }
        public IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId) {
            return Static.OpenProcess(dwDesiredAccess, bInheritHandle, dwProcessId);
        }
        public bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out uint lpNumberOfBytesRead) {
            return Static.ReadProcessMemory(hProcess, lpBaseAddress, lpBuffer, nSize, out lpNumberOfBytesRead);
        }
    }
}
