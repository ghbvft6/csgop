using csgop.Unmanaged;
using System;

namespace csgop.CSGO {
    unsafe class Player : External<IntPtr> {

        readonly External<int> hp;
        readonly External<int> team;
        readonly External<int> state;
        readonly External<bool> dormant;
        readonly PositionVector position;
        readonly Bones bones;

        public Player(AbstractExternal<IntPtr, External> parentObject, int offset) : base(parentObject, offset) {
            hp = new External<int>(this, 0xFC);
            team = new External<int>(this, 0xF0);
            state = new External<int>(this, 0x100);
            dormant = new External<bool>(this, 0xE9);
            position = new PositionVector(this, 0x134);
            bones = new Bones(this, 0x2698);
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
