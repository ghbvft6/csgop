using csgop.CSGO;
using csgop.Functions;
using csgop.Imported;
using System;
using System.Diagnostics;
using System.Threading;


namespace csgop.Functions {
    class SoundESP {

        private readonly Player player;
        private readonly Player[] players;

        public SoundESP(Player player, Player[] players) {
            this.player = player;
            this.players = players;
        }

        public void Run() {
            while (true) {
                for (int i = 0; i < players.Length; ++i) {
                    if ((!players[i].Dormant) && (player.Team != players[i].Team) && (players[i].Hp > 0)) {
                        float distance = Algebra.distance(player.Position.x, player.Position.y, players[i].Position.x, players[i].Position.y);
                        Console.Beep(300, 150);
                        Thread.Sleep((int)(distance / 3));
                    }
                }
                Thread.Sleep(1);
            }
        }
    }
}


