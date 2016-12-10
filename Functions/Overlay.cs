using csgop.Imported;
using csgop.Unmanaged;
using csgop.GUI;
using System.IO;
using System.Threading;
using SharpDX;
using SharpDX.Direct2D1;
using Factory = SharpDX.Direct2D1.Factory;
using Format = SharpDX.DXGI.Format;

namespace csgop.Functions {

    class Overlay {

        private Thread DirectX = null;
        private Factory Factory = new Factory();
        private WindowRenderTarget Device;
        private HwndRenderTargetProperties RenderProperties = new HwndRenderTargetProperties();

        public static int width = ExternalProcess<External>.Width;
        public static int height = ExternalProcess<External>.Height;

        private readonly static Kernel32 kernel;

        static Overlay() {
            kernel = Kernel32.Instance;
        }

        public void Run() {
            RenderProperties.Hwnd = Form1.FormHandle;
            RenderProperties.PixelSize = new Size2(width, height);
            RenderProperties.PresentOptions = PresentOptions.None;

            Device = new WindowRenderTarget(Factory, new RenderTargetProperties(new PixelFormat(Format.B8G8R8A8_UNorm, AlphaMode.Premultiplied)), RenderProperties);

            DirectX = new Thread(new ParameterizedThreadStart(Render));
            DirectX.Priority = ThreadPriority.Highest;
            DirectX.IsBackground = true;
            DirectX.Start();
        }
   
        private void Render(object sender) {
            while (true) {
                Device.BeginDraw();
                Device.Clear(Color.Transparent);
                Device.TextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode.Aliased;

                if (kernel.GetForegroundWindow() == ExternalProcess<External>.Window) {
                    Draw.StringOutlined(5, 30, "Last modification date : " + File.GetLastWriteTime(Directory.GetCurrentDirectory()), "Tahoma", 10.0f, Color.White, Device);
                }

                Device.EndDraw();
            }
        }
    }
}
