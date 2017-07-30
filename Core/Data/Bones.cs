using CSGOP.Unmanaged;
using System;

namespace CSGOP.Core.Data {
    unsafe class Bones<BindingClass> : IBones {

        protected BonesVector<BindingClass> head;
        protected BonesVector<BindingClass> somethingelse;

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
