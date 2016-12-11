using csgop.Unmanaged;
using System;

namespace csgop.Core.Data {
    interface IPlayer {
        int Hp {
            get;
        }

        int Team {
            get;
        }

        int State {
            get;
        }

        int ConsecutiveShots {
            get;
        }

        bool Dormant {
            get;
        }

        IPositionVector Position {
            get;
        }

        IBones Bones {
            get;
        }
    }
}
