using csgop.CSGO;
using System.Threading;
using System.Windows.Forms;

namespace csgop.GUI {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e) {
            new Thread(new CSGOCheat().Run).Start();
        }
    }
}
