using csgop.Unmanaged;
using System;

namespace csgop.CSGO {
    unsafe class View : OffsetDAO {

        readonly External<float>[] view = new External<float>[16];

        public View(int baseAddress) : base(baseAddress) {
            for (var i = 0; i < view.Length; ++i) {
                view[i] = new External<float>(new IntPtr(baseAddress + 0x4A49A44 + i * sizeof(float)));
            }
        }

        public float this[int i] {
            get {
                return *(float*)view[i].Pointer;
            }
        }
    }
}
