using System;
using System.Runtime.InteropServices;

namespace csgop.src {
    class Unmanaged<T> {

        protected IntPtr ptr;

        public Unmanaged() {
            ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(T)));
        }

        ~Unmanaged() {
            Marshal.FreeHGlobal(ptr);
        }

        public T Value {
            get { return Marshal.PtrToStructure<T>(ptr); }
            set { Marshal.StructureToPtr<T>(value, ptr, true); }
        }

        unsafe public void* Pointer {
            get { return ptr.ToPointer(); }
        }
    }
}
