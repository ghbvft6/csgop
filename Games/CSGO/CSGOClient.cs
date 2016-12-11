using csgop.Unmanaged;
using System;

namespace csgop.Games.CSGO {

    unsafe class CSGOClient {

        readonly Player player;
        readonly Player[] players;
        readonly External.Array<float> view;

        public CSGOClient(Func<IntPtr> GetBaseAddress) {
            player = new Player(GetBaseAddress, 0xAA38E4);
            players = External.NewArray<Player>(24, GetBaseAddress, 0x4AC5E94 + 0x10, 0x10);
            view = new External.Array<float>(16, GetBaseAddress, 0x4AB7A34, sizeof(float));
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

        internal External.Values<float> View {
            get {
                return view.ValuesArray;
            }
        }
    }
}
