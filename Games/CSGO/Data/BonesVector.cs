using csgop.Core.Data;
using csgop.Unmanaged;
using System;

namespace csgop.Games.CSGO.Data {
    unsafe class BonesVector : BonesVector<Process> {

        public BonesVector(Bones boneBase, int boneOffset) {
            x = new External<float>(boneBase, boneOffset + 0x0C);
            y = new External<float>(boneBase, boneOffset + 0x1C);
            z = new External<float>(boneBase, boneOffset + 0x2C);
        }
    }
}