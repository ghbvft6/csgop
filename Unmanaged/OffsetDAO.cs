using System;
using System.Reflection;

namespace csgop.Unmanaged {
    abstract class OffsetDAO {

        public OffsetDAO(int baseAddress) {
            var fieldInfos = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var fieldInfo in fieldInfos) {
                if (fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(External<>)) {
                    fieldInfo.SetValue(this, ((External<object>)fieldInfo.GetValue(this)).ExternalPointer + baseAddress);
                }
            }
        }
    }
}
