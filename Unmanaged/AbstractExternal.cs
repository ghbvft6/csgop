using csgop.Imported;
using System;
using System.Runtime.InteropServices;

namespace csgop.Unmanaged {

    interface IExternal {
        IntPtr ExternalPointer { get; set; }
    }

    abstract class AbstractExternal<T, BindingClass> : Unmanaged<T>, IExternal {

        private AbstractExternal<IntPtr, BindingClass> parentObject;
        private IntPtr address;
        private int offset;
        protected readonly static Kernel32 kernel;
        private static uint lpNumberOfBytesRead; // used by ReadProcessMemory()

        private Action UpdateAddress; // mayby better name

        static AbstractExternal() {
            kernel = Kernel32.Instance;
        }

        public AbstractExternal(int address) {
            // NOT USED: parentObject, offset, UpdateAddress
            this.address = new IntPtr(address);
        }

        public AbstractExternal(Func<IntPtr> GetAddress) {
            // NOT USED: parentObject, offset
            this.UpdateAddress = () => { address = GetAddress(); };
            UpdateAddress();
        }

        public unsafe AbstractExternal(AbstractExternal<IntPtr, BindingClass> parentObject, int offset) {
            this.parentObject = parentObject;
            this.offset = offset;
            unsafe
            {
                UpdateAddress = () => { address = *((IntPtr*)parentObject.Pointer) + this.offset; };
            }
            UpdateAddress();
        }

        // TODO + mayby better name
        private void UpdateAncestors() {
            if (parentObject != null)
                parentObject.UpdateAncestors(); // make a stack with root pointer on the top
            else {
                // refresh addresses to the bottom of the stack
            }
            throw new NotImplementedException();
        }

        // TODO
        private void UpdateChildren() {
            throw new NotImplementedException();
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
            get { return address; }
            set { address = value; }
        }

        public void Read() {
            kernel.ReadProcessMemory(ExternalProcess<BindingClass>.PHandle, address, ptr, (uint)Marshal.SizeOf(typeof(T)), out lpNumberOfBytesRead);
        }

        public void Write() {
            throw new NotImplementedException();
        }
    }
}
