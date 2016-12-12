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
        private IList<Action> cheats = new List<Action>();
        private IList<Thread> cheatsThreads = new List<Thread>();
        private Func<IntPtr> clientBaseAddress = () => new IntPtr(0);

        public Process() {
            client = new Client(clientBaseAddress);
        }

        public IClient Client {
            get {
                return client;
            }
        }

        public void AddCheat(Action cheat) {
            cheats.Add(cheat);
        }

        public void MonitorClient() {
            while (true) {
                var foundClient = false;
                while (foundClient == false) {
                    while (AttachToProccess() == false) {
                        ProcessName = "csgo";
                        Thread.Sleep(100);
                    }
                    foreach (ProcessModule Module in Process.Modules) {
                        if (Module.ModuleName.Equals("client.dll")) {
                            clientBaseAddress = () => Module.BaseAddress;
                            foundClient = true;
                            break;
                        }
                        Thread.Sleep(100);
                    }
                }
                foreach (var cheat in cheats) {
                    var t = new Thread(() => cheat());
                    cheatsThreads.Add(t);
                    t.Start();
                }
                while (true) {
                    try {
                        var testproc = System.Diagnostics.Process.GetProcessById(Process.Id);
                    } catch (ArgumentException e) {
                        break;
                    }
                    Thread.Sleep(100);
                }
                foreach (var cheat in cheatsThreads) {
                    cheat.Abort();
                }
                cheatsThreads.Clear();
            }
        }

        public void AddCheat(Thread cheat) {
            throw new NotImplementedException();
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
