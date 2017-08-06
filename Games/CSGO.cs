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

    unsafe class Client : Client<Process> {
        public Client() {
            player = new Player("client.dll", 0xAA78E4, 0x4ACA154);
            players = Common.Array.New(24, (i) => new Player("client.dll", 0x4ACA154 + (i + 1) * 0x10, 0x4ACA154));
            view = External.NewArray<float>(16, "client.dll", 0x4ABBCF4, sizeof(float));
        }
    }

    unsafe class Player : Player<Process> {
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

    unsafe class Bones : Bones<Process> {
        public Bones(External<IntPtr> playerBase, int offset) {
            var boneBase = External.New<IntPtr>(playerBase, offset);
            head = new BonesVector(boneBase, 0x30 * 8);
            somethingelse = new BonesVector(boneBase, 0x30 * 5);
        }
    }

    unsafe class BonesVector : BonesVector<Process> {
        public BonesVector(External<IntPtr> boneBase, int boneOffset) {
            x = External.New<float>(boneBase, boneOffset + 0x0C);
            y = External.New<float>(boneBase, boneOffset + 0x1C);
            z = External.New<float>(boneBase, boneOffset + 0x2C);
        }
    }

    unsafe class PositionVector : PositionVector<Process> {
        public PositionVector(External<IntPtr> playerBase, int positionOffset) {
            x = External.New<float>(playerBase, positionOffset + sizeof(float) * 0);
            y = External.New<float>(playerBase, positionOffset + sizeof(float) * 1);
            z = External.New<float>(playerBase, positionOffset + sizeof(float) * 2);
        }
    }

    interface External<T> : IExternal<T, Process> { }

    class ExternalFactory<T> : External<T, Process>, External<T> where T : struct {

        protected ExternalFactory(int address) : base(address) {
        }

        protected new class WithOffset : External<T, Process>.WithOffset, External<T> {

            public WithOffset(string module, int offset) : base(module, offset) {
            }

            public unsafe WithOffset(Func<IntPtr> GetBaseAddress, int offset) : base(GetBaseAddress, offset) {
            }

            public new class WithPointer : External<T, Process>.WithOffset.WithPointer, External<T> {
                public unsafe WithPointer(IExternal<IntPtr, Process> parentObject, int offset) : base(parentObject, offset) {
                }
            }
        }

        public new static External<T> New(int address) {
            return new ExternalFactory<T>(address);
        }

        public new static External<T> New(string module, int offset) {
            return new ExternalFactory<T>.WithOffset(module, offset);
        }

        public new static External<T> New(Func<IntPtr> GetBaseAddress, int offset) {
            return new ExternalFactory<T>.WithOffset(GetBaseAddress, offset);
        }

        public new static External<T> New(IExternal<IntPtr, Process> parentObject, int offset) {
            return new ExternalFactory<T>.WithOffset.WithPointer(parentObject, offset);
        }
    }

    class External {
        public static External<T> New<T>(int address) where T : struct {
            return ExternalFactory<T>.New(address);
        }

        public static External<T> New<T>(string module, int offset) where T : struct {
            return ExternalFactory<T>.New(module, offset);
        }

        public static External<T> New<T>(Func<IntPtr> GetBaseAddress, int offset) where T : struct {
            return ExternalFactory<T>.New(GetBaseAddress, offset);
        }

        public static External<T> New<T>(IExternal<IntPtr, Process> parentObject, int offset) where T : struct {
            return ExternalFactory<T>.New(parentObject, offset);
        }

        public class Array<T> : External<T, Process>.Array where T : struct {
            private Array(int length, int address, int elementSize) : base(length, address, elementSize) {
            }

            private Array(int length, string module, int offset, int elementSize) : base(length, module, offset, elementSize) {
            }

            private Array(int length, Func<IntPtr> GetBaseAddress, int offset, int elementSize) : base(length, GetBaseAddress, offset, elementSize) {
            }

            private Array(int length, IExternal<IntPtr, Process> parentObject, int offset, int elementSize) : base(length, parentObject, offset, elementSize) {
            }

            public static Array<T> New(int length, int address, int elementSize) {
                return new Array<T>(length, address, elementSize);
            }

            public static Array<T> New(int length, string module, int offset, int elementSize) {
                return new Array<T>(length, module, offset, elementSize);
            }

            public static Array<T> New(int length, Func<IntPtr> GetBaseAddress, int offset, int elementSize) {
                return new Array<T>(length, GetBaseAddress, offset, elementSize);
            }

            public static Array<T> New(int length, IExternal<IntPtr, Process> parentObject, int offset, int elementSize) {
                return new Array<T>(length, parentObject, offset, elementSize);
            }
        }

        public static Array<T> NewArray<T>(int length, int address, int elementSize) where T : struct {
            return Array<T>.New(length, address, elementSize);
        }

        public static Array<T> NewArray<T>(int length, string module, int offset, int elementSize) where T : struct {
            return Array<T>.New(length, module, offset, elementSize);
        }

        public static Array<T> NewArray<T>(int length, Func<IntPtr> GetBaseAddress, int offset, int elementSize) where T : struct {
            return Array<T>.New(length, GetBaseAddress, offset, elementSize);
        }

        public static Array<T> NewArray<T>(int length, IExternal<IntPtr, Process> parentObject, int offset, int elementSize) where T : struct {
            return Array<T>.New(length, parentObject, offset, elementSize);
        }
    }
}
