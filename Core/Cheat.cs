using csgop.Games.CSGO;
using csgop.Unmanaged;
using csgop.Functions;
using System.Diagnostics;
using System.Threading;
using csgop.Core.Data;
using csgop.Games.CSGO.Data;

namespace csgop.Core {
    class Cheat {

        public static ICSGOClient csgo;

        private void AttachToClient() {
            Games.CSGO.Process.ProcessName = "csgo";

            while (Games.CSGO.Process.AttachToProccess() == false) {
                Thread.Sleep(1);
            }

            foreach (ProcessModule Module in Games.CSGO.Process.Process.Modules) {
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
