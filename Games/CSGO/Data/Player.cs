using CSGOP.Core.Data;
using CSGOP.Unmanaged;
using System;

namespace CSGOP.Games.CSGO.Data {
    unsafe class Player : Player<Process> {
        public Player(string module, int offset, int playersOffset) {
            var playerBase = new External<IntPtr>(module, offset);
            hp = new External<int>(playerBase, 0xFC);
            armor = new External<int>(playerBase, 0xAA04);
            team = new External<int>(playerBase, 0xF0);
            state = new External<int>(playerBase, 0x100);
            consecutiveshots = new External<int>(playerBase, 0xA2C0);
            dormant = new External<bool>(playerBase, 0xE9);
            position = new PositionVector(playerBase, 0x134);
            bones = new Bones(playerBase, 0x2698);
            activeweapon = new External<IntPtr>(playerBase, 0x1337);
            weaponbase = new External<IntPtr>(() => new External<IntPtr>(module, 0).ExternalPointer + playersOffset + ((activeweapon.Value.ToInt32() & 0xFFF) - 1) * 0x10, 0);
            weaponId = new External<int>(weaponbase, 0x1337);
        }
    }
}
