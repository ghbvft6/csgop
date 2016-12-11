using csgop.Games.CSGO;
using csgop.Imported;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace csgop.Unmanaged {

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
                }
                var fieldInfos = obj.GetType().GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var fieldInfo in fieldInfos) {
                    if (fieldInfo.FieldType.IsArray) {
                        foreach (var element in fieldInfo.GetValue(obj) as object[]) {
                            if (element == null) continue;
                            if (element.GetType().IsGenericType && element.GetType().GetGenericTypeDefinition() == typeof(AbstractExternal<,>)) continue;
                            queue.Enqueue(element);
                        }
                    } else {
                        if (fieldInfo.GetValue(obj) == null) continue;
                        if (fieldInfo.GetValue(obj).GetType().IsGenericType && fieldInfo.GetValue(obj).GetType().GetGenericTypeDefinition() == typeof(AbstractExternal<,>)) continue;
                        queue.Enqueue(fieldInfo.GetValue(obj));
                    }
                }
            }
        }
    }

    class AbstractExternal<BindingClass> {

        /* start OF ExternalProcess */
        private readonly static Kernel32 kernel;
        private static IntPtr pHandle;
        private static IntPtr window;
        private static int width;
        private static int height;
        private static Process process;
        private static string processName;

        static AbstractExternal() {
            kernel = Kernel32.Instance;
        }

        public static IntPtr PHandle {
            get { return pHandle; }
            set { pHandle = value; }
        }

        public static IntPtr Window {
            get { return window; }
            set { window = value; }
        }

        public static int Width {
            get { return width; }
            set { width = value; }
        }

        public static int Height {
            get { return height; }
            set { height = value; }
        }

        public static Process Process {
            get { return process; }
            set {
                process = value;
                ProcessName = process.ProcessName;
            }
        }

        public static string ProcessName {
            get { return processName; }
            set { processName = value; }
        }

        public static bool AttachToProccess() {
            var processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0) {
                process = processes[0];
            }
            return AttachToProccess(process);
        }

        public static bool AttachToProccess(Process process) {
            if (process != null) {
                pHandle = kernel.OpenProcess(0x0010, false, process.Id);
            }
            return pHandle == IntPtr.Zero ? false : true;
        }

        public static bool WindowHandle(string process) {
            var processes = Process.GetProcessesByName(process);
            if (processes.Length > 0) {
                window = processes[0].MainWindowHandle;
            }
            return window == IntPtr.Zero ? false : true;
        }

        public static bool WindowRect() {
            Kernel32.RECT WindowSize = new Kernel32.RECT();
            if (kernel.GetClientRect(External.Window, out WindowSize)) {
                width = WindowSize.Right - WindowSize.Left;
                height = WindowSize.Bottom - WindowSize.Top;
                return true;
            }
            return false;
        }
        /* END OF ExternalProcess */



        public class Array<T> where T : struct {

            private AbstractExternal<T, BindingClass>[] array;

            public Array(int length, int address, int elementSize) {
                array = new AbstractExternal<T, BindingClass>[length];
                for (var i = 0; i < length; ++i) {
                    array[i] = new AbstractExternal<T, BindingClass>(address + i * elementSize);
                }
            }

            public unsafe Array(int length, Func<IntPtr> GetBaseAddress, int offset, int elementSize) {
                array = new AbstractExternal<T, BindingClass>[length];
                for (var i = 0; i < length; ++i) {
                    array[i] = new AbstractExternal<T, BindingClass>(GetBaseAddress, offset + i * elementSize);
                }
            }

            public unsafe Array(int length, AbstractExternal<IntPtr, BindingClass> parentObject, int offset, int elementSize) {
                array = new AbstractExternal<T, BindingClass>[length];
                for (var i = 0; i < length; ++i) {
                    array[i] = new AbstractExternal<T, BindingClass>(parentObject, offset + i * elementSize);
                }
            }

            public AbstractExternal<T, BindingClass> this[int i] {
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

        public class Values<T> where T : struct {

            public AbstractExternal<T, BindingClass>[] array;

            public Values(AbstractExternal<T, BindingClass>[] array) {
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

        public static T[] NewArray<T>(int length, int address, int elementSize) where T : AbstractExternal<IntPtr, BindingClass> {
            var array = new T[length];
            ConstructorInfo constructorInfo = typeof(T).GetConstructor(new[] { typeof(int) });
            for (var i = 0; i < length; ++i) {
                array[i] = (T)constructorInfo.Invoke(new object[] { address + i * elementSize });
            }
            return array;
        }

        public static T[] NewArray<T>(int length, Func<IntPtr> GetBaseAddress, int offset, int elementSize) where T : AbstractExternal<IntPtr, BindingClass> {
            var array = new T[length];
            ConstructorInfo constructorInfo = typeof(T).GetConstructor(new[] { typeof(Func<IntPtr>), typeof(int) });
            for (var i = 0; i < length; ++i) {
                array[i] = (T)constructorInfo.Invoke(new object[] { GetBaseAddress, offset + i * elementSize });
            }
            return array;
        }

        public static T[] NewArray<T>(int length, AbstractExternal<IntPtr, BindingClass> parentObject, int offset, int elementSize) where T : AbstractExternal<IntPtr, BindingClass> {
            var array = new T[length];
            ConstructorInfo constructorInfo = typeof(T).GetConstructor(new[] { typeof(AbstractExternal<IntPtr, BindingClass>), typeof(int)});
            for (var i = 0; i < length; ++i) {
                array[i] = (T)constructorInfo.Invoke(new object[] { parentObject, offset + i * elementSize });
            }
            return array;
        }

        // TODO move
        private static T[] NewArray<T>(int length, Func<int, T> constructor) {
            var array = new T[length];
            for (var i = 0; i < length; ++i) {
                array[i] = constructor(i);
            }
            return array;
        }
    }

    class AbstractExternal<T, BindingClass> : Unmanaged<T>, IExternal where T : struct {

        private AbstractExternal<IntPtr, BindingClass> parentObject;
        private IntPtr address;
        private int offset;
        protected readonly static Kernel32 kernel;
        private static uint lpNumberOfBytesReadOrWritten; // used by ReadProcessMemory()

        private Action UpdateAddressDelegate;

        static AbstractExternal() {
            kernel = Kernel32.Instance;
        }

        public AbstractExternal(int address) {
            // NOT USED: parentObject, offset, UpdateAddress
            this.address = new IntPtr(address);
            this.UpdateAddressDelegate = () => { }; // not null
        }

        public unsafe AbstractExternal(Func<IntPtr> GetBaseAddress, int offset) {
            // NOT USED: parentObject
            this.offset = offset;
            this.UpdateAddressDelegate = () => { address = GetBaseAddress() + this.offset; };
            UpdateAddressDelegate();
        }

        public unsafe AbstractExternal(AbstractExternal<IntPtr, BindingClass> parentObject, int offset) {
            this.parentObject = parentObject;
            this.offset = offset;
            unsafe
            {
                UpdateAddressDelegate = () => { address = *((IntPtr*)parentObject.Pointer) + this.offset; };
            }
            UpdateAddressDelegate();
        }

        public void UpdatePointingAddresses() {
            if (parentObject != null) {
                parentObject.UpdatePointingAddresses();
                UpdateAddressDelegate();
            } else {
                UpdateAddressDelegate();
            }
        }

        public void UpdateAddress() {
            UpdateAddressDelegate();
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
            kernel.ReadProcessMemory(AbstractExternal<BindingClass>.PHandle, address, ptr, (uint)Marshal.SizeOf(typeof(T)), out lpNumberOfBytesReadOrWritten);
        }

        public void Write() {
            kernel.WriteProcessMemory(AbstractExternal<BindingClass>.PHandle, address, ptr, (uint)Marshal.SizeOf(typeof(T)), out lpNumberOfBytesReadOrWritten);
        }
    }
}
