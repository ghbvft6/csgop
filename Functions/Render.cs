﻿using CSGOP.Games.CSGO;
using CSGOP.Memory;
using System.IO;
using SharpDX;
using CSGOP.Data;
using CSGOP.Core;

namespace CSGOP.Functions {
    class Render {

        private static WorldToScreen world = new WorldToScreen(Games.CSGO.Process.client.View);

        private static int width = Process.Width;
        private static int height = Process.Height;

        private static float[] boneout = new float[3];
        private static float[] positionout = new float[3];

        public static int DrawAimbotRange = 1;

        public static int DrawHealthBar = 1;
        public static int HealthBarStyle = 2;
        public static byte HealthBarColorR = 255;
        public static byte HealthBarColorG = 0;
        public static byte HealthBarColorB = 80;
        public static byte HealthBarColorAlpha = 255;

        public static int DrawArmorBar = 1;
        public static int ArmorBarStyle = 1;
        public static byte ArmorBarColorR = 0;
        public static byte ArmorBarColorG = 150;
        public static byte ArmorBarColorB = 255;
        public static byte ArmorBarColorAlpha = 255;

        public static int DrawRectangle = 1;
        public static int RectangleStyle = 1;
        public static byte RectangleColorR = 255;
        public static byte RectangleColorG = 255;
        public static byte RectangleColorB = 255;
        public static byte RectangleColorAlpha = 255;

        public static void ModificationDate(SharpDX.Direct2D1.WindowRenderTarget Device) {
            Draw.StringOutlined(5, 30, "Last modification date : " + File.GetLastWriteTime(Directory.GetCurrentDirectory()), "Tahoma", 10.0f, 255, 255, 255, 255, Device);
        }

        public static void AimbotRange(SharpDX.Direct2D1.WindowRenderTarget Device) {
            if (DrawAimbotRange == 1) {
                Draw.Circle(width / 2, height / 2 + 26, (int)Aimbot.distancetohead, 255, 255, 255, 255, Device);
                Draw.StringOutlined(width / 2 - 55, height / 2 + 60, "Your current aimbot range.", "Tahoma", 10.0f, 255, 255, 255, 255, Device);
            }
        }

        public static void Run(IPlayer player, IPlayer[] players, SharpDX.Direct2D1.WindowRenderTarget Device) {
            for (int i = 0; i < players.Length; ++i) {
                if (players[i].Dormant == false && player.Team != players[i].Team && players[i].Hp > 0 && world.conversion(players[i].Bones.Head, boneout, width, height) && world.conversion(players[i].Position, positionout, width, height)) {

                    float height = System.Math.Abs(boneout[1] - positionout[1]);
                    float width = height / 2;

                    if (DrawHealthBar == 1) {

                          if (HealthBarStyle == 1 || HealthBarStyle == 3) {                     
                            if (players[i].Hp > 75 && players[i].Hp < 101) { HealthBarColorR = 154; HealthBarColorG = 205; HealthBarColorB = 50; }
                            if (players[i].Hp > 50 && players[i].Hp < 75) { HealthBarColorR = 255; HealthBarColorG = 255; HealthBarColorB = 100; }
                            if (players[i].Hp > 25 && players[i].Hp < 50) { HealthBarColorR = 255; HealthBarColorG = 165; HealthBarColorB = 0; }
                            if (players[i].Hp > 0 && players[i].Hp < 25) { HealthBarColorR = 205; HealthBarColorG = 92; HealthBarColorB = 92; }
                        }

                        switch (HealthBarStyle) {
                            case 0:
                                Draw.VerticalBar((int)boneout[0] + (int)height / 3, (int)boneout[1] + 25, 3, (int)height, players[i].Hp, 1, HealthBarColorR, HealthBarColorG, HealthBarColorB, HealthBarColorAlpha, Device);
                                break;
                            case 1:
                                Draw.VerticalBar((int)boneout[0] + (int)height / 3, (int)boneout[1] + 25, 3, (int)height, players[i].Hp, 1, HealthBarColorR, HealthBarColorG, HealthBarColorB, HealthBarColorAlpha, Device);
                                break;
                            case 2:
                                Draw.HorizontalBar((int)boneout[0] - (int)height / 4, (int)positionout[1] + 28, (int)height / 2, 4, players[i].Hp, 1, HealthBarColorR, HealthBarColorG, HealthBarColorB, HealthBarColorAlpha, Device);
                                break;
                            case 3:
                                Draw.HorizontalBar((int)boneout[0] - (int)height / 4, (int)positionout[1] + 28, (int)height / 2, 4, players[i].Hp, 1, HealthBarColorR, HealthBarColorG, HealthBarColorB, HealthBarColorAlpha, Device);
                                break;
                        }

                        if (DrawArmorBar == 1) {
                            switch (ArmorBarStyle) {
                                case 0:
                                    Draw.VerticalBar((int)boneout[0] + (int)height / 3 + 5, (int)boneout[1] + 25, 3, (int)height, players[i].Armor, 1, ArmorBarColorR, ArmorBarColorG, ArmorBarColorB, ArmorBarColorAlpha, Device);
                                    break;
                                case 1:
                                    Draw.HorizontalBar((int)boneout[0] - ((int)height / 4), (int)positionout[1] + 34, (int)height / 2, 4, players[i].Armor, 1, ArmorBarColorR, ArmorBarColorG, ArmorBarColorB, ArmorBarColorAlpha, Overlay.Device);
                                    break;
                            }
                        }

                        if (DrawRectangle == 1) {
                            switch (RectangleStyle) {
                                case 0:
                                    Draw.Rectangle((int)boneout[0] - (int)height / 4, (int)boneout[1] + 25, (int)height / 2, (int)height, 1, RectangleColorR, RectangleColorG, RectangleColorB, RectangleColorAlpha, Device);
                                    break;
                                case 1:
                                    Draw.RectangleOutlined((int)boneout[0] - (int)height / 4, (int)boneout[1] + 25, (int)height / 2, (int)height, 1, RectangleColorR, RectangleColorG, RectangleColorB, RectangleColorAlpha, Device);
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
