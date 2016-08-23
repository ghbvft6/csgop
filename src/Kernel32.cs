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
        }
        public bool AllocConsole() {
            return Static.AllocConsole();
        }
    }
}
