using CSGOP.Unmanaged;
using System;

namespace CSGOP.Core.Data {

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
}
