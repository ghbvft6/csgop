using csgop.Core.Data;
using csgop.Unmanaged;
using System;

namespace csgop.Games.CSGO.Data {
    unsafe class BonesVector : BonesVector<Process> {

        public BonesVector(Bones boneBase, int boneOffset) {
            x = new Process<float>(boneBase, boneOffset + 0x0C);
            y = new Process<float>(boneBase, boneOffset + 0x1C);
            z = new Process<float>(boneBase, boneOffset + 0x2C);
        }
    }
}