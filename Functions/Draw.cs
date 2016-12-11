using SharpDX;
using SharpDX.DirectWrite;
using SharpDX.Direct2D1;
using FontFactory = SharpDX.DirectWrite.Factory;
using SharpDX.Mathematics.Interop;

namespace csgop.Functions {
    class Draw {

        private static readonly FontFactory FontFactory = new FontFactory();

        public static void StringOutlined(int x, int y, string text, string fontname, float size, Color color, WindowRenderTarget device) {
            TextFormat TextFormat = new TextFormat(FontFactory, fontname, size);
            device.DrawText(text, TextFormat, new RectangleF(x + 1, y + 1, TextFormat.FontSize * text.Length, TextFormat.FontSize), new SolidColorBrush(device, Color.Black));
            device.DrawText(text, TextFormat, new RectangleF(x, y, TextFormat.FontSize * text.Length, TextFormat.FontSize), new SolidColorBrush(device, color));
        }

        public static void Circle(int x, int y, int w, Color color, WindowRenderTarget device) {
            device.DrawEllipse(new Ellipse(new Vector2(x, y), w, w), new SolidColorBrush(device, color));
        }

        public static void VerticalBar(int x, int y, int width, int height, float value, float thickness, Color color, WindowRenderTarget device) {
            RawRectangleF first = new RawRectangleF(x, y, x + width, y + height);
            device.DrawRectangle(first, new SolidColorBrush(device, Color.White), thickness);
            first.Top += height - ((float)height / 100.0f * value);
            device.FillRectangle(first, new SolidColorBrush(device, color));
        }

        public static void Rectangle(int x, int y, int w, int h, float thickness, Color color, WindowRenderTarget device) {
            device.DrawRectangle(new RectangleF(x, y, w, h), new SolidColorBrush(device, color), thickness);
        }
    }
}
