using CSGOP.Games.CSGO;
using CSGOP.Unmanaged;
using CSGOP.Functions;
using System.Diagnostics;
using System.Threading;
using CSGOP.Core.Data;
using CSGOP.Games.CSGO.Data;

namespace CSGOP.Core {
    class Cheat {
        public void Run() {
            var csgoProcess = new Games.CSGO.Process();
            var csgo = Games.CSGO.Process.client;
            csgoProcess.AddCheat(() => { while (true) { Games.CSGO.Process.client.UpdateAllAddresses(); Thread.Sleep(5000); } });
            csgoProcess.AddCheat(new Bunnyhop(csgo.Player).Run);
            csgoProcess.AddCheat(new Aimbot(csgo.Player, csgo.Players).Run);
            csgoProcess.AddCheat(new Overlay(csgo.Player, csgo.Players).Run);
            //new Thread(csgoProcess.MonitorClient).Start();
            new Thread(new Games.MU.Process().MonitorClient).Start();
        }
    }
}
