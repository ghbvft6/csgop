using csgop.Unmanaged;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace csgop.Clients {
    unsafe class CSGOClient {

        readonly External<int> client;
        readonly External<int> hp = 0xFC;
        readonly External<bool> isWalking = 102;

        internal int Client {
            get {
                return *(int*)client.Pointer;
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
                    client.ExternalPointer = Module.BaseAddress;
                    hp.ExternalPointer += Client + 0xA3A43C;
                    isWalking.ExternalPointer += Client + 0xA3A43C;
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
