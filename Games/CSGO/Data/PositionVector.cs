using CSGOP.Core.Data;
using CSGOP.Unmanaged;
using System;

namespace CSGOP.Games.CSGO.Data {
    unsafe class PositionVector : PositionVector<Process> {

        public PositionVector(External<IntPtr> playerBase, int positionOffset) {
            x = new External<float>(playerBase, positionOffset + sizeof(float) * 0);
            y = new External<float>(playerBase, positionOffset + sizeof(float) * 1);
            z = new External<float>(playerBase, positionOffset + sizeof(float) * 2);
        }
    }
}
