using csgop.Unmanaged;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace csgop.Clients {
    unsafe class CSGOClient {

        readonly External<int> localbase = 0;
        readonly External<int> hp = 0xFC;
        readonly External<bool> isWalking = 102;

        internal int Localbase {
            get {
                return *(int*)localbase.Pointer;
            }
        }

        internal int Hp {
            get {
                return *(int*)hp.Pointer;
            }
        }

        internal bool IsWalking {
            get {
                return *(bool*)isWalking.Pointer;
            }
        }

        public void Run() {
            External.ProcessName = "csgo";

            while (External.AttachToProccess() == false) {
                Thread.Sleep(1);
            }

            foreach (ProcessModule Module in External.Process.Modules) {
                if (Module.ModuleName.Equals("client.dll")) {
                    localbase.ExternalPointer = Module.BaseAddress + 0xA3A43C;
                    hp.ExternalPointer += Localbase;
                    isWalking.ExternalPointer += Localbase;
                    break;
                }
            }

            while (true) {
                Console.WriteLine(Hp);
                Thread.Sleep(1000);
            }
        }
    }
}
