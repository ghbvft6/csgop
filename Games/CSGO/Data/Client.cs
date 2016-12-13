using CSGOP.Core.Data;
using CSGOP.Unmanaged;
using System;

namespace CSGOP.Games.CSGO.Data {

    unsafe class Client : Client<Process> {

        public Client(Func<IntPtr> GetBaseAddress) {
            player = new Player(GetBaseAddress, 0xAA68E4);
            players = External.NewArray(24, (i) => new Player(GetBaseAddress, 0x4AC9224 + (i + 1) * 0x10));
            view = new External<float>.Array(16, GetBaseAddress, 0x4ABADC4, sizeof(float));
        }
    }
}
