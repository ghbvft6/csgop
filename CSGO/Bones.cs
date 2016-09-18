using csgop.Unmanaged;

namespace csgop.CSGO {
    unsafe class Bones : OffsetDAO {

        readonly BonesVector head = new BonesVector(0x30* 6);
        readonly BonesVector somethingelse = new BonesVector(0x30 * 5);

        public Bones(int pointerAddressOffset) : base(pointerAddressOffset) {
        }

        internal BonesVector Head {
            get {
                return head;
            }
        }

        internal BonesVector Somethingelse {
            get {
                return somethingelse;
            }
        }
    }
}
