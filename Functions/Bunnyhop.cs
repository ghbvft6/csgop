using csgop.CSGO;
using csgop.Imported;
using System;
using System.Diagnostics;
using System.Threading;

namespace csgop.Functions {
    class Bunnyhop {

        private readonly Player player;
        private readonly CSGOClient csgo;

        public Bunnyhop(Player player, CSGOClient csgo) {
            this.player = player;
            this.csgo = csgo;
        }

        public void Run() {
            while (true) {
                if ((Kernel32.Instance.GetAsyncKeyState(0x11)) && (player.State == 257)) {
                    Console.WriteLine(csgo.View.i[0]);
                    Thread.Sleep(10);
                    Kernel32.Instance.keybd_event(0x20, 0x39, 1, 0);
                    Thread.Sleep(10);
                    Kernel32.Instance.keybd_event(0x20, 0x39, 2, 0);
                    Thread.Sleep(10);
                }
                Thread.Sleep(1);
            }
        }
    }
}
