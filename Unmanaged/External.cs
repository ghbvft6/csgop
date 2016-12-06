using csgop.Imported;
using System;
using System.Diagnostics;

namespace csgop.Unmanaged {

    sealed class External { }

    class External<T> : AbstractExternal<T, External> where T : struct {
        public External(int address) : base(address) {
        }

        public External(Func<IntPtr> GetBaseAddress, int offset) : base(GetBaseAddress, offset)
        {
        }

        public External(AbstractExternal<IntPtr, External> parentObject, int offset) : base(parentObject, offset) {
        }
    }

    class ExternalArray<T> : AbstractExternalArray<T, External> where T : struct {
        public ExternalArray(int length, int address, int elementSize) : base(length, address, elementSize) {
        }

        public ExternalArray(int length, AbstractExternal<IntPtr, External> parentObject, int offset, int elementSize) : base(length, parentObject, offset, elementSize) {
        }

        public ExternalArray(int length, Func<IntPtr> GetBaseAddress, int offset, int elementSize) : base(length, GetBaseAddress, offset, elementSize) {
        }
    }
}
