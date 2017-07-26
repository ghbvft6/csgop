using CSGOP.Games.CSGO;
using CSGOP.Unmanaged;
using CSGOP.Functions;
using System.Diagnostics;
using System.Threading;
using CSGOP.Core.Data;
using CSGOP.Games.CSGO.Data;
using System.Collections.Generic;
using System;

namespace CSGOP.Core {
    class Cheat {

        private static IList<ExternalProcess> games = new List<ExternalProcess>();
        private static IList<ExternalProcess> gamesRunning = new List<ExternalProcess>();

        static Cheat() {
            var csgo = new Games.CSGO.Process();
            var csgoClient = Games.CSGO.Process.client;
            csgo.AddCheat(() => { while (true) { csgoClient.UpdateAllAddresses(); Thread.Sleep(5000); } });
            csgo.AddCheat(new Bunnyhop(csgoClient.Player).Run);
            csgo.AddCheat(new Aimbot(csgoClient.Player, csgoClient.Players).Run);
            csgo.AddCheat(new Overlay(csgoClient.Player, csgoClient.Players).Run);
            games.Add(csgo);

            var mu = new Games.MU.Process();
            mu.AddCheat(() => { mu.AgilityCheat(); });           
            games.Add(mu);
        }

        public void MonitorGames() {
            while (true) {
                for (var i = 0; i < games.Count; ++i) {
                    if (games[i].AttachToProccess()) {
                        Console.WriteLine("Attached to " + games[i].Process.Id);
                        games[i].SetClientBaseAddress();
                        foreach (var cheat in games[i].cheats) {
                            var t = new Thread(() => cheat());
                            games[i].cheatsThreads.Add(t);
                            t.Start();
                        }
                        gamesRunning.Add(games[i]);
                        games.RemoveAt(i);
                    }
                    Thread.Sleep(100);
                }
                for (var i = 0; i < gamesRunning.Count; ++i) {
                    try {
                        var testproc = System.Diagnostics.Process.GetProcessById(gamesRunning[i].Process.Id);
                    } catch (ArgumentException e) {
                        Console.WriteLine("Deattached from " + gamesRunning[i].Process.Id);
                        foreach (var cheat in gamesRunning[i].cheatsThreads) {
                            cheat.Abort();
                        }
                        gamesRunning[i].cheatsThreads.Clear();
                        games.Add(gamesRunning[i]);
                        gamesRunning.RemoveAt(i);
                    }
                    Thread.Sleep(100);
                }
            }
        }

        public void Run() {
            MonitorGames();
        }
    }
}
