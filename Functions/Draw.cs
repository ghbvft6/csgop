using SharpDX;
using SharpDX.DirectWrite;
using SharpDX.Direct2D1;
using FontFactory = SharpDX.DirectWrite.Factory;
using SharpDX.Mathematics.Interop;

namespace CSGOP.Functions {
    class Draw {

        private static readonly FontFactory FontFactory = new FontFactory();

        public static void StringOutlined(int x, int y, string text, string fontname, float size, byte R, byte G, byte B, byte A, WindowRenderTarget device) {
            TextFormat TextFormat = new TextFormat(FontFactory, fontname, size);
            device.DrawText(text, TextFormat, new RectangleF(x + 1, y + 1, TextFormat.FontSize * text.Length, TextFormat.FontSize), new SolidColorBrush(device, Color.Black));
            device.DrawText(text, TextFormat, new RectangleF(x, y, TextFormat.FontSize * text.Length, TextFormat.FontSize), new SolidColorBrush(device, new Color { R = R, G = G, B = B, A = A }));
        }

        public static void Circle(int x, int y, int w, byte R, byte G, byte B, byte A, WindowRenderTarget device) {
            device.DrawEllipse(new Ellipse(new Vector2(x, y), w, w), new SolidColorBrush(device, new Color { R = R, G = G, B = B, A = A }));
        }

        public static void VerticalBar(int x, int y, int width, int height, float value, float thickness, byte R, byte G, byte B, byte A, WindowRenderTarget device) {
            RawRectangleF first = new RawRectangleF(x, y, x + width, y + height);
            device.DrawRectangle(first, new SolidColorBrush(device, Color.White), thickness);
            RawRectangleF second = new RawRectangleF(x, y, x - 1 + width, y - 1 + height);
            second.Top += height - ((float)height / 100.0f * value);
            device.FillRectangle(second, new SolidColorBrush(device, new Color { R = R, G = G, B = B, A = A }));
        }

        public static void HorizontalBar(int x, int y, int width, int height, float value, float thickness, byte R, byte G, byte B, byte A, WindowRenderTarget device) {
            RawRectangleF first = new RawRectangleF(x, y, x + width, y + height);
            device.DrawRectangle(first, new SolidColorBrush(device, Color.White), thickness);
            RawRectangleF second = new RawRectangleF(x, y, x - 1 + width, y - 1 + height);
            second.Right -= width - ((float)width / 100.0f * value);
            device.FillRectangle(second, new SolidColorBrush(device, new Color { R = R, G = G, B = B, A = A }));
        }

        public static void Rectangle(int x, int y, int w, int h, float thickness, byte R, byte G, byte B, byte A, WindowRenderTarget device) {
            device.DrawRectangle(new RectangleF(x, y, w, h), new SolidColorBrush(device, new Color { R = R, G = G, B = B, A = A }), thickness);
        }
    }
}
