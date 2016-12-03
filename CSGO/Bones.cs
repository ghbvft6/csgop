using csgop.Unmanaged;
using System;

namespace csgop.CSGO {
    unsafe class Bones : External<IntPtr> {

        readonly BonesVector head;
        readonly BonesVector somethingelse;

        public Bones(AbstractExternal<IntPtr, External> parentObject, int offset) : base(parentObject, offset) {
            head = new BonesVector(this, 0x30 * 6);
            somethingelse = new BonesVector(this, 0x30 * 5);
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
