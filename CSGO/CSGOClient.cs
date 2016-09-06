using csgop.Unmanaged;

namespace csgop.CSGO {
    public unsafe struct m { public fixed float i[16]; }

    unsafe class CSGOClient : OffsetDAO {       

        readonly External<int> player = 0xA3A43C;
        readonly External<int> players = 0x04A57EA4;
        public readonly External<m> view = 0x4A49A44;

        public CSGOClient(int baseAddress) : base(baseAddress) {
        }

        public int GetPlayer(int i) {
            return *(int*)new External<int>(players.ExternalPointer + (i + 1) * 0x10).Pointer;
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

        internal m View {
            get {
                return *(m*)view.Pointer;
            }
        }
    }
}
