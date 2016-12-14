using CSGOP.Unmanaged;
using System;

namespace CSGOP.Core.Data {
    unsafe class Player<BindingClass> : External<IntPtr, BindingClass>, IPlayer {

        protected External<int, BindingClass> hp;
        protected External<int, BindingClass> armor;
        protected External<int, BindingClass> team;
        protected External<int, BindingClass> state;
        protected External<int, BindingClass> consecutiveshots;
        protected External<bool, BindingClass> dormant;
        protected PositionVector<BindingClass> position;
        protected Bones<BindingClass> bones;

        public Player(int address) : base(address) {
        }

        public Player(Func<IntPtr> GetBaseAddress, int offset) : base(GetBaseAddress, offset) {
        }

        public Player(External<IntPtr, BindingClass> parentObject, int offset) : base(parentObject, offset) {
        }

        int IPlayer.Hp {
            get {
                return hp.Value;
            }
        }

        int IPlayer.Armor {
            get {
                return armor.Value;
            }
        }

        int IPlayer.Team {
            get {
                return team.Value;
            }
        }

        int IPlayer.State {
            get {
                return state.Value;
            }
        }

        int IPlayer.ConsecutiveShots {
            get {
                return consecutiveshots.Value;
            }
        }

        bool IPlayer.Dormant {
            get {
                return dormant.Value;
            }
        }

        IPositionVector IPlayer.Position {
            get {
                return position;
            }
        }

        IBones IPlayer.Bones {
            get {
                return bones;
            }
        }
    }
}
