using csgop.CSGO;
using csgop.Functions;
using System.Threading;
using System.Windows.Forms;

namespace csgop.GUI {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e) {
            CSGOCheat.AttachToClient();
            new Thread(new Bunnyhop().Run).Start();
        }
    }
}
