using csgop.Unmanaged;
using System;

namespace csgop.CSGO {
    unsafe class PositionVector {

        readonly External<float> x;
        readonly External<float> y;
        readonly External<float> z;

        public PositionVector(External<IntPtr> player, int positionOffset) {
            x = new External<float>(player, positionOffset + sizeof(float) * 0);
            y = new External<float>(player, positionOffset + sizeof(float) * 1);
            z = new External<float>(player, positionOffset + sizeof(float) * 2);
        }

        internal float X {
            get {
                return x.Value;
            }
        }

        internal float Y {
            get {
                return y.Value;
            }
        }

        internal float Z {
            get {
                return z.Value;
            }
        }
    }
}
