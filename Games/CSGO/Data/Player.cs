using CSGOP.Core.Data;
using CSGOP.Unmanaged;
using System;

namespace CSGOP.Games.CSGO.Data {
    unsafe class Player : Player<Process> {
        public Player(Func<IntPtr> clientdllAddress, int offset, int playersOffset) : base(clientdllAddress, offset) {
            hp = new External<int>(this, 0xFC);
            armor = new External<int>(this, 0xAA04);
            team = new External<int>(this, 0xF0);
            state = new External<int>(this, 0x100);
            consecutiveshots = new External<int>(this, 0xA2C0);
            dormant = new External<bool>(this, 0xE9);
            position = new PositionVector(this, 0x134);
            bones = new Bones(this, 0x2698);
            activeweapon = new External<IntPtr>(this, 0x1337);
            weaponbase = new External<IntPtr>(() => clientdllAddress() + playersOffset + ((activeweapon.Value.ToInt32() & 0xFFF) - 1) * 0x10, 0);
            weaponId = new External<int>(weaponbase, 0x1337);
        }
    }
}
