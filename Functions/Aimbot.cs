using csgop.CSGO;
using csgop.Imported;
using System.Threading;
using System;

namespace csgop.Functions {
    class Aimbot {

        private readonly Player player;
        private readonly Player[] players;

        public Aimbot(Player player, Player[] players) {
            this.player = player;
            this.players = players;
        }

        WorldToScreen world = new WorldToScreen(CSGOCheat.csgo.View);

        bool automatic = false;

        public static int width = 1280, height = 720;

        float[] boneout = new float[3];

        float distancetohead = 30.0f, smooth = 3.0f, distance = 0, x = 0, y = 0;

        public void Run() {
            while (true) {
                if (automatic == true || Kernel32.Instance.GetAsyncKeyState(02)) {
                    for (int i = 0; i < players.Length; ++i) {
                        if (players[i].Dormant == false && player.Team != players[i].Team && players[i].Hp > 0 && world.conversion(players[i].Bones.Head, boneout, width, height)) {
                            distance = Algebra.distance(boneout[0], boneout[1], width / 2, height / 2);
                            float mydistance = distancetohead;
                            if (smooth > 1 && distance > 0.1 && distance < mydistance) {
                                i = players.Length + 1;
                                x = (boneout[0] - width / 2) / smooth;
                                y = (boneout[1] - height / 2) / smooth;
                                Kernel32.Instance.mouse_event(01, (uint)x, (uint)y, 0, 0);
                            }
                        }
                    }
                }
                Thread.Sleep(5);
            }
        }
    }
}