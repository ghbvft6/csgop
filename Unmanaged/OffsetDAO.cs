using System;
using System.Reflection;
using csgop.CSGO;

namespace csgop.Unmanaged {
    abstract class OffsetDAO {

        public OffsetDAO(int baseAddress) {
            var fieldInfos = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var fieldInfo in fieldInfos) {
                if (fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(External<>)) {
                    var argumentType = fieldInfo.FieldType;
                    if (argumentType == typeof(External<int>)) {
                        ((External<int>)fieldInfo.GetValue(this)).ExternalPointer += baseAddress;
                    } else if (argumentType == typeof(External<bool>)) {
                        ((External<bool>)fieldInfo.GetValue(this)).ExternalPointer += baseAddress;
                    } else if (argumentType == typeof(External<short>)) {
                        ((External<short>)fieldInfo.GetValue(this)).ExternalPointer += baseAddress;
                    } else if (argumentType == typeof(External<Player.Vector3>)) {
                        ((External<Player.Vector3>)fieldInfo.GetValue(this)).ExternalPointer += baseAddress;
                    } else {
                        ((External<object>)fieldInfo.GetValue(this)).ExternalPointer += baseAddress; // INFO will throw an exception
                    }
                }
            }
        }
    }
}
