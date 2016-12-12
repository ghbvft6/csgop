using CSGOP.Core;
using CSGOP.GUI;
using CSGOP.Imported;
using System;
using System.Threading;
using System.Windows.Forms;

namespace CSGOP {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            var kernel = Kernel32.Instance;
            kernel.AllocConsole();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new Thread(new Cheat().Run).Start();
            Application.Run(new Form1());
            
        }
    }
}
