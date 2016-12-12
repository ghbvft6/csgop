using CSGOP.Imported;
using CSGOP.Unmanaged;
using System;
using System.Diagnostics;

namespace CSGOP.Games.CSGO {

    sealed class Process : ExternalProcess<Process> {
    }

    class External<T> : External<T, Process> where T : struct {
        public External(int address) : base(address) {
        }

        public External(Func<IntPtr> GetBaseAddress, int offset) : base(GetBaseAddress, offset){
        }

        public External(External<IntPtr, Process> parentObject, int offset) : base(parentObject, offset) {
        }
    }
}
