using csgop.Imported;
using System;
using System.Diagnostics;

namespace csgop.Unmanaged {

    sealed class External {

        private readonly static Kernel32 kernel;
        internal static IntPtr pHandle; // TODO fix accessibility level
        private static Process process;
        private static string processName;

        static External() {
            kernel = Kernel32.Instance;
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

    sealed class External<T> : AbstractExternal<T> {

        public unsafe External(void* externalPtr) : base(externalPtr) { }

        public External(IntPtr externalPtr) : base(externalPtr) { }

        public unsafe static implicit operator External<T>(int externalPointer) {
            return new External<T>(new IntPtr(externalPointer));
        }

        public override IntPtr PHandle {
            get { return External.pHandle; }
            set { External.pHandle = value; }
        }
    }
}
