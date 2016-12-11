using csgop.Unmanaged;
using System;

namespace csgop.Core.Data {

    unsafe class CSGOClient<BindingClass> : ICSGOClient {

        protected Player<BindingClass> player;
        protected Player<BindingClass>[] players;
        protected External<BindingClass>.Array<float> view;

        IPlayer ICSGOClient.Player {
            get {
                return player;
            }
        }

        IPlayer[] ICSGOClient.Players {
            get {
                return players;
            }
        }

        External.IValues<float> ICSGOClient.View {
            get {
                return view.ValuesArray;
            }
        }
    }
}
