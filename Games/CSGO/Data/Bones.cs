using CSGOP.Core.Data;
using CSGOP.Unmanaged;
using System;

namespace CSGOP.Games.CSGO.Data {
    unsafe class Bones : Bones<Process> {

        public Bones(External<IntPtr, Process> parentObject, int offset) {
            var boneBase = new External<IntPtr>(parentObject, offset);
            head = new BonesVector(boneBase, 0x30 * 8);
            somethingelse = new BonesVector(boneBase, 0x30 * 5);
        }
    }
}
