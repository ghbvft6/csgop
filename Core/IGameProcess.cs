using CSGOP.Core.Data;
using CSGOP.Unmanaged;
using System;
using System.Threading;

namespace CSGOP.Core {
    unsafe interface IGameProcess { 
        IClient Client {
            get;
        }
        void MonitorClient();
        void AddCheat(Thread cheat);
    }
}