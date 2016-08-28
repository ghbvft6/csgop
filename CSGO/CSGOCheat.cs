using csgop.Unmanaged;
using System;
using System.Diagnostics;
using System.Threading;

namespace csgop.CSGO {
    class CSGOCheat {

        public static CSGOClient csgo;
        public static Player player;
        public static Player[] players = new Player[5];

        public static void AttachToClient() {
            External.ProcessName = "csgo";

            while (External.AttachToProccess() == false) {
                Thread.Sleep(1);
            }

            foreach (ProcessModule Module in External.Process.Modules) {
                if (Module.ModuleName.Equals("client.dll")) {
                    csgo = new CSGOClient(Module.BaseAddress.ToInt32());
                    player = new Player(csgo.Player);
                    for (int i = 0; i<players.Length; ++i) {
                        players[i] = new Player(csgo.GetPlayer(i));
                    }
                    break;
                }
            }
        }     
    }
}
