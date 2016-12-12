using csgop.Core.Data;
using csgop.Unmanaged;
using System;

namespace csgop.Games.CSGO.Data {
    unsafe class PositionVector : PositionVector<Process> {

        public PositionVector(Player player, int positionOffset) {
            x = new External<float>(player, positionOffset + sizeof(float) * 0);
            y = new External<float>(player, positionOffset + sizeof(float) * 1);
            z = new External<float>(player, positionOffset + sizeof(float) * 2);
        }
    }
}
