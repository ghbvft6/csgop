using csgop.Unmanaged;
using System;

namespace csgop.CSGO {

    unsafe class CSGOClient {

        readonly Player player;
        readonly Player[] players;
        readonly View view;

        public CSGOClient(Func<IntPtr> GetBaseAddress) {
            player = new Player(GetBaseAddress, 0xA3B43C);
            players = new Player[24];
            for (var i = 0; i < players.Length; ++i) {
                players[i] = new Player(GetBaseAddress, 0x4A58F14 + (i + 1) * 0x10);
            }
            view = new View(GetBaseAddress, 0x4A4AAB4);
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
