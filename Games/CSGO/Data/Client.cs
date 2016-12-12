using csgop.Core.Data;
using csgop.Unmanaged;
using System;

namespace csgop.Games.CSGO.Data {

    unsafe class Client : Client<Process> {

        public Client(Func<IntPtr> GetBaseAddress) {
            player = new Player(GetBaseAddress, 0xAA38E4);
            players = External.NewArray(24, (i) => new Player(GetBaseAddress, 0x4AC5E94 + (i + 1) * 0x10));
            view = new External<float>.Array(16, GetBaseAddress, 0x4AB7A34, sizeof(float));
        }
    }
}
