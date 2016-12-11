using csgop.Unmanaged;
using System;

namespace csgop.Core.Data {

    interface IClient {
        IPlayer Player {
            get;
        }

        IPlayer[] Players {
            get;
        }

        External<object>.IValues<float> View {
            get;
        }
    }
}
