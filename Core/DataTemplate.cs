using CSGOP.Core.Data;
using CSGOP.Unmanaged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGOP.Core.DataTemplate {
    unsafe class Client {
        protected Player player;
        protected Player[] players;
        protected External<float>.Array view;
    }

    unsafe class Player {
        protected External<int> hp;
        protected External<int> armor;
        protected External<int> team;
        protected External<int> state;
        protected External<int> consecutiveshots;
        protected External<bool> dormant;
        protected PositionVector position;
        protected Bones bones;
        protected External<IntPtr> activeweapon;
        protected External<IntPtr> weaponbase;
        protected External<int> weaponId;
    }

    unsafe class Bones {
        protected BonesVector head;
        protected BonesVector somethingelse;
    }

    unsafe class BonesVector : IVector3 {
        protected External<float> x;
        protected External<float> y;
        protected External<float> z;

        public float X => throw new NotImplementedException();

        public float Y => throw new NotImplementedException();

        public float Z => throw new NotImplementedException();
    }

    unsafe class PositionVector : IVector3 {
        protected External<float> x;
        protected External<float> y;
        protected External<float> z;

        public float X => throw new NotImplementedException();

        public float Y => throw new NotImplementedException();

        public float Z => throw new NotImplementedException();
    }

    class External<T> : External<T, object> where T : struct {
        public External(int address) : base(address) {
        }

        public External(string module, int offset) : base(module, offset) {
        }

        public External(Func<IntPtr> GetBaseAddress, int offset) : base(GetBaseAddress, offset) {
        }

        public External(External<IntPtr, object> parentObject, int offset) : base(parentObject, offset) {
        }
    }
}
