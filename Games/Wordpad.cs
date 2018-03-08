using CSGOP.Core;
using CSGOP.OS;
using CSGOP.Memory;
using System;
using CSGOP.Common;
using System.Diagnostics;
using CSGOP.Data;
using System.Threading;

namespace CSGOP.Games.Wordpad {

    sealed class Process : ExternalProcess<Process> {
        public Process() {
            ProcessName = "wordpad";
            client = new Client();
        }
    }

    class Client : Client<Process> {
        public Client() {
            text = External.NewArray<byte>(32, "MSCTF.dll", 0x110250, sizeof(byte));

            new Thread(() => {
                while (true) {
                    if (Process.PHandle != IntPtr.Zero) { // TODO abstract OnAttached()
                        text.UpdateAllAddresses();
                        break;
                    }
                    Thread.Sleep(100);
                }
            }).Start();
        }
    }
}
