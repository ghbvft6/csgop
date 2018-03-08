using CSGOP.Core;
using CSGOP.OS;
using CSGOP.Memory;
using System;
using CSGOP.Common;
using System.Diagnostics;
using CSGOP.Data;
using System.Threading;

namespace CSGOP.Games.Notepad {

    sealed class Process : ExternalProcess<Process> {
        public Process() {
            ProcessName = "notepad";
            client = new Client();
        }
    }

    class Client : Client<Process> {
        public Client() {
            var ptr1 = External.New<IntPtr>("notepad.exe", 0x00024548);
            var ptr2 = External.New<IntPtr>(ptr1, 0);
            text = External.NewArray<byte>(32, ptr2, 0, sizeof(byte));

            new Thread(() => {
                while (true) {
                    if (Process.PHandle != IntPtr.Zero) { // TODO abstract OnAttached()
                        ptr2.UpdatePointingAddresses();
                        text.UpdateAllAddresses();
                        break;
                    }
                    Thread.Sleep(100);
                }
            }).Start();
        }
    }
}
