using csgop.Unmanaged;
using System;

namespace csgop.CSGO {

    unsafe class CSGOClient : OffsetDAO {

        readonly Player player;
        readonly Player[] players = new Player[24];
        readonly View view;

        public CSGOClient(int baseAddress) : base(baseAddress) {
            player = new Player(baseAddress + 0xA3A43C);
            for (var i = 0; i < players.Length; ++i) {
                players[i] = new Player(baseAddress + 0x04A57EA4 + (i + 1) * 0x10);
            }
            view = new View(baseAddress);
        }

        internal Player Player {
            get {
                return player;
            }
        }

        internal Player[] Players {
            get {
                return players;
            }
        }

        internal View View {
            get {
                return view;
            }
        }
    }
}
