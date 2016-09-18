using csgop.Unmanaged;
using System;

namespace csgop.CSGO {

    unsafe class CSGOClient : OffsetDAO {

        readonly Player player = new Player(0xA3A43C);
        readonly Player[] players = new Player[24];
        readonly View view;

        public CSGOClient(Func<IntPtr> GetBaseAddress) : base(GetBaseAddress) {
            for (var i = 0; i < players.Length; ++i) {
                players[i] = new Player(0 + 0x04A57EA4 + (i + 1) * 0x10);
            }
            view = new View(0);
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
