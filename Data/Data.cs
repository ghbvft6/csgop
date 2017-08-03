using System;
using CSGOP.Unmanaged;
using CSGOP.Core.Data;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword

namespace CSGOP.Data {

		class Client<BindingClass> : IClient {
			protected Player<BindingClass> player;
			protected Player<BindingClass>[] players;
			protected External<float, BindingClass>.Array view;
			public IPlayer Player {
				get => player;
				
			}
			public IPlayer[] Players {
				get => players;
				
			}
			public External.IValues<float> View {
				get => view.ValuesArray;
				
			}
		}
		interface IClient {
			IPlayer Player {
				get;
				
			}
			IPlayer[] Players {
				get;
				
			}
			External.IValues<float> View {
				get;
				
			}
		}

		class Player<BindingClass> : IPlayer {
			protected IExternal<int, BindingClass> hp;
			protected IExternal<int, BindingClass> armor;
			protected IExternal<int, BindingClass> team;
			protected IExternal<int, BindingClass> state;
			protected IExternal<int, BindingClass> consecutiveshots;
			protected IExternal<bool, BindingClass> dormant;
			protected PositionVector<BindingClass> position;
			protected Bones<BindingClass> bones;
			protected IExternal<System.IntPtr, BindingClass> activeweapon;
			protected IExternal<System.IntPtr, BindingClass> weaponbase;
			protected IExternal<int, BindingClass> weaponId;
			public int Hp {
				get => hp.Value;
				set => hp.Value = value;
			}
			public int Armor {
				get => armor.Value;
				set => armor.Value = value;
			}
			public int Team {
				get => team.Value;
				set => team.Value = value;
			}
			public int State {
				get => state.Value;
				set => state.Value = value;
			}
			public int Consecutiveshots {
				get => consecutiveshots.Value;
				set => consecutiveshots.Value = value;
			}
			public bool Dormant {
				get => dormant.Value;
				set => dormant.Value = value;
			}
			public IPositionVector Position {
				get => position;
				
			}
			public IBones Bones {
				get => bones;
				
			}
			public System.IntPtr Activeweapon {
				get => activeweapon.Value;
				set => activeweapon.Value = value;
			}
			public System.IntPtr Weaponbase {
				get => weaponbase.Value;
				set => weaponbase.Value = value;
			}
			public int WeaponId {
				get => weaponId.Value;
				set => weaponId.Value = value;
			}
		}
		interface IPlayer {
			int Hp {
				get;
				set;
			}
			int Armor {
				get;
				set;
			}
			int Team {
				get;
				set;
			}
			int State {
				get;
				set;
			}
			int Consecutiveshots {
				get;
				set;
			}
			bool Dormant {
				get;
				set;
			}
			IPositionVector Position {
				get;
				
			}
			IBones Bones {
				get;
				
			}
			System.IntPtr Activeweapon {
				get;
				set;
			}
			System.IntPtr Weaponbase {
				get;
				set;
			}
			int WeaponId {
				get;
				set;
			}
		}

		class Bones<BindingClass> : IBones {
			protected BonesVector<BindingClass> head;
			protected BonesVector<BindingClass> somethingelse;
			public IBonesVector Head {
				get => head;
				
			}
			public IBonesVector Somethingelse {
				get => somethingelse;
				
			}
		}
		interface IBones {
			IBonesVector Head {
				get;
				
			}
			IBonesVector Somethingelse {
				get;
				
			}
		}

		class BonesVector<BindingClass> : IBonesVector {
			protected IExternal<float, BindingClass> x;
			protected IExternal<float, BindingClass> y;
			protected IExternal<float, BindingClass> z;
			public float X {
				get => x.Value;
				set => x.Value = value;
			}
			public float Y {
				get => y.Value;
				set => y.Value = value;
			}
			public float Z {
				get => z.Value;
				set => z.Value = value;
			}
		}
		interface IBonesVector : CSGOP.Core.Data.IVector3 {
			float X {
				get;
				set;
			}
			float Y {
				get;
				set;
			}
			float Z {
				get;
				set;
			}
		}

		class PositionVector<BindingClass> : IPositionVector {
			protected IExternal<float, BindingClass> x;
			protected IExternal<float, BindingClass> y;
			protected IExternal<float, BindingClass> z;
			public float X {
				get => x.Value;
				set => x.Value = value;
			}
			public float Y {
				get => y.Value;
				set => y.Value = value;
			}
			public float Z {
				get => z.Value;
				set => z.Value = value;
			}
		}
		interface IPositionVector : CSGOP.Core.Data.IVector3 {
			float X {
				get;
				set;
			}
			float Y {
				get;
				set;
			}
			float Z {
				get;
				set;
			}
		}

		class Array<BindingClass> : IArray {
		}
		interface IArray {
		}
}

