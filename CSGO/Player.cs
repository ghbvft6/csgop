using csgop.Unmanaged;

namespace csgop.CSGO {
    unsafe class Player : OffsetDAO {

        readonly External<int> hp = 0xFC;
        readonly External<bool> isWalking = 102;

        public Player(int baseAddress) : base(baseAddress) {
        }

        internal int Hp {
            get {
                return *(int*)hp.Pointer;
            }
        }

        internal bool IsWalking {
            get {
                return *(bool*)isWalking.Pointer;
            }
        }
    }
}
