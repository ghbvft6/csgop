﻿using csgop.Unmanaged;
using csgop.Functions;
using System;
using System.Diagnostics;
using System.Threading;

namespace csgop.CSGO {
    class CSGOCheat {

        public static CSGOClient csgo;

        private void AttachToClient() {
            ExternalProcess<External>.ProcessName = "csgo";

            while (ExternalProcess<External>.AttachToProccess() == false) {
                Thread.Sleep(1);
            }

            foreach (ProcessModule Module in ExternalProcess<External>.Process.Modules) {
                if (Module.ModuleName.Equals("client.dll")) {
                    csgo = new CSGOClient(() => Module.BaseAddress);
                    break;
                }
            }
        }

        public void Run() {
            AttachToClient();
            new Thread(new Bunnyhop(csgo.Player).Run).Start();
            new Thread(new Aimbot(csgo.Player, csgo.Players).Run).Start();
        }
    }
}
