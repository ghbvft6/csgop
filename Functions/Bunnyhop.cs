using CSGOP.Core.Data;
using CSGOP.Games.CSGO.Data;
using CSGOP.Imported;
using System.Threading;

namespace CSGOP.Functions {
    class Bunnyhop {
        
        private readonly IPlayer player;

        public Bunnyhop(IPlayer player) {
            this.player = player;
        }

        public void Run() {
            while (true) {
                if (Kernel32.Instance.GetAsyncKeyState(0x11) && player.State == 257) {
                    Thread.Sleep(10);
                    Kernel32.Instance.mouse_event(0x800, 0, 0, 120, 0);
                    Thread.Sleep(10);
                }
                Thread.Sleep(1);
            }
        }
    }
}
