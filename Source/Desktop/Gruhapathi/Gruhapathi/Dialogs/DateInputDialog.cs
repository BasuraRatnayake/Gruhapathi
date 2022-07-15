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

namespace Gruhapathi.Dialogs {
    public partial class DateInputDialog : MetroForm {

        private GUICore gui;

        public string getSelectedDate() {
            return gui.getCalenderDate();
        }

        public DateInputDialog() {
            InitializeComponent();

            gui = new GUICore(this);

            gui.setCalender(new Point(26, 78), 2016);
            tim.Start();
        }

        private void tim_Tick(object sender, EventArgs e) {
            lblDate.Text = gui.getCalenderDate();
        }

        private void btnDate_Click(object sender, EventArgs e) {
            this.Hide();
        }
    }
}