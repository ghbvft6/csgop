using System;
using System.Reflection;
using csgop.CSGO;

namespace csgop.Unmanaged {
    abstract class OffsetDAO {

        public OffsetDAO(int baseAddress) {
            var fieldInfos = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var fieldInfo in fieldInfos) {
                if (fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(External<>)) {
                    ((IExternal)fieldInfo.GetValue(this)).ExternalPointer += baseAddress; // INFO will throw an exception
                }
            }
        }
    }
}
