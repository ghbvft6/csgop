using CSGOP.Core;
using CSGOP.Imported;
using CSGOP.Unmanaged;
using System;
using System.Diagnostics;
using CSGOP.Core.Data;
using System.Threading;
using System.Collections.Generic;

namespace CSGOP.Games.CSGO {

    sealed class Process : ExternalProcess<Process> {
        public Process() {
            ProcessName = "csgo";
            client = new Client();
        }
    }

    unsafe class Client : Client<Process> {
        public Client() {
            player = new Player("client.dll", 0xAA78E4, 0x4ACA154);
            players = External.NewArray(24, (i) => new Player("client.dll", 0x4ACA154 + (i + 1) * 0x10, 0x4ACA154));
            view = new External<float>.Array(16, "client.dll", 0x4ABBCF4, sizeof(float));
        }
    }

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

    unsafe class Bones : Bones<Process> {
        public Bones(External<IntPtr, Process> playerBase, int offset) {
            var boneBase = new External<IntPtr>(playerBase, offset);
            head = new BonesVector(boneBase, 0x30 * 8);
            somethingelse = new BonesVector(boneBase, 0x30 * 5);
        }
    }

    unsafe class BonesVector : BonesVector<Process> {
        public BonesVector(External<IntPtr> boneBase, int boneOffset) {
            x = new External<float>(boneBase, boneOffset + 0x0C);
            y = new External<float>(boneBase, boneOffset + 0x1C);
            z = new External<float>(boneBase, boneOffset + 0x2C);
        }
    }

    unsafe class PositionVector : PositionVector<Process> {
        public PositionVector(External<IntPtr> playerBase, int positionOffset) {
            x = new External<float>(playerBase, positionOffset + sizeof(float) * 0);
            y = new External<float>(playerBase, positionOffset + sizeof(float) * 1);
            z = new External<float>(playerBase, positionOffset + sizeof(float) * 2);
        }
    }

    class External<T> : External<T, Process> where T : struct {
        public External(int address) : base(address) {
        }

        public External(string module, int offset) : base(module, offset) {
        }

        public External(Func<IntPtr> GetBaseAddress, int offset) : base(GetBaseAddress, offset){
        }

        public External(External<IntPtr, Process> parentObject, int offset) : base(parentObject, offset) {
        }
    }
}
