using csgop.Games.CSGO;
using csgop.Imported;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace csgop.Unmanaged {

    interface IExternal {
        IntPtr ExternalPointer { get; set; }
        void UpdateAddress();
    }

    static class ExternalExtensions {
        public static void UpdateAllAddresses(this object o) {
            var queue = new Queue<object>();
            queue.Enqueue(o);
            while (queue.Count > 0) {
                var obj = queue.Dequeue();
                if (obj is IExternal externalObj) {
                    externalObj.UpdateAddress();
                }
                var fieldInfos = obj.GetType().GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var fieldInfo in fieldInfos) {
                    if (fieldInfo.FieldType.IsArray) {
                        foreach (var element in fieldInfo.GetValue(obj) as object[]) {
                            if (element == null) continue;
                            if (element.GetType().IsGenericType && element.GetType().GetGenericTypeDefinition() == typeof(External<,>)) continue;
                            queue.Enqueue(element);
                        }
                    } else {
                        if (fieldInfo.GetValue(obj) == null) continue;
                        if (fieldInfo.GetValue(obj).GetType().IsGenericType && fieldInfo.GetValue(obj).GetType().GetGenericTypeDefinition() == typeof(External<,>)) continue;
                        queue.Enqueue(fieldInfo.GetValue(obj));
                    }
                }
            }
        }
    }

    class External {
        public interface IValues<T> where T : struct {
            T this[int i] {
                get;
            }

            int Length {
                get;
            }
        }

        // TODO move
        public static T[] NewArray<T>(int length, Func<int, T> constructor) {
            var array = new T[length];
            for (var i = 0; i < length; ++i) {
                array[i] = constructor(i);
            }
            return array;
        }
    }

    class External<T, BindingClass> : Unmanaged<T>, IExternal where T : struct {

        private External<IntPtr, BindingClass> parentObject;
        private IntPtr address;
        private int offset;
        protected readonly static Kernel32 kernel;
        private static uint lpNumberOfBytesReadOrWritten; // used by ReadProcessMemory()

        private Action UpdateAddressDelegate;

        static External() {
            kernel = Kernel32.Instance;
        }

        public External(int address) {
            // NOT USED: parentObject, offset, UpdateAddress
            this.address = new IntPtr(address);
            this.UpdateAddressDelegate = () => { }; // not null
        }

        public unsafe External(Func<IntPtr> GetBaseAddress, int offset) {
            // NOT USED: parentObject
            this.offset = offset;
            this.UpdateAddressDelegate = () => { address = GetBaseAddress() + this.offset; };
            UpdateAddressDelegate();
        }

        public unsafe External(External<IntPtr, BindingClass> parentObject, int offset) {
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
            kernel.ReadProcessMemory(ExternalProcess<BindingClass>.PHandle, address, ptr, (uint)Marshal.SizeOf(typeof(T)), out lpNumberOfBytesReadOrWritten);
        }

        public void Write() {
            kernel.WriteProcessMemory(ExternalProcess<BindingClass>.PHandle, address, ptr, (uint)Marshal.SizeOf(typeof(T)), out lpNumberOfBytesReadOrWritten);
        }


        public class Array {

            private External<T, BindingClass>[] array;

            public Array(int length, int address, int elementSize) {
                array = new External<T, BindingClass>[length];
                for (var i = 0; i < length; ++i) {
                    array[i] = new External<T, BindingClass>(address + i * elementSize);
                }
            }

            public unsafe Array(int length, Func<IntPtr> GetBaseAddress, int offset, int elementSize) {
                array = new External<T, BindingClass>[length];
                for (var i = 0; i < length; ++i) {
                    array[i] = new External<T, BindingClass>(GetBaseAddress, offset + i * elementSize);
                }
            }

            public unsafe Array(int length, External<IntPtr, BindingClass> parentObject, int offset, int elementSize) {
                array = new External<T, BindingClass>[length];
                for (var i = 0; i < length; ++i) {
                    array[i] = new External<T, BindingClass>(parentObject, offset + i * elementSize);
                }
            }

            public External<T, BindingClass> this[int i] {
                get {
                    return array[i];
                }
            }

            public int Length {
                get {
                    return array.Length;
                }
            }

            public Values ValuesArray {
                get {
                    return new Values(array);
                }
            }
        }

        public class Values : External.IValues<T> {

            public External<T, BindingClass>[] array;

            public Values(External<T, BindingClass>[] array) {
                this.array = array;
            }

            public T this[int i] {
                get {
                    return array[i].Value;
                }
            }

            public int Length {
                get {
                    return array.Length;
                }
            }
        }
    }
}
