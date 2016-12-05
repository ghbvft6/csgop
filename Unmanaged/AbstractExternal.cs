using csgop.Imported;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace csgop.Unmanaged {

    interface IExternal {
        IntPtr ExternalPointer { get; set; }
        void UpdateAddress();
    }

    abstract class AbstractExternal<T, BindingClass> : Unmanaged<T>, IExternal where T : struct {

        private AbstractExternal<IntPtr, BindingClass> parentObject;
        private IntPtr address;
        private int offset;
        protected readonly static Kernel32 kernel;
        private static uint lpNumberOfBytesRead; // used by ReadProcessMemory()

        private Action UpdateAddressDelegate;

        static AbstractExternal() {
            kernel = Kernel32.Instance;
        }

        public AbstractExternal(int address) {
            // NOT USED: parentObject, offset, UpdateAddress
            this.address = new IntPtr(address);
            this.UpdateAddressDelegate = () => { }; // not null
        }

        public unsafe AbstractExternal(Func<IntPtr> GetBaseAddress, int offset) {
            // NOT USED: parentObject
            this.offset = offset;
            this.UpdateAddressDelegate = () => { address = GetBaseAddress() + this.offset; };
            UpdateAddressDelegate();
        }

        public unsafe AbstractExternal(AbstractExternal<IntPtr, BindingClass> parentObject, int offset) {
            this.parentObject = parentObject;
            this.offset = offset;
            unsafe
            {
                UpdateAddressDelegate = () => { address = *((IntPtr*)parentObject.Pointer) + this.offset; };
            }
            UpdateAddressDelegate();
        }

        public void UpdatePointingAddresses() {
            if (parentObject != null) {
                parentObject.UpdatePointingAddresses();
                UpdateAddressDelegate();
            } else {
                UpdateAddressDelegate();
            }
        }

        public void UpdateAllAddresses() {
            var queue = new Queue<object>();
            queue.Enqueue(this);
            while (queue.Count>0) {
                var obj = queue.Dequeue();
                if (obj is IExternal externalObj) {
                    externalObj.UpdateAddress();
                }
                var fieldInfos = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var fieldInfo in fieldInfos) {
                    if (fieldInfo.FieldType.IsArray) {
                        foreach (var element in fieldInfo.GetValue(obj) as object[]) {
                            queue.Enqueue(element);
                        }
                    } else {
                        queue.Enqueue(fieldInfo.GetValue(obj));
                    }
                }
            }
        }

        public void UpdateAddress() {
            UpdateAddressDelegate();
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
