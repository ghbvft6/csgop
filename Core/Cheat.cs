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
            new Thread(csgoProcess.MonitorClient).Start();
            csgoProcess.AddCheat(new Thread(() => { while (true) { Games.CSGO.Process.client.UpdateAllAddresses(); Thread.Sleep(500); } }));
            csgoProcess.AddCheat(new Thread(new Bunnyhop(csgo.Player).Run));
            csgoProcess.AddCheat(new Thread(new Aimbot(csgo.Player, csgo.Players).Run));
            csgoProcess.AddCheat(new Thread(new Overlay(csgo.Player, csgo.Players).Run));
        }
    }
}
