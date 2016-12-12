﻿using CSGOP.Imported;
using CSGOP.Unmanaged;
using CSGOP.GUI;
using CSGOP.Games.CSGO;
using CSGOP.Games.CSGO.Data;
using System.Threading;
using SharpDX;
using SharpDX.Direct2D1;
using Factory = SharpDX.Direct2D1.Factory;
using Format = SharpDX.DXGI.Format;
using CSGOP.Core.Data;

namespace CSGOP.Functions {

    class Overlay {

        private readonly IPlayer player;
        private readonly IPlayer[] players;

        public Overlay(IPlayer player, IPlayer[] players) {
            this.player = player;
            this.players = players;
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

        public void Run() {
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
                    Render.ModificationDate();
                    Render.AimbotRange();
                    Render.Run(player, players);
                    Thread.Sleep(1);
                }
                Device.EndDraw();
            }
        }
    }
}
