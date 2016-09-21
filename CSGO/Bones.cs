using csgop.Unmanaged;
using System;

namespace csgop.CSGO {
    unsafe class Bones : OffsetDAO {

        readonly BonesVector head = new BonesVector(new IntPtr(0x30* 6));
        readonly BonesVector somethingelse = new BonesVector(new IntPtr(0x30 * 5));

        public Bones(IntPtr pointerAddressOffset) : base(pointerAddressOffset) {
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
