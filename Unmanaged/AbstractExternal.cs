using csgop.Imported;
using System;
using System.Runtime.InteropServices;

namespace csgop.Unmanaged {

    interface IExternal {
        IntPtr ExternalPointer { get; set; }
    }

    abstract class AbstractExternal<T, U> : Unmanaged<T>, IExternal {

        protected IntPtr externalPtr;
        protected readonly static Kernel32 kernel;
        private static uint lpNumberOfBytesRead; // used by ReadProcessMemory()

        static AbstractExternal() {
            kernel = Kernel32.Instance;
        }

        public unsafe AbstractExternal(void* externalPtr) : this(new IntPtr(externalPtr)) { }

        public AbstractExternal(IntPtr externalPtr) {
            this.externalPtr = externalPtr;
        }

        public new T Value {
            get {
                Read();
                return base.Value;
            }
            set {
                base.Value = value;
                Write();
            }
        }

        public unsafe new void* Pointer {
            get {
                Read();
                return base.Pointer;
            }
        }

        public IntPtr ExternalPointer {
            get { return externalPtr; }
            set { externalPtr = value; }
        }

        public void Read() {
            kernel.ReadProcessMemory(ExternalProcess<U>.PHandle, externalPtr, ptr, (uint)Marshal.SizeOf(typeof(T)), out lpNumberOfBytesRead);
        }

        public void Write() {
            throw new NotImplementedException();
        }
    }
}
