using csgop.Imported;
using System;
using System.Diagnostics;

namespace csgop.Unmanaged {

    sealed class External { }

    class External<T> : AbstractExternal<T, External> {
        public External(Func<IntPtr> GetAddress) : base(GetAddress) {
        }

        public External(int address) : base(address) {
        }

        public External(AbstractExternal<IntPtr, External> parentObject, int offset) : base(parentObject, offset) {
        }
    }
}
