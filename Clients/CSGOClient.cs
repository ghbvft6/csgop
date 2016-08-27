using csgop.Unmanaged;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace csgop.Clients {
    unsafe class CSGOClient {

        readonly External<int> hp = 0xFC;
        readonly External<bool> isWalking = 102;

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
                    var client = Module.BaseAddress.ToInt32();
                    hp.ExternalPointer += client + 0xA3A43C;
                    isWalking.ExternalPointer += client + 0xA3A43C;
                }
            }

            while (true) {
                Console.WriteLine(Hp);
                Thread.Sleep(1);
            }
        }
    }
}
