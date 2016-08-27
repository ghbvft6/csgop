using csgop.GUI;
using csgop.Imported;
using System;
using System.Windows.Forms;

namespace csgop {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            var kernel = new Kernel32();
            kernel.AllocConsole();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
