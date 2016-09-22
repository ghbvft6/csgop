using csgop.Unmanaged;
using System;

namespace csgop.CSGO {
    unsafe class PositionVector : OffsetDAO {

        readonly External<float> x = sizeof(float) * 0;
        readonly External<float> y = sizeof(float) * 1;
        readonly External<float> z = sizeof(float) * 2;

        public PositionVector(IntPtr pointerAddressOffset) : base(pointerAddressOffset) {
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
