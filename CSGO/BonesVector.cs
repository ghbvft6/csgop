using csgop.Unmanaged;
using System;

namespace csgop.CSGO {
    unsafe class BonesVector {

        readonly External<float> x;
        readonly External<float> y;
        readonly External<float> z;

        public BonesVector(External<IntPtr> boneBase, int boneOffset) {
            x = new External<float>(boneBase, boneOffset + 0x0C);
            y = new External<float>(boneBase, boneOffset + 0x1C);
            z = new External<float>(boneBase, boneOffset + 0x2C);
        }

        internal float X {
            get {
                return *(float*)x.Pointer;
            }
        }

        internal float Y {
            get {
                return *(float*)y.Pointer;
            }
        }

        internal float Z {
            get {
                return *(float*)z.Pointer;
            }
        }
    }
}