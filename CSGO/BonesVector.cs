using csgop.Unmanaged;
using System;

namespace csgop.CSGO {
    unsafe class BonesVector : OffsetDAO {

        readonly External<float> x = 0x0C;
        readonly External<float> y = 0x1C;
        readonly External<float> z = 0x2C;

        public BonesVector(IntPtr pointerAddressOffset) : base(pointerAddressOffset) {
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