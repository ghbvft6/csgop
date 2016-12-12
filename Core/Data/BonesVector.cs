﻿using CSGOP.Unmanaged;
using System;

namespace CSGOP.Core.Data {
    unsafe class BonesVector<BindingClass> : IBonesVector {

        protected External<float, BindingClass> x;
        protected External<float, BindingClass> y;
        protected External<float, BindingClass> z;

        float IVector3.X {
            get {
                return x.Value;
            }
        }

        float IVector3.Y {
            get {
                return y.Value;
            }
        }

        float IVector3.Z {
            get {
                return z.Value;
            }
        }
    }
}