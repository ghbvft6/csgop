using csgop.Unmanaged;

namespace csgop.CSGO {
    unsafe class Player : OffsetDAO {

        public struct Vector3 { public float x, y, z; };

        readonly External<int> hp = 0xFC;
        readonly External<int> team = 0xF0;
        readonly External<int> state = 0x100;
        readonly External<bool> dormant = 0xE9;
        readonly External<Vector3> position = 0x134;

        public Player(int baseAddress) : base(baseAddress) {
        }

        internal int Hp {
            get {
                return *(int*)hp.Pointer;
            }
        }

        internal int Team
        {
            get {
                return *(int*)team.Pointer;
            }
        }

        internal int State {
            get {
                return *(int*)state.Pointer;
            }
        }

        internal bool Dormant
        {
            get {
                return *(bool*)dormant.Pointer;
            }
        }

        internal Vector3 Position {
            get {
                return *(Vector3*)position.Pointer;
            }
        }
    }
}
