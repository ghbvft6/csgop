using System;
using System.Reflection;

namespace csgop.Unmanaged {
    abstract class OffsetDAO {

        // TODO - abstract factory
        private int pointerAddressOffset; // not used by public OffsetDAO(Func<IntPtr> GetBaseAddress) {
        private External<IntPtr> pointer = 0; // not used by public OffsetDAO(Func<IntPtr> GetBaseAddress) {
        private IntPtr currentBaseAddress = new IntPtr(0);

        private Func<IntPtr> GetBaseAddress;

        public OffsetDAO(int pointerAddressOffset) {
            this.pointerAddressOffset = pointerAddressOffset;
            unsafe
            {
                GetBaseAddress = () => { return *(IntPtr*)pointer.Pointer; };
            }
        }

        public OffsetDAO(Func<IntPtr> GetBaseAddress) {
            this.GetBaseAddress = GetBaseAddress;
            Update();
        }

        public unsafe void Update() {
            IntPtr newBaseAddress = GetBaseAddress();
            if (newBaseAddress != currentBaseAddress) {
                var fieldInfos = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var fieldInfo in fieldInfos) {
                    if (fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(External<>)) {
                        ((IExternal)fieldInfo.GetValue(this)).ExternalPointer += (newBaseAddress.ToInt32() - currentBaseAddress.ToInt32());
                    } else if (fieldInfo.FieldType.IsSubclassOf(typeof(OffsetDAO))) {
                        var nestedDAO = ((OffsetDAO)fieldInfo.GetValue(this));
                        nestedDAO.pointer.ExternalPointer = (newBaseAddress + nestedDAO.pointerAddressOffset);
                        nestedDAO.Update();
                    }
                }
                currentBaseAddress = newBaseAddress;
            }
        }
    }
}
