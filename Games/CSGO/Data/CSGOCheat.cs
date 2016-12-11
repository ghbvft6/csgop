using csgop.Unmanaged;
using csgop.Functions;
using System.Diagnostics;
using System.Threading;

namespace csgop.Games.CSGO.Data {
    class CSGOCheat {

        public static CSGOClient csgo;

        private void AttachToClient() {
            Process.ProcessName = "csgo";

            while (Process.AttachToProccess() == false) {
                Thread.Sleep(1);
            }

            foreach (ProcessModule Module in Process.Process.Modules) {
                if (Module.ModuleName.Equals("client.dll")) {
                    csgo = new CSGOClient(() => Module.BaseAddress);
                    break;
                }
            }
        }

        public void Run() {
            AttachToClient();
            new Thread(() => { while (true) { csgo.UpdateAllAddresses(); Thread.Sleep(500); } } ).Start();
            new Thread(new Bunnyhop(csgo.Player).Run).Start();
            new Thread(new Aimbot(csgo.Player, csgo.Players).Run).Start();
            new Thread(new Overlay(csgo.Player, csgo.Players).Run).Start();
        }
    }
}
