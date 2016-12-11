using csgop.Imported;
using csgop.Unmanaged;
using System;
using System.Diagnostics;

namespace csgop.Games.CSGO {

    sealed class External : AbstractExternal<External> {
    }

    class External<T> : AbstractExternal<T, External> where T : struct {
        public External(int address) : base(address) {
        }

        public External(Func<IntPtr> GetBaseAddress, int offset) : base(GetBaseAddress, offset){
        }

        public External(AbstractExternal<IntPtr, External> parentObject, int offset) : base(parentObject, offset) {
        }
    }
}
