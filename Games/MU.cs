using CSGOP.Core;
using CSGOP.OS;
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

        public void AttackSpeedCheatBackup() {
            var ptr = External.New<IntPtr>("main.exe", 0x09CDCAC8);
            while (ptr.Value == IntPtr.Zero) {
                Thread.Sleep(10);
            }
            var agility = External.New<ushort>(ptr, 0x1BA);
            while (agility.Value == 0) {
                Thread.Sleep(10);
            }
            new Thread(printTime).Start();
            var agiOrig = agility.Value;
            doForMs(40000, () => agility.Value = 65000);
            agility.Value = agiOrig;
            Thread.Sleep(5000);
            while (true) {
                doForMs(55000, () => agility.Value = 65000);
                agility.Value = agiOrig;
                Thread.Sleep(5000);
            }
        }

        public void AttackSpeedCheat() {
            var ptr = External.New<IntPtr>("main.exe", 0x0811ACC4);
            while (ptr.Value == IntPtr.Zero) {
                Thread.Sleep(10);
            }
            var agility = External.New<ushort>(ptr, 0x154);
            while (agility.Value == 0) {
                Thread.Sleep(10);
            }
            new Thread(printTime).Start();
            var sh = External.New<byte>(0x0019DDA0);
            new Thread(() => {
                while (true) {
                    //sh.Value = 120;
                    Thread.Sleep(10);
                }
            }).Start();
            var agiOrig = agility.Value;
            doForMs(39000, () => agility.Value = 65000);
            agility.Value = agiOrig;
            Thread.Sleep(10000);
            while (true) {
                doForMs(50000, () => agility.Value = 65000);
                agility.Value = agiOrig;
                Thread.Sleep(10000);
            }
        }

        private void printTime() {
            var i = 0;
            while (true) {
                i += 500;
                Thread.Sleep(500);
                Console.WriteLine(i);
            }
        }

        private void doForMs(int ms, Action action) {
            while (ms > 0) {
                action();
                Thread.Sleep(100);
                ms -= 100;
            }
        }
    }
}
