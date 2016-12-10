using csgop.Unmanaged;
using System;

namespace csgop.CSGO {
    unsafe class PositionVector : Vector3 {

        readonly External<float> x;
        readonly External<float> y;
        readonly External<float> z;

        public PositionVector(External<IntPtr> player, int positionOffset) {
            x = new External<float>(player, positionOffset + sizeof(float) * 0);
            y = new External<float>(player, positionOffset + sizeof(float) * 1);
            z = new External<float>(player, positionOffset + sizeof(float) * 2);
        }

        float Vector3.X {
            get {
                return x.Value;
            }
        }

        float Vector3.Y {
            get {
                return y.Value;
            }
        }

        float Vector3.Z {
            get {
                return z.Value;
            }
        }
    }
}
