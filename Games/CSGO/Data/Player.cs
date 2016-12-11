using csgop.Unmanaged;
using System;

namespace csgop.Games.CSGO.Data {
    unsafe class Player : Process<IntPtr> {

        readonly Process<int> hp;
        readonly Process<int> team;
        readonly Process<int> state;
        readonly Process<int> consecutiveshots;
        readonly Process<bool> dormant;
        readonly PositionVector position;
        readonly Bones bones;

        public Player(Func<IntPtr> GetBaseAddress, int offset) : base(GetBaseAddress, offset) {
            hp = new Process<int>(this, 0xFC);
            team = new Process<int>(this, 0xF0);
            state = new Process<int>(this, 0x100);
            consecutiveshots = new Process<int>(this, 0xA2C0);
            dormant = new Process<bool>(this, 0xE9);
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
