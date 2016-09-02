using csgop.Imported;
using System;
using System.Diagnostics;

namespace csgop.Unmanaged {

    sealed class External { }

    sealed class External<T> : AbstractExternal<T, External> {

        public unsafe External(void* externalPtr) : base(externalPtr) { }

        public External(IntPtr externalPtr) : base(externalPtr) { }

        public unsafe static implicit operator External<T>(int externalPointer) {
            return new External<T>(new IntPtr(externalPointer));
        }
    }
}
