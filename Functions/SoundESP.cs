using csgop.CSGO;
using csgop.Functions;
using csgop.Imported;
using System;
using System.Diagnostics;
using System.Threading;


namespace csgop.Functions
{
    class SoundESP
    {
        public void Run()
        {
            while (true)
            {
                for (int i = 0; i < CSGOCheat.players.Length; ++i)
                {
                    if ((!CSGOCheat.players[i].Dormant) && (CSGOCheat.player.Team != CSGOCheat.players[i].Team) && (CSGOCheat.players[i].Hp > 0))
                    {
                        float distance = Algebra.distance(CSGOCheat.player.Position.x, CSGOCheat.player.Position.y, CSGOCheat.players[i].Position.x, CSGOCheat.players[i].Position.y);
                        Console.Beep(300, 150);
                        Thread.Sleep((int)(distance / 3));
                    }
                }
                Thread.Sleep(1);
            }
        }
    }
}


