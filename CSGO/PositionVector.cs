using csgop.Unmanaged;

namespace csgop.CSGO {
    unsafe class PositionVector : OffsetDAO {

        readonly External<float> x = sizeof(float) * 0;
        readonly External<float> y = sizeof(float) * 1;
        readonly External<float> z = sizeof(float) * 2;

        public PositionVector(int baseAddress) : base(baseAddress) {
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
