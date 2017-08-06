using System;
using System.Runtime.InteropServices;

namespace CSGOP.Memory {
    class Unmanaged<T> where T : struct {

        protected IntPtr ptr;

        public unsafe Unmanaged() {
            ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(T)));
            switch ((object)default(T)) {
                case byte Byte:
                    GetValue = () => (T)(object)*(byte*)ptr.ToPointer();
                    SetValue = (value) => *(byte*)ptr.ToPointer() = (byte)value;
                    break;
                case sbyte SByte:
                    GetValue = () => (T)(object)*(sbyte*)ptr.ToPointer();
                    SetValue = (value) => *(sbyte*)ptr.ToPointer() = (sbyte)value;
                    break;
                case int Int32:
                    GetValue = () => (T)(object)*(int*)ptr.ToPointer();
                    SetValue = (value) => *(int*)ptr.ToPointer() = (int)value;
                    break;
                case uint UInt32:
                    GetValue = () => (T)(object)*(uint*)ptr.ToPointer();
                    SetValue = (value) => *(uint*)ptr.ToPointer() = (uint)value;
                    break;
                case short Int16:
                    GetValue = () => (T)(object)*(short*)ptr.ToPointer();
                    SetValue = (value) => *(short*)ptr.ToPointer() = (short)value;
                    break;
                case ushort UInt16:
                    GetValue = () => (T)(object)*(ushort*)ptr.ToPointer();
                    SetValue = (value) => *(ushort*)ptr.ToPointer() = (ushort)value;
                    break;
                case long Int64:
                    GetValue = () => (T)(object)*(long*)ptr.ToPointer();
                    SetValue = (value) => *(long*)ptr.ToPointer() = (long)value;
                    break;
                case ulong UInt64:
                    GetValue = () => (T)(object)*(ulong*)ptr.ToPointer();
                    SetValue = (value) => *(ulong*)ptr.ToPointer() = (ulong)value;
                    break;
                case float Single:
                    GetValue = () => (T)(object)*(float*)ptr.ToPointer();
                    SetValue = (value) => *(float*)ptr.ToPointer() = (float)value;
                    break;
                case double Double:
                    GetValue = () => (T)(object)*(double*)ptr.ToPointer();
                    SetValue = (value) => *(double*)ptr.ToPointer() = (double)value;
                    break;
                case char Char:
                    GetValue = () => (T)(object)*(char*)ptr.ToPointer();
                    SetValue = (value) => *(char*)ptr.ToPointer() = (char)value;
                    break;
                case bool Boolean:
                    GetValue = () => (T)(object)*(bool*)ptr.ToPointer();
                    SetValue = (value) => *(bool*)ptr.ToPointer() = (bool)value;
                    break;
                case decimal Decimal:
                    GetValue = () => (T)(object)*(decimal*)ptr.ToPointer();
                    SetValue = (value) => *(decimal*)ptr.ToPointer() = (decimal)value;
                    break;
                default:
                    GetValue = () => Marshal.PtrToStructure<T>(ptr);
                    SetValue = (value) => Marshal.StructureToPtr<T>((T)value, ptr, true);
                    break;
                case null:
                    throw new ArgumentNullException(nameof(T));
            }
        }

        ~Unmanaged() {
            Marshal.FreeHGlobal(ptr);
        }

        public unsafe T Value {
            get { return GetValue(); }
            set { SetValue(value); }
        }

        private Func<T> GetValue;
        private Action<object> SetValue;

        unsafe public void* Pointer {
            get { return ptr.ToPointer(); }
        }
    }
}
