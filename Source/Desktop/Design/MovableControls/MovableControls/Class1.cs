using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForeRunners.Devices.Design {
    public class Class1 : Window{

        public Class1(Panel mainPanel, bool power = false) : base(mainPanel, power) {
           
        }

        public override void control_Click(object sender, MouseEventArgs e) {
            MessageBox.Show(findControl);
        }
    }
}
