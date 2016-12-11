using csgop.Unmanaged;
using System;

namespace csgop.Games.CSGO.Data {
    unsafe class BonesVector : Vector3 {

        readonly Process<float> x;
        readonly Process<float> y;
        readonly Process<float> z;

        public BonesVector(Process<IntPtr> boneBase, int boneOffset) {
            x = new Process<float>(boneBase, boneOffset + 0x0C);
            y = new Process<float>(boneBase, boneOffset + 0x1C);
            z = new Process<float>(boneBase, boneOffset + 0x2C);
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