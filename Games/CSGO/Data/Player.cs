using csgop.Core.Data;
using csgop.Unmanaged;
using System;

namespace csgop.Games.CSGO.Data {
    unsafe class Player : Player<Process> {
        public Player(Func<IntPtr> GetBaseAddress, int offset) : base(GetBaseAddress, offset) {
            hp = new Process<int>(this, 0xFC);
            team = new Process<int>(this, 0xF0);
            state = new Process<int>(this, 0x100);
            consecutiveshots = new Process<int>(this, 0xA2C0);
            dormant = new Process<bool>(this, 0xE9);
            position = new PositionVector(this, 0x134);
            bones = new Bones(this, 0x2698);
        }
    }
}
