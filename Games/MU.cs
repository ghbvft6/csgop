using CSGOP.Core;
using CSGOP.Imported;
using CSGOP.Unmanaged;
using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;

namespace CSGOP.Games.MU {

    sealed class Process : ExternalProcess<Process> {

        public Process() {
            ProcessName = "main";
        }

        public void AgilityCheat() {
            var ptr = External.New<IntPtr>("main.exe", 0x07D26AC4);
            while (ptr.Value == IntPtr.Zero) {
                Thread.Sleep(10);
            }
            var agility = External.New<ushort>(ptr, 0x1A);
            while (agility.Value == 0) {
                Thread.Sleep(10);
            }
            var agiOrig = agility.Value;
            agility.Value = 65000;
            Thread.Sleep(44500);
            agility.Value = agiOrig;
            Thread.Sleep(5000);
            agility.Value = 65000;
            while (true) {
                Thread.Sleep(55000);
                agility.Value = agiOrig;
                Thread.Sleep(5000);
                agility.Value = 65000;
            }
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
