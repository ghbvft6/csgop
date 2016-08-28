using csgop.Unmanaged;

namespace csgop.CSGO {
    unsafe class CSGOClient : OffsetDAO {

        readonly External<int> player = 0xA3A43C;
        readonly External<int> players = 0x04A57EA4;

        public CSGOClient(int baseAddress) : base(baseAddress) {
        }

        internal int Player {
            get {
                return *(int*)player.Pointer;
            }
        }

        internal int Players {
            get {
                return *(int*)players.Pointer;
            }
        }
    }
}
