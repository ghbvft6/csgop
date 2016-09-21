using csgop.Unmanaged;
using System;

namespace csgop.CSGO {
    unsafe class View : OffsetDAO {

        readonly External<float>[] view = InitView();

        static External<float>[] InitView() {
            var view = new External<float>[16];
            for (var i = 0; i < view.Length; ++i) {
                view[i] = new External<float>(new IntPtr(i * sizeof(float)));
            }
            return view;
        }

        public View(IntPtr pointerAddressOffset) : base(pointerAddressOffset) {
        }

        public float this[int i] {
            get {
                return *(float*)view[i].Pointer;
            }
        }
    }
}
