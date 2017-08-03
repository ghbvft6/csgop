using CSGOP.Data;
using System;
using System.Threading;

namespace CSGOP.Functions {
    class SoundESP {

        private readonly IPlayer player;
        private readonly IPlayer[] players;

        public SoundESP(IPlayer player, IPlayer[] players) {
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


