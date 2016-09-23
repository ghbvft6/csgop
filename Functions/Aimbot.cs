using csgop.CSGO;
using csgop.Functions;
using csgop.Imported;
using System;
using System.Diagnostics;
using System.Threading;

namespace csgop.Functions {
    class Aimbot {

        private readonly Player player;
        private readonly Player[] players;

        public Aimbot(Player player, Player[] players) {
            this.player = player;
            this.players = players;
        }

        WorldToScreen world = new WorldToScreen(CSGOCheat.csgo.View);
        float[] boneout = new float[3];

        public void Run() {
            while (true) {
                if (Kernel32.Instance.GetAsyncKeyState(02)) {
                    for (int i = 0; i < players.Length; ++i) {
                        if ((!players[i].Dormant) && (player.Team != players[i].Team) && (players[i].Hp > 0) && (world.conversion(players[i].Bones.Head, boneout, 1280, 720))) {

                            Kernel32.Instance.SetCursorPos((int)boneout[0], (int)boneout[1]);

                        }
                    }
                }
                Thread.Sleep(1);
            }
        }
    }
}