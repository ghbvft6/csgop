using csgop.Imported;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csgop.Unmanaged {
    class ExternalProcess<T> {

        private readonly static Kernel32 kernel;
        private static IntPtr pHandle;
        private static Process process;
        private static string processName;

        static ExternalProcess() {
            kernel = Kernel32.Instance;
        }

        public static IntPtr PHandle {
            get { return pHandle; }
            set { pHandle = value; }
        }

        public static Process Process {
            get { return process; }
            set {
                process = value;
                ProcessName = process.ProcessName;
            }
        }

        public static string ProcessName {
            get { return processName; }
            set { processName = value; }
        }

        public static bool AttachToProccess() {
            var processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0) {
                process = processes[0];
            }
            return AttachToProccess(process);
        }

        public static bool AttachToProccess(Process process) {
            if (process != null) {
                pHandle = kernel.OpenProcess(0x0010, false, process.Id);
            }
            return pHandle == IntPtr.Zero ? false : true;
        }
    }
}
