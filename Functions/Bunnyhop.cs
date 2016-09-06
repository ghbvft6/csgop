using csgop.CSGO;
using csgop.Imported;
using System;
using System.Diagnostics;
using System.Threading;

namespace csgop.Functions {
    class Bunnyhop {
        
        private readonly Player player;
        private  m view;

        public Bunnyhop(Player player, m view) {
            this.player = player;
            this.view = view;
        }

        unsafe public void Run() {
            while (true) {
                fixed (float* i = view.i) {
                    if ((Kernel32.Instance.GetAsyncKeyState(0x11)) && (player.State == 257)) {
                        Console.WriteLine(i[0]);
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
}
