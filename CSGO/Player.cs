using csgop.Unmanaged;
using System;

namespace csgop.CSGO {
    unsafe class Player : External<IntPtr> {

        readonly External<int> hp;
        readonly External<int> team;
        readonly External<int> state;
        readonly External<int> consecutiveshots;
        readonly External<bool> dormant;
        readonly PositionVector position;
        readonly Bones bones;

        public Player(Func<IntPtr> GetBaseAddress, int offset) : base(GetBaseAddress, offset) {
            hp = new External<int>(this, 0xFC);
            team = new External<int>(this, 0xF0);
            state = new External<int>(this, 0x100);
            consecutiveshots = new External<int>(this, 0xA2C0);
            dormant = new External<bool>(this, 0xE9);
            position = new PositionVector(this, 0x134);
            bones = new Bones(this, 0x2698);
        }

        internal int Hp {
            get {
                return hp.Value;
            }
        }

        internal int Team {
            get {
                return team.Value;
            }
        }

        internal int State {
            get {
                return state.Value;
            }
        }

        internal int ConsecutiveShots {
            get {
                return consecutiveshots.Value;
            }
        }

        internal bool Dormant {
            get {
                return dormant.Value;
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
