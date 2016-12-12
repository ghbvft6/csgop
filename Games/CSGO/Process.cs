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

    sealed class Process : ExternalProcess<Process>, IGameProcess {
        public static IClient client;
        private IList<Thread> cheats = new List<Thread>();
        private Func<IntPtr> clientBaseAddress = () => new IntPtr(0);


        IntPtr foo () { return clientBaseAddress(); }

        public Process() {
            client = new Client(foo);
        }

        public IClient Client {
            get {
                return client;
            }
        }

        public void AddCheat(Thread cheat) {
            cheats.Add(cheat);
            //cheat.Start();
        }

        public void MonitorClient() {
            
            while (true) {
                while (AttachToProccess() == false) {
                    ProcessName = "csgo";
                    Thread.Sleep(1);
                }
                foreach (ProcessModule Module in Process.Modules) {
                    if (Module.ModuleName.Equals("client.dll")) {
                        clientBaseAddress = () => Module.BaseAddress;
                        break;
                    }
                }
                var y = clientBaseAddress();
                foreach (var cheat in cheats) {
                    cheat.Start();
                }
                while (System.Diagnostics.Process.GetProcessById(Process.Id) != null) {
                    Thread.Sleep(5000);
                    //((Client<Process>)client).player.UpdateAddress();
                    //var x = ((Client<Process>)client).player.address;
                }
                foreach (var cheat in cheats) {
                    cheat.Abort();
                }
            }
        }
    }

    class External<T> : External<T, Process> where T : struct {
        public External(int address) : base(address) {
        }

        public External(Func<IntPtr> GetBaseAddress, int offset) : base(GetBaseAddress, offset){
        }

        public External(External<IntPtr, Process> parentObject, int offset) : base(parentObject, offset) {
        }
    }
}
