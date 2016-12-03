using csgop.Unmanaged;
using System;

namespace csgop.CSGO {
    unsafe class View {

        readonly External<float>[] view;

        public View (External<IntPtr> client, int viewOffset) {
            view = new External<float>[16];
            for (var i = 0; i < view.Length; ++i) {
                view[i] = new External<float>(client, viewOffset + i * sizeof(float));
            }
        }

        public float this[int i] {
            get {
                return *(float*)view[i].Pointer;
            }
        }
    }
}
