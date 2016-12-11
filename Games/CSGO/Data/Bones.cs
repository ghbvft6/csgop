using csgop.Unmanaged;
using System;

namespace csgop.Games.CSGO.Data {
    unsafe class Bones : Process<IntPtr> {

        readonly BonesVector head;
        readonly BonesVector somethingelse;

        public Bones(External<IntPtr, Process> parentObject, int offset) : base(parentObject, offset) {
            head = new BonesVector(this, 0x30 * 8);
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
