using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using ForeRunners.GUI.Buttons;
using ForeRunners.Data;
using ForeRunners.Data.Model;

namespace Gruhapathi.ControlPanel {
    public partial class Summary : MetroForm {
        private GUICore gui;
        private API api;
        private ConfigurationFile iniFile;

        private ButtonLockUnlock[] lockUnlock;
        private ButtonOnOff[] onOff;

        protected string auth_token;

        private UsageData elecUsage;
        private UsageData waterUsage;

        public Summary() {
            InitializeComponent();
            try {
                gui = new GUICore(this);
                gui.createCoreGUI(6);

                init_GUI();

                iniFile = new ConfigurationFile("Gruhapathi.ini");
                api = new API(iniFile.Read("Path", "API"));
                auth_token = iniFile.Read("Token", "Authorization");

                getData();
            } catch (Exception ex) { }
        }

        private void init_GUI() {
            string[] deviceIDs = new string[] {
                "asdasd", "asdasd", "asde3df", "ds4fsadf"
            };

            lockUnlock = new ButtonLockUnlock[deviceIDs.Length];
            Point[] lockUP = new Point[] {
                new Point(246, 417), new Point(377, 417),
                new Point(510, 417), new Point(647, 417)
            };
            for (int i = 0; i < 4; i++) {
                ButtonLockUnlock butLockUnlock = lockUnlock[i];
                butLockUnlock = new ButtonLockUnlock();
                butLockUnlock.DeviceId = deviceIDs[i];
                butLockUnlock.Location = lockUP[i];
                butLockUnlock.DeviceId = deviceIDs[i];
                butLockUnlock.LockEvent = (sender2, e2) => butLock_Click(sender2, e2, butLockUnlock);
                butLockUnlock.UnlockEvent = (sender2, e2) => butUnlock_Click(sender2, e2, butLockUnlock);
                this.Controls.Add(butLockUnlock.getButton());
            }

            deviceIDs = new string[] {
                "asdasd67", "asdasd45"
            };

            onOff = new ButtonOnOff[deviceIDs.Length];
            Point[] onOffP = new Point[] {
                new Point(690, 212), new Point(690, 309)
            };
            for (int i = 0; i < 2; i++) {
                ButtonOnOff butLockUnlock = onOff[i];
                butLockUnlock = new ButtonOnOff();
                butLockUnlock.DeviceId = deviceIDs[i];
                butLockUnlock.Location = onOffP[i];
                butLockUnlock.DeviceId = deviceIDs[i];
                butLockUnlock.OnEvent = (sender2, e2) => butOn_Click(sender2, e2, butLockUnlock);
                butLockUnlock.OffEvent = (sender2, e2) => butOff_Click(sender2, e2, butLockUnlock);
                this.Controls.Add(butLockUnlock.getButton());
            }

            pnlElec.BackgroundImage = Image.FromFile("images/electricity.png");
            pnlWat.BackgroundImage = Image.FromFile("images/water.png");
            pnlSec.BackgroundImage = Image.FromFile("images/security.png");
        }

        private void getData() {
            DateTime dt = DateTime.Now;

            string date1 = string.Format("{0}-{1}-{2}", dt.Year, dt.Month, 1);
            string date2 = string.Format("{0}-{1}-{2}", dt.Year, dt.Month, dt.Day+1);

            try {
                elecUsage = api.get_UsageData(auth_token, date1, date2);
            } catch { }
            try {
                waterUsage = api.get_UsageData(auth_token, date1, date2, "W");
            } catch { }

            lblDateTime.Text = string.Format("Time Period: {0} - {1}", date1.Replace("-", "/"), date2.Replace("-", "/"));

            double price = 0, prog = 0;
            int watts = 0;
            bool forward = true;

            try {
                foreach(UsageD use in elecUsage.data) {
                    price += use.price;
                    watts += use.units;

                    if(use.progress_data.direction == "-") {
                        prog -= use.progress_data.progress;
                        forward = false;
                    } else {
                        prog += use.progress_data.progress;
                    }
                }
            } catch { }

            lblElecPrice.Text = "Rs, "+ Math.Round(price,2);
            lblWatts.Text = watts + " Units";
            lblElecProgressNum.Text = Math.Round(prog,1).ToString().Replace("-","")+"%";
            lblElecProgress.Text = (forward) ? "Decrease" : "Increase";

            price = 0; watts = 0; prog = 0; forward = true;
            try {
                foreach (UsageD use in waterUsage.data) {
                    price += use.price;
                    watts += use.units;

                    if (use.progress_data.direction == "-") {
                        prog -= use.progress_data.progress;
                        forward = false;
                    } else {
                        prog += use.progress_data.progress;
                    }
                }
            } catch { }

            lblWatPrice.Text = "Rs, " + Math.Round(price, 2);
            lblLitres.Text = watts + " Units";
            lblWatProgressNum.Text = Math.Round(prog, 1).ToString().Replace("-", "") + "%";
            lblWatProgress.Text = (forward) ? "Decrease" : "Increase";
            if (price == 0) {
                lblWatProgress.Text = "";
            }
        }

        private void butOn_Click(Object sender, MouseEventArgs e, ButtonOnOff data) {
            data.On();
            MessageBox.Show(data.DeviceId);
        }
        private void butOff_Click(Object sender, MouseEventArgs e, ButtonOnOff data) {
            data.Off();
            MessageBox.Show(data.DeviceId);
        }

        private void butLock_Click(Object sender, MouseEventArgs e, ButtonLockUnlock data) {
            data.Lock();
            MessageBox.Show(data.DeviceId);
        }
        private void butUnlock_Click(Object sender, MouseEventArgs e, ButtonLockUnlock data) {
            data.Unlock();
            MessageBox.Show(data.DeviceId);
        }

        private void label2_Click(object sender, EventArgs e) {
            gui.showHide_Form(this, new Usage());
        }

        private void pnlWat_Click(object sender, EventArgs e) {
            gui.showHide_Form(this, new Usage());
        }

        private void label25_Click(object sender, EventArgs e) {
            gui.showHide_Form(this, new Security());
        }
    }
}
