﻿using CSGOP.Core;
using CSGOP.Games.CSGO;
using CSGOP.Games.CSGO.Data;
using CSGOP.Imported;
using CSGOP.Unmanaged;
using System;
using System.Threading;
using System.Windows.Forms;

namespace CSGOP.GUI {
    public partial class Form1 : Form {

        public static IntPtr FormHandle;
        private readonly static Kernel32 kernel;

        static Form1() {
            kernel = Kernel32.Instance;
        }

        public Form1() {
            if (Process.WindowHandle("csgo") == true && Process.WindowRect() == true) {
                kernel.SetForegroundWindow(Process.Window);
                this.ClientSize = new System.Drawing.Size(Process.Width, Process.Height);
                InitializeComponent();
                FormHandle = this.Handle;
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            new Thread(new Cheat().Run).Start();
        }

        protected override void OnPaint(PaintEventArgs e) {
            int[] marg = new int[] { 0, 0, Process.Width / 2, Process.Height / 2 };
            kernel.DwmExtendFrameIntoClientArea(this.Handle, ref marg);
        }
    }
}
