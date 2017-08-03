using CSGOP.Imported;
using CSGOP.Unmanaged;
using CSGOP.GUI;
using CSGOP.Games.CSGO;
using System.Threading;
using SharpDX;
using SharpDX.Direct2D1;
using Factory = SharpDX.Direct2D1.Factory;
using Format = SharpDX.DXGI.Format;
using CSGOP.Data;
using System;
using csgop.Functions;

namespace CSGOP.Functions {

    class Overlay : CheatFunction {

        private readonly IPlayer player;
        private readonly IPlayer[] players;

        public Overlay(IClient client) : base(client) {
            this.player = client.Player;
            this.players = client.Players;
        }

        private Thread DirectX = null;
        private Factory Factory = new Factory();
        public static WindowRenderTarget Device;
        private HwndRenderTargetProperties RenderProperties = new HwndRenderTargetProperties();

        public static int Antialiasing = 0;
        public static int width = Process.Width;
        public static int height = Process.Height;

        private readonly static Kernel32 kernel;

        static Overlay() {
            kernel = Kernel32.Instance;
        }

        public override void Run() {
            while (Form1.FormHandle == IntPtr.Zero) Thread.Sleep(100);
            width = Process.Width;
            height = Process.Height;
            RenderProperties.Hwnd = Form1.FormHandle;
            RenderProperties.PixelSize = new Size2(width, height);
            RenderProperties.PresentOptions = PresentOptions.None;

            Device = new WindowRenderTarget(Factory, new RenderTargetProperties(new PixelFormat(Format.B8G8R8A8_UNorm, AlphaMode.Premultiplied)), RenderProperties);

            DirectX = new Thread(new ParameterizedThreadStart(Run));
            DirectX.Priority = ThreadPriority.Highest;
            DirectX.IsBackground = true;
            DirectX.Start();
        }

        private void Antialias() {
            switch (Antialiasing) {
                case 0: Device.AntialiasMode = AntialiasMode.Aliased; break;
                case 1: Device.AntialiasMode = AntialiasMode.PerPrimitive; break;
            }
        }

        private void Run(object sender) {
            while (true) {
                Device.BeginDraw();
                Device.Clear(Color.Transparent);
                Device.TextAntialiasMode = TextAntialiasMode.Aliased;
                Antialias();
                if (kernel.GetForegroundWindow() == Process.Window) {
                    Render.ModificationDate(Device);
                    Render.AimbotRange(Device);
                    Render.Run(player, players, Device);
                    Thread.Sleep(1);
                }
                Device.EndDraw();
            }
        }
    }
}
