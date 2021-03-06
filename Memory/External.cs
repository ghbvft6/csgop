﻿using CSGOP.Core;
using CSGOP.Data;
using CSGOP.OS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace CSGOP.Memory {

    interface IExternal {
        IntPtr ExternalPointer { get; set; }
        void UpdateAddress();
    }

    static class ExternalExtensions {
        public static void UpdateAllAddresses(this object o) {
            var queue = new Queue<object>();
            queue.Enqueue(o);
            while (queue.Count > 0) {
                var obj = queue.Dequeue();
                if (obj is IExternal externalObj) {
                    externalObj.UpdateAddress();
                } else {

                }
                var fieldInfos = obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var fieldInfo in fieldInfos) {
                    if (fieldInfo.FieldType.IsArray) {
                        foreach (var element in fieldInfo.GetValue(obj) as object[]) {
                            if (element == null
                                || element.GetType().Namespace.Split('.')[0].Equals(typeof(Client<object>).Namespace.Split('.')[0]) == false) continue; // typeof(Cheat)
                            queue.Enqueue(element);
                        }
                    } else {
                        if (fieldInfo.GetValue(obj) == null
                            || fieldInfo.GetValue(obj).GetType().Namespace.Split('.')[0].Equals(typeof(Client<object>).Namespace.Split('.')[0]) == false // typeof(Cheat)
                            || fieldInfo.Name.Equals("parentObject")) continue;
                        queue.Enqueue(fieldInfo.GetValue(obj));
                    }
                }
            }
        }
    }

    class External {
        public interface IValues<T> where T : struct {
            T this[int i] {
                get;
            }

            int Length {
                get;
            }
        }
    }

    class External<BindingClass> {

        public static IntPtr GetModuleAddress(string module) {
            return External<BindingClass, IntPtr>.New(module, 0).ExternalPointer;
        }

        public class Array<T> where T : struct {

            private External<BindingClass, T>[] array;

            private Array(int length, int address, int elementSize) {
                array = new External<BindingClass, T>[length];
                for (var i = 0; i < length; ++i) {
                    array[i] = External<BindingClass, T>.New(address + i * elementSize);
                }
            }

            private unsafe Array(int length, string module, int offset, int elementSize) {
                array = new External<BindingClass, T>[length];
                for (var i = 0; i < length; ++i) {
                    array[i] = External<BindingClass, T>.New(module, offset + i * elementSize);
                }
            }

            private unsafe Array(int length, Func<IntPtr> GetBaseAddress, int offset, int elementSize) {
                array = new External<BindingClass, T>[length];
                for (var i = 0; i < length; ++i) {
                    array[i] = External<BindingClass, T>.New(GetBaseAddress, offset + i * elementSize);
                }
            }

            private unsafe Array(int length, IExternal<BindingClass, IntPtr> parentObject, int offset, int elementSize) {
                array = new External<BindingClass, T>[length];
                for (var i = 0; i < length; ++i) {
                    array[i] = External<BindingClass, T>.New(parentObject, offset + i * elementSize);
                }
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

            public static Array<T> New(int length, IExternal<BindingClass, IntPtr> parentObject, int offset, int elementSize) {
                return new Array<T>(length, parentObject, offset, elementSize);
            }

            public External<BindingClass, T> this[int i] {
                get {
                    return array[i];
                }
            }

            public int Length {
                get {
                    return array.Length;
                }
            }

            public Values<T> ValuesArray {
                get {
                    return new Values<T>(array);
                }
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

        public static Array<T> NewArray<T>(int length, IExternal<BindingClass, IntPtr> parentObject, int offset, int elementSize) where T : struct {
            return Array<T>.New(length, parentObject, offset, elementSize);
        }

        public class Values<T> : External.IValues<T> where T : struct {

            public External<BindingClass, T>[] array;

            public Values(External<BindingClass, T>[] array) {
                this.array = array;
            }

            public T this[int i] {
                get {
                    return array[i].Value;
                }
            }

            public int Length {
                get {
                    return array.Length;
                }
            }
        }
    }

    interface IExternal<BindingClass, T> {
        void UpdatePointingAddresses();
        unsafe void* Pointer {
            get;
        }
        T Value {
            get;
            set;
        }
        IntPtr ExternalPointer {
            get;
            set;
        }
    }

    class External<BindingClass, T> : Unmanaged<T>, IExternal<BindingClass, T> where T : struct {
        private IntPtr address;
        
        protected readonly static Kernel kernel;
        private static uint lpNumberOfBytesReadOrWritten; // used by ReadProcessMemory()

        private Action UpdateAddressDelegate;
        private Action UpdatePointingAddressesDelegate;

        static External() {
            kernel = Kernel.Instance;
        }

        private External() {
            UpdateAddressDelegate = () => { };
            UpdatePointingAddressesDelegate = () => UpdateAddressDelegate();
        }

        protected External(int address) : base() {
            this.address = new IntPtr(address);
        }

        public static External<BindingClass, T> New(int address) {
            return new External<BindingClass, T>(address);
        }

        public static External<BindingClass, T> New(string module, int offset) {
            return new External<BindingClass, T>.WithOffset(module, offset);
        }

        public static External<BindingClass, T> New(Func<IntPtr> GetBaseAddress, int offset) {
            return new External<BindingClass, T>.WithOffset(GetBaseAddress, offset);
        }

        public static External<BindingClass, T> New(IExternal<BindingClass, IntPtr> parentObject, int offset) {
            return new External<BindingClass, T>.WithOffset.WithPointer(parentObject, offset);
        }

        public new T Value {
            get {
                Read();
                return base.Value;
            }
            set {
                base.Value = value;
                Write();
            }
        }

        public unsafe new void* Pointer {
            get {
                Read();
                return base.Pointer;
            }
        }

        public IntPtr ExternalPointer {
            get { return address; }
            set { address = value; }
        }

        public void Read() {
            kernel.ReadProcessMemory(ExternalProcess<BindingClass>.PHandle, address, ptr, (uint)Marshal.SizeOf(typeof(T)), out lpNumberOfBytesReadOrWritten);
        }

        public void Write() {
            kernel.WriteProcessMemory(ExternalProcess<BindingClass>.PHandle, address, ptr, (uint)Marshal.SizeOf(typeof(T)), out lpNumberOfBytesReadOrWritten);
        }

        public void UpdateAddress() {
            UpdateAddressDelegate();
        }

        public void UpdatePointingAddresses() {
            UpdatePointingAddressesDelegate();
        }


        protected class WithOffset : External<BindingClass, T>, IExternal {

            private int offset;
            
            private WithOffset() { }

            public WithOffset(string module, int offset) {
                this.offset = offset;
                this.UpdateAddressDelegate = () => {
                    var foundClient = false;
                    while (foundClient == false) {
                        if (ExternalProcess<BindingClass>.ProcessStatic != null) {
                            foreach (ProcessModule Module in ExternalProcess<BindingClass>.ProcessStatic.Modules) {
                                if (Module.ModuleName.Equals(module)) {
                                    address = Module.BaseAddress + this.offset;
                                    foundClient = true;
                                    break;
                                }
                            }
                            Thread.Sleep(100);
                        } else break;
                    }
                };
                UpdateAddressDelegate();
            }

            public unsafe WithOffset(Func<IntPtr> GetBaseAddress, int offset) {
                // NOT USED: parentObject
                this.offset = offset;
                this.UpdateAddressDelegate = () => { address = GetBaseAddress() + this.offset; };
                UpdateAddressDelegate();
            }            


            public class WithPointer : WithOffset {

                private IExternal<BindingClass, IntPtr> parentObject;

                public unsafe WithPointer(IExternal<BindingClass, IntPtr> parentObject, int offset) {
                    this.UpdatePointingAddressesDelegate = () => {
                        parentObject.UpdatePointingAddresses();
                        UpdateAddressDelegate();
                    };
                    this.parentObject = parentObject;
                    this.offset = offset;
                    unsafe
                    {
                        UpdateAddressDelegate = () => { address = *((IntPtr*)parentObject.Pointer) + this.offset; };
                    }
                    UpdateAddressDelegate();
                }
            }
        }
    }
}
