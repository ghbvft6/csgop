using csgop.Games.CSGO.Data;
using System;
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
                    if (players[i].Dormant == false && player.Team != players[i].Team && players[i].Hp > 0) {                 
        
                    }
                }
                Thread.Sleep(1);
            }
        }
    }
}


