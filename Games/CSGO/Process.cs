using CSGOP.Core;
using CSGOP.Imported;
using CSGOP.Unmanaged;
using System;
using System.Diagnostics;
using CSGOP.Core.Data;
using System.Threading;
using CSGOP.Games.CSGO.Data;
using System.Collections.Generic;

namespace CSGOP.Games.CSGO {

    sealed class Process : ExternalProcess<Process> {

        public Process() {
            ProcessName = "csgo";
            client = new Client(() => clientBaseAddress);
        }

        public override void SetClientBaseAddress() {
            clientBaseAddress = new External<IntPtr>("client.dll", 0).ExternalPointer;
        }
    }

    class External<T> : External<T, Process> where T : struct {
        public External(int address) : base(address) {
        }

        public External(string module, int offset) : base(module, offset) {
        }

        public External(Func<IntPtr> GetBaseAddress, int offset) : base(GetBaseAddress, offset){
        }

        public External(External<IntPtr, Process> parentObject, int offset) : base(parentObject, offset) {
        }
    }
}
