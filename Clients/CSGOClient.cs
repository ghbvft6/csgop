using csgop.Unmanaged;
using System.Diagnostics;
using System.Reflection;

namespace csgop.Clients {
    unsafe class CSGOClient {

        readonly External<int> hp = 10;
        readonly External<bool> isWalking = 102;

        internal int Hp {
            get {
                return *(int*)hp.Pointer;
            }
        }

        internal bool IsWalking {
            get {
                return *(bool*)isWalking.Pointer;
            }
        }

        public void Run() {
            External.ProcessName = "csgop";
            External.AttachToProccess();

            foreach (ProcessModule Module in External.Process.Modules) {
                if (Module.ModuleName.Equals("client.dll")) {
                    var client = (int)Module.BaseAddress;
                    FieldInfo[] fieldInfos;
                    fieldInfos = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                    foreach (var fieldInfo in fieldInfos) {
                        if (fieldInfo.FieldType == typeof(External<>)) {
                            fieldInfo.SetValue(this, fieldInfo.GetValue(this));
                        }
                    }
                    break;
                }
            }

            if (Hp > 0) {
            }
            if (IsWalking) {

            }
        }
    }
}
