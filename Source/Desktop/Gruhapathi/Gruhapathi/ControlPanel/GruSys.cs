using Gruhapathi.ProgressBars;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gruhapathi.ControlPanel {
    public partial class GruSys : MetroForm {

        GUICore gui;


        public GruSys() {
            InitializeComponent();

            gui = new GUICore(this);
            gui.createCoreGUI(2);
            timer1.Start();
        }

        int percent = 100;

        private void timer1_Tick(object sender, EventArgs e) {
            Random ran = new Random();
            percent = ran.Next(0, 100);
            hubSignalBar.Percentage = percent;
            lblSignal.Text = percent + "%";
        }
    }
}
