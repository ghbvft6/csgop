using csgop.Unmanaged;

namespace csgop.CSGO {
    unsafe class Player : OffsetDAO {

        readonly External<int> hp = 0xFC;
        readonly External<int> state = 0x100;

        public Player(int baseAddress) : base(baseAddress) {
        }

        internal int Hp {
            get {
                return *(int*)hp.Pointer;
            }
        }

        internal int State {
            get {
                return *(int*)state.Pointer;
            }
        }
    }
}
