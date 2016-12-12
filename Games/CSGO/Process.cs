using csgop.Imported;
using csgop.Unmanaged;
using System;
using System.Diagnostics;

namespace csgop.Games.CSGO {

    sealed class Process : ExternalProcess<Process> {
    }

    class Process<T> : External<T, Process> where T : struct {
        public Process(int address) : base(address) {
        }

        public Process(Func<IntPtr> GetBaseAddress, int offset) : base(GetBaseAddress, offset){
        }

        public Process(External<IntPtr, Process> parentObject, int offset) : base(parentObject, offset) {
        }
    }
}
