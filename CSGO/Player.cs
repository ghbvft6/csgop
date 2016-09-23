using csgop.Unmanaged;
using System;

namespace csgop.CSGO {
    unsafe class Player : OffsetDAO {

        readonly External<int> hp = 0xFC;
        readonly External<int> team = 0xF0;
        readonly External<int> state = 0x100;
        readonly External<bool> dormant = 0xE9;
        readonly PositionVector position = new PositionVector(new IntPtr(0x134));
        readonly Bones bones = new Bones(0x2698);

        public Player(int baseAddress) : base(baseAddress) {
        }

        internal int Hp {
            get {
                return *(int*)hp.Pointer;
            }
        }

        internal int Team {
            get {
                return *(int*)team.Pointer;
            }
        }

        internal int State {
            get {
                return *(int*)state.Pointer;
            }
        }

        internal bool Dormant {
            get {
                return *(bool*)dormant.Pointer;
            }
        }

        internal PositionVector Position {
            get {
                return position;
            }
        }

        internal Bones Bones {
            get {
                return bones;
            }
        }
    }
}
