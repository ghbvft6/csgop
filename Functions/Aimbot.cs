﻿using CSGOP.Games.CSGO;
using CSGOP.Games.CSGO.Data;
using CSGOP.Imported;
using System.Threading;
using System;
using CSGOP.Unmanaged;
using CSGOP.Core.Data;
using CSGOP.Core;

namespace CSGOP.Functions {
    class Aimbot {

        private readonly IPlayer player;
        private readonly IPlayer[] players;

        public Aimbot(IPlayer player, IPlayer[] players) {
            this.player = player;
            this.players = players;
        }

        WorldToScreen world = new WorldToScreen(Games.CSGO.Process.client.View);

        float[] boneout = new float[3];

        bool automatic = false;

        public static int width = 1280, height = 720, correctionx = 0, correctiony = 0;

        public static float distancetohead = 30.0f, smooth = 1.5f, recoil = 0, distance = 0, x = 0, y = 0;

        private void recoilsystem() {
            recoil = 0;
            recoil = player.ConsecutiveShots * 3.0f;
            if (recoil > 35.0f) { recoil = 35.0f; }
        }

        private void smoothsystem() {
            smooth = 15.0f * distance / distancetohead;
            if (smooth < 3.0f) { smooth = 3.0f; }
        }

        private void normalize() {
            if (x > 89.0f) { x = 89.0f; }
            if (x < -89.0f) { x = -89.0f; }
            if (y > 180.0f) { y = 360.0f; }
            if (y < -180.0f) { y = -360.0f; }
        }

        public void Run() {
            while (true) {
                if (automatic == true || Kernel32.Instance.GetAsyncKeyState(02)) {
                    for (int i = 0; i < players.Length; ++i) {
                        if (players[i].Dormant == false && player.Team != players[i].Team && players[i].Hp > 0 && world.conversion(players[i].Bones.Head, boneout, width, height)) {
                            distance = Algebra.distance(boneout[0], boneout[1], width / 2, height / 2);
                            float mydistance = distancetohead;
                            if (smooth > 1.0f && distance > 0.1f && distance < mydistance) {
                                i = players.Length + 1;
                                recoilsystem(); smoothsystem();
                                x = (boneout[0] - width / 2) / smooth;
                                y = (boneout[1] - height / 2 - -recoil) / smooth;
                                normalize();
                                Kernel32.Instance.mouse_event(01, (uint)x, (uint)y, 0, 0);
                            }
                        }
                    }
                }
                Thread.Sleep(5);
            }
        }
    }
}