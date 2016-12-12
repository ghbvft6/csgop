using CSGOP.Core.Data;
using CSGOP.Unmanaged;
using System;

namespace CSGOP.Games.CSGO.Data {
    unsafe class BonesVector : BonesVector<Process> {

        public BonesVector(Bones boneBase, int boneOffset) {
            x = new External<float>(boneBase, boneOffset + 0x0C);
            y = new External<float>(boneBase, boneOffset + 0x1C);
            z = new External<float>(boneBase, boneOffset + 0x2C);
        }
    }
}