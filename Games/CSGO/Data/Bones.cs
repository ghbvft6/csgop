using csgop.Core.Data;
using csgop.Unmanaged;
using System;

namespace csgop.Games.CSGO.Data {
    unsafe class Bones : Bones<Process> {

        public Bones(External<IntPtr, Process> parentObject, int offset) : base(parentObject, offset) {
            head = new BonesVector(this, 0x30 * 8);
            somethingelse = new BonesVector(this, 0x30 * 5);
        }
    }
}
