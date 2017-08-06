using CSGOP.Core;
using CSGOP.Games.CSGO;
using CSGOP.OS;
using CSGOP.Memory;
using System;
using System.Threading;
using System.Windows.Forms;

namespace CSGOP.GUI {
    public partial class Form1 : Form {

        public static IntPtr FormHandle;
        private readonly static Windows windows;

        static Form1() {
            windows = Windows.Instance;
        }

        public Form1() {
            while (Process.WindowHandle("csgo") == false || Process.WindowRect() == false) Thread.Sleep(100);
            this.ClientSize = new System.Drawing.Size(Process.Width, Process.Height);
            InitializeComponent();
            windows.SetForegroundWindow(Process.Window);
            windows.SetWindowLong(this.Handle, -20, (IntPtr)(windows.GetWindowLong(this.Handle, -20) | 0x80));
            FormHandle = this.Handle;
            Thread.Sleep(10);
        }

        protected override void OnPaint(PaintEventArgs e) {
            int[] marg = new int[] { 0, 0, Process.Width / 2, Process.Height / 2 };
            windows.DwmExtendFrameIntoClientArea(this.Handle, ref marg);
        }
    }
}
