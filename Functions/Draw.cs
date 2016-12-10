using SharpDX;
using SharpDX.DirectWrite;
using SharpDX.Direct2D1;
using FontFactory = SharpDX.DirectWrite.Factory;

namespace csgop.Functions {
    class Draw {

        private static readonly FontFactory FontFactory = new FontFactory();

        public static void StringOutlined(int x, int y, string text, string fontname, float size, Color color, WindowRenderTarget device) {
            TextFormat TextFormat = new TextFormat(FontFactory, fontname, size);
            device.DrawText(text, TextFormat, new RectangleF(x + 1, y + 1, TextFormat.FontSize * text.Length, TextFormat.FontSize), new SolidColorBrush(device, Color.Black));
            device.DrawText(text, TextFormat, new RectangleF(x, y, TextFormat.FontSize * text.Length, TextFormat.FontSize), new SolidColorBrush(device, color));
        }
    }
}
