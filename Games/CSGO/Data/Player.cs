using csgop.Core.Data;
using csgop.Unmanaged;
using System;

namespace csgop.Games.CSGO.Data {
    unsafe class Player : Player<Process> {
        public Player(Func<IntPtr> GetBaseAddress, int offset) : base(GetBaseAddress, offset) {
            hp = new External<int>(this, 0xFC);
            team = new External<int>(this, 0xF0);
            state = new External<int>(this, 0x100);
            consecutiveshots = new External<int>(this, 0xA2C0);
            dormant = new External<bool>(this, 0xE9);
            position = new PositionVector(this, 0x134);
            bones = new Bones(this, 0x2698);
        }
    }
}
