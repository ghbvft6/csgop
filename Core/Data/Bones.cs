using CSGOP.Unmanaged;
using System;

namespace CSGOP.Core.Data {
    unsafe class Bones<BindingClass> : External<IntPtr, BindingClass>, IBones {

        protected BonesVector<BindingClass> head;
        protected BonesVector<BindingClass> somethingelse;

        public Bones(int address) : base(address) {
        }

        public Bones(External<IntPtr, BindingClass> parentObject, int offset) : base(parentObject, offset) {
        }

        public Bones(Func<IntPtr> GetBaseAddress, int offset) : base(GetBaseAddress, offset) {
        }

        IBonesVector IBones.Head {
            get {
                return head;
            }
        }

        IBonesVector IBones.Somethingelse {
            get {
                return somethingelse;
            }
        }
    }
}
