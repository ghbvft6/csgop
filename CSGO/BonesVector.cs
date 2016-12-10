using csgop.Unmanaged;
using System;

namespace csgop.CSGO {
    unsafe class BonesVector : Vector3 {

        readonly External<float> x;
        readonly External<float> y;
        readonly External<float> z;

        public BonesVector(External<IntPtr> boneBase, int boneOffset) {
            x = new External<float>(boneBase, boneOffset + 0x0C);
            y = new External<float>(boneBase, boneOffset + 0x1C);
            z = new External<float>(boneBase, boneOffset + 0x2C);
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