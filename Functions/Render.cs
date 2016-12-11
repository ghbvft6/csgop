using csgop.Unmanaged;
using System.IO;
using SharpDX;

namespace csgop.Functions {
    class Render {

        private static int width = ExternalProcess<External>.Width;
        private static int height = ExternalProcess<External>.Height;

        public static int DrawAimbotRange = 1;

        public static void ModificationDate() {
            Draw.StringOutlined(5, 30, "Last modification date : " + File.GetLastWriteTime(Directory.GetCurrentDirectory()), "Tahoma", 10.0f, Color.White, Overlay.Device);
        }

        public static void AimbotRange() {
            if (DrawAimbotRange == 1) {
                Draw.Circle(width / 2, height / 2 + 26, (int)Aimbot.distancetohead, Color.White, Overlay.Device);
                Draw.StringOutlined(width / 2 - 55, height / 2 + 60, "Your current aimbot range.", "Tahoma", 10.0f, Color.White, Overlay.Device);
            }
        }
    }
}
