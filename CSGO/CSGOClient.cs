using csgop.Unmanaged;
using System;

namespace csgop.CSGO {

    unsafe class CSGOClient {

        readonly Player player;
        readonly Player[] players;
        readonly External.Array<float> view;

        public CSGOClient(Func<IntPtr> GetBaseAddress) {
            player = new Player(GetBaseAddress, 0xA9E8E4);
            players = External.NewArray(24, (i) => new Player(GetBaseAddress, 0x4AC0CA4 + (i + 1) * 0x10));
            view = new External.Array<float>(16, GetBaseAddress, 0x4AB2844, sizeof(float));
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
