using CSGOP.Data.Interfaces;
using System;

#pragma warning disable 0649 // Field is never assigned to, and will always have its default value null

namespace CSGOP.DataTemplate {
    unsafe class Client {
        protected Player player;
        protected Player[] players;
        protected External.Array<float> view;
        protected External.Array<byte> text;
    }

    unsafe class Player {
        protected External<int> hp;
        protected External<int> armor;
        protected External<int> team;
        protected External<int> state;
        protected External<int> consecutiveshots;
        protected External<bool> dormant;
        protected PositionVector position;
        protected Bones bones;
        protected External<IntPtr> activeweapon;
        protected External<IntPtr> weaponbase;
        protected External<int> weaponId;
    }

    unsafe class Bones {
        protected BonesVector head;
        protected BonesVector somethingelse;
    }

    unsafe class BonesVector : IVector3 {
        protected External<float> x;
        protected External<float> y;
        protected External<float> z;

        public float X => throw new NotImplementedException();

        public float Y => throw new NotImplementedException();

        public float Z => throw new NotImplementedException();
    }

    unsafe class PositionVector : IVector3 {
        protected External<float> x;
        protected External<float> y;
        protected External<float> z;

        public float X => throw new NotImplementedException();

        public float Y => throw new NotImplementedException();

        public float Z => throw new NotImplementedException();
    }

    

    class External {
        public class Array<T> where T : struct {
        }
    }

    class External<T> where T : struct {
    }
}
