﻿using CSGOP.Games.CSGO;
using CSGOP.Memory;
using CSGOP.Functions;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System;

namespace CSGOP.Core {
    class Cheat {

        private static IList<ExternalProcess> games = new List<ExternalProcess>();
        private static IList<ExternalProcess> gamesRunning = new List<ExternalProcess>();

        static Cheat() {
            /*var csgo = new Games.CSGO.Process();
            csgo.AddCheat(() => { while (true) { Games.CSGO.Process.client.UpdateAllAddresses(); Thread.Sleep(5000); } });
            csgo.AddCheat<Bunnyhop>();
            csgo.AddCheat<Aimbot>();
            csgo.AddCheat<Overlay>();
            //games.Add(csgo);

            var mu = new Games.MU.Process();
            mu.AddCheat(() => { mu.AttackSpeedCheat(); });           
            games.Add(mu);*/

            var notepad = new Games.Notepad.Process();
            notepad.AddCheat<TextReader>();
            games.Add(notepad);

            var wordpad = new Games.Wordpad.Process();
            wordpad.AddCheat<TextReader>();
            games.Add(wordpad);
        }

        public void MonitorGames() {
            while (true) {
                for (var i = 0; i < games.Count; ++i) {
                    if (games[i].AttachToProccess()) {
                        gamesRunning.Add(games[i]);
                        games.RemoveAt(i);
                    }
                    Thread.Sleep(100);
                }
                for (var i = 0; i < gamesRunning.Count; ++i) {
                    try {
                        var testproc = System.Diagnostics.Process.GetProcessById(gamesRunning[i].Process.Id);
                    } catch (ArgumentException e) {
                        gamesRunning[i].DeattachFromProccess();
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
