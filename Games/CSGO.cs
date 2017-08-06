using CSGOP.Core;
using CSGOP.Imported;
using CSGOP.Unmanaged;
using System;
using CSGOP.Common;
using System.Diagnostics;
using CSGOP.Data;
using System.Threading;
using System.Collections.Generic;

namespace CSGOP.Games.CSGO {

    sealed class Process : ExternalProcess<Process> {
        public Process() {
            ProcessName = "csgo";
            client = new Client();
        }
    }

    class Client : Client<Process> {
        public Client() {
            player = new Player("client.dll", 0xAA78E4, 0x4ACA154);
            players = Common.Array.New(24, (i) => new Player("client.dll", 0x4ACA154 + (i + 1) * 0x10, 0x4ACA154));
            view = External.NewArray<float>(16, "client.dll", 0x4ABBCF4, sizeof(float));
        }
    }

    class Player : Player<Process> {
        public Player(string module, int offset, int playersOffset) {
            var playerBase = External.New<IntPtr>(module, offset);
            hp = External.New<int>(playerBase, 0xFC);
            armor = External.New<int>(playerBase, 0xAA04);
            team = External.New<int>(playerBase, 0xF0);
            state = External.New<int>(playerBase, 0x100);
            consecutiveshots = External.New<int>(playerBase, 0xA2C0);
            dormant = External.New<bool>(playerBase, 0xE9);
            position = new PositionVector(playerBase, 0x134);
            bones = new Bones(playerBase, 0x2698);
            activeweapon = External.New<IntPtr>(playerBase, 0x1337);
            weaponbase = External.New<IntPtr>(() => External.New<IntPtr>(module, 0).ExternalPointer + playersOffset + ((activeweapon.Value.ToInt32() & 0xFFF) - 1) * 0x10, 0);
            weaponId = External.New<int>(weaponbase, 0x1337);
        }
    }

    class Bones : Bones<Process> {
        public Bones(External<IntPtr> playerBase, int offset) {
            var boneBase = External.New<IntPtr>(playerBase, offset);
            head = new BonesVector(boneBase, 0x30 * 8);
            somethingelse = new BonesVector(boneBase, 0x30 * 5);
        }
    }

    class BonesVector : BonesVector<Process> {
        public BonesVector(External<IntPtr> boneBase, int boneOffset) {
            x = External.New<float>(boneBase, boneOffset + 0x0C);
            y = External.New<float>(boneBase, boneOffset + 0x1C);
            z = External.New<float>(boneBase, boneOffset + 0x2C);
        }
    }

    class PositionVector : PositionVector<Process> {
        public PositionVector(External<IntPtr> playerBase, int positionOffset) {
            x = External.New<float>(playerBase, positionOffset + sizeof(float) * 0);
            y = External.New<float>(playerBase, positionOffset + sizeof(float) * 1);
            z = External.New<float>(playerBase, positionOffset + sizeof(float) * 2);
        }
    }
}
