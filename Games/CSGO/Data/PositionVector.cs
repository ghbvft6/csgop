using csgop.Unmanaged;
using System;

namespace csgop.Games.CSGO.Data {
    unsafe class PositionVector : Vector3 {

        readonly Process<float> x;
        readonly Process<float> y;
        readonly Process<float> z;

        public PositionVector(Process<IntPtr> player, int positionOffset) {
            x = new Process<float>(player, positionOffset + sizeof(float) * 0);
            y = new Process<float>(player, positionOffset + sizeof(float) * 1);
            z = new Process<float>(player, positionOffset + sizeof(float) * 2);
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
