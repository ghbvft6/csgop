using CSGOP.Core;
using CSGOP.Imported;
using CSGOP.Unmanaged;
using System;
using System.Diagnostics;
using CSGOP.Core.Data;
using System.Threading;
using CSGOP.Games.CSGO.Data;
using System.Collections.Generic;

namespace CSGOP.Games.MU {

    sealed class Process : ExternalProcess<Process> {

        public Process() {
            ProcessName = "main";
        }

        public void AgilityCheat() {
            var mainexe = new External<IntPtr>("main.exe", 0).ExternalPointer;
            var ptr = new External<IntPtr>(() => mainexe, 0x07D26AC4);
            while (ptr.Value == IntPtr.Zero) {
                Thread.Sleep(10);
            }
            var agility = new External<ushort>((int)ptr.Value + 0x1A);
            while (agility.Value == 0) {
                Thread.Sleep(10);
            }
            var agiOrig = agility.Value;
            agility.Value = 65000;
            Thread.Sleep(44500);
            agility.Value = agiOrig;
            Thread.Sleep(5000);
            agility.Value = 65000;
            while (true) {
                Thread.Sleep(55000);
                agility.Value = agiOrig;
                Thread.Sleep(5000);
                agility.Value = 65000;
            }
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
