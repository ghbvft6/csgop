using csgop.Unmanaged;
using System;

namespace csgop.Core.Data {

    unsafe class Client<BindingClass> : IClient {

        protected Player<BindingClass> player;
        protected Player<BindingClass>[] players;
        protected External<float, BindingClass>.Array view;

        IPlayer IClient.Player {
            get {
                return player;
            }
        }

        IPlayer[] IClient.Players {
            get {
                return players;
            }
        }

        External.IValues<float> IClient.View {
            get {
                return view.ValuesArray;
            }
        }
    }
}
