using CSGOP.Core.Data;
using CSGOP.Unmanaged;
using System;

namespace CSGOP.Games.CSGO.Data {

    unsafe class Client : Client<Process> {

        public Client() {
            Func<IntPtr> clientdllAddress = () => { return new External<IntPtr>("client.dll", 0).ExternalPointer; };
            player = new Player(clientdllAddress, 0xAA78E4, 0x4ACA154);
            players = External.NewArray(24, (i) => new Player(clientdllAddress, 0x4ACA154 + (i + 1) * 0x10, 0x4ACA154));
            view = new External<float>.Array(16, clientdllAddress, 0x4ABBCF4, sizeof(float));
        }
    }
}
