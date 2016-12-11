using csgop.CSGO;
using csgop.Unmanaged;
using System.IO;
using SharpDX;

namespace csgop.Functions {
    class Render {

        private static WorldToScreen world = new WorldToScreen(CSGOCheat.csgo.View);

        private static int width = ExternalProcess<External>.Width;
        private static int height = ExternalProcess<External>.Height;

        private static float[] boneout = new float[3];
        private static float[] positionout = new float[3];

        public static int DrawAimbotRange = 1;

        public static int DrawPlayersHealth = 1;
        public static int HealthBarStyle = 1;

        public static void ModificationDate() {
            Draw.StringOutlined(5, 30, "Last modification date : " + File.GetLastWriteTime(Directory.GetCurrentDirectory()), "Tahoma", 10.0f, Color.White, Overlay.Device);
        }

        public static void AimbotRange() {
            if (DrawAimbotRange == 1) {
                Draw.Circle(width / 2, height / 2 + 26, (int)Aimbot.distancetohead, Color.White, Overlay.Device);
                Draw.StringOutlined(width / 2 - 55, height / 2 + 60, "Your current aimbot range.", "Tahoma", 10.0f, Color.White, Overlay.Device);
            }
        }

        public static void Run(Player player, Player[] players) {
            for (int i = 0; i < players.Length; ++i) {
                if (players[i].Dormant == false && player.Team != players[i].Team && players[i].Hp > 0 && world.conversion(players[i].Bones.Head, boneout, width, height) && world.conversion(players[i].Position, positionout, width, height)) {

                    float height = System.Math.Abs(boneout[1] - positionout[1]);
                    float width = height / 2;

                    if (DrawPlayersHealth == 1) {
                        switch (HealthBarStyle) {
                            case 0:
                                Draw.VerticalBar((int)boneout[0] + ((int)height / 3), (int)boneout[1] + 25, (int)height / 20, (int)height, players[i].Hp, 1, Color.White, Overlay.Device);
                                break;
                            case 1:
                                Color healthcolor = Color.Green;
                                if (players[i].Hp > 75 && players[i].Hp < 100) { healthcolor = Color.Green; }
                                if (players[i].Hp > 50 && players[i].Hp < 75) { healthcolor = Color.Yellow; }
                                if (players[i].Hp > 25 && players[i].Hp < 50) { healthcolor = Color.DarkOrange; }
                                if (players[i].Hp > 0 && players[i].Hp < 25) { healthcolor = Color.Red; }
                                Draw.VerticalBar((int)boneout[0] + ((int)height / 3), (int)boneout[1] + 25, (int)height / 20, (int)height, players[i].Hp, 1, healthcolor, Overlay.Device);
                                break;
                        }
                    }
                }
            }
        }
    }
}
