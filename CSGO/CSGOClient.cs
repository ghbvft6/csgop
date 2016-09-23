using csgop.Unmanaged;
using System;

namespace csgop.CSGO {

    unsafe class CSGOClient : OffsetDAO {

        readonly Player player = new Player(0xA3B43C);
        readonly Player[] players = InitPlayers();
        readonly View view = new View(new IntPtr(0x4A4AAB4));

        static Player[] InitPlayers() {
            var players = new Player[24];
            for (var i = 0; i < players.Length; ++i) {
                players[i] = new Player(0x4A58F14 + (i + 1) * 0x10);
            }
            return players;
        }

        public CSGOClient(Func<IntPtr> GetBaseAddress) : base(GetBaseAddress) {
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
