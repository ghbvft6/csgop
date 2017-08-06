using System;
using CSGOP.Memory;

namespace CSGOP.Games.CSGO {

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

    class External : Memory.External<Process> {
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
    }
}namespace CSGOP.Games.MU {

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

    class External : Memory.External<Process> {
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
    }
}