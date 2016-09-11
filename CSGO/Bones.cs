using csgop.Unmanaged;

namespace csgop.CSGO {
    unsafe class Bones : OffsetDAO {

        readonly BonesVector head;
        readonly BonesVector somethingelse;

        public Bones(int baseAddress) : base(baseAddress) {
            head = new BonesVector(baseAddress + 0x30 * 6);
            somethingelse = new BonesVector(baseAddress + 0x30 * 5);
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
