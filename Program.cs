using csgop.CSGO;
using csgop.GUI;
using csgop.Imported;
using System;
using System.Threading;
using System.Windows.Forms;

namespace csgop {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            var kernel = Kernel32.Instance;
            kernel.AllocConsole();

            new Thread(new CSGOCheat().Run).Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
