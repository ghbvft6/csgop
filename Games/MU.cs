using CSGOP.Core;
using CSGOP.Imported;
using CSGOP.Memory;
using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;

namespace CSGOP.Games.MU {

    sealed class Process : ExternalProcess<Process> {

        public Process() {
            ProcessName = "main";
        }

        public void AgilityCheat() {
            var ptr = External.New<IntPtr>("main.exe", 0x07D26AC4);
            while (ptr.Value == IntPtr.Zero) {
                Thread.Sleep(10);
            }
            var agility = External.New<ushort>(ptr, 0x1A);
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
}
