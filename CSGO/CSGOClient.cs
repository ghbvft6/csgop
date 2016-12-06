using csgop.Unmanaged;
using System;

namespace csgop.CSGO {

    unsafe class CSGOClient {

        readonly Player player;
        readonly Player[] players;
        readonly ExternalArray<float> view;

        public CSGOClient(Func<IntPtr> GetBaseAddress) {
            player = new Player(GetBaseAddress, 0xA9E8E4);
            players = new Player[24];
            for (var i = 0; i < players.Length; ++i) {
                players[i] = new Player(GetBaseAddress, 0x4AC0CA4 + (i + 1) * 0x10);
            }
            view = new ExternalArray<float>(16, GetBaseAddress, 0x4AB2844, sizeof(float));
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

        internal ExternalArray<float>.Values View {
            get {
                return view.ValuesArray;
            }
        }
    }
}
