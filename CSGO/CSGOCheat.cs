using csgop.Unmanaged;
using System;
using System.Diagnostics;
using System.Threading;

namespace csgop.CSGO {
    class CSGOCheat {

        private static CSGOClient csgo;
        private static Player player;
        private static Player[] players = new Player[63];

        private void AttachToClient() {
            External.ProcessName = "csgo";

            while (External.AttachToProccess() == false) {
                Thread.Sleep(1);
            }

            foreach (ProcessModule Module in External.Process.Modules) {
                if (Module.ModuleName.Equals("client.dll")) {
                    csgo = new CSGOClient(Module.BaseAddress.ToInt32());
                    player = new Player(csgo.Player);
                    for (int i = 2; i<players.Length + 2; ++i) {
                        players[i-2] = new Player(csgo.Players + i * 0x10);
                    }
                    break;
                }
            }
        }

        public void Run() {
            AttachToClient();

            new Thread(this.ContinouslyPrintHp).Start();
            new Thread(this.ContinouslyPrintHp2).Start();
        }        

        public void ContinouslyPrintHp() {
            while (true) {
                Console.WriteLine(player.Hp);
                Thread.Sleep(1000);
            }
        }

        public void ContinouslyPrintHp2() {
            while (true) {
                Console.WriteLine(players[0].Hp);
                Thread.Sleep(1000);
            }
        }
    }
}
