using System;
using System.Drawing;
using System.Windows.Forms;

namespace ForeRunners.Devices.Design {
    public class PowerOutlet : CoreControls {

        private string uniqId;
        private PictureBox powerOutlet;

        private const string powerOn = @"images\power_on.png";
        private const string powerOff = @"images\power_off.png";

        public bool powerState { get; set; }

        public void changePowerState(bool power) {
            powerState = power;
            if (power)
                powerOutlet.Image = Image.FromFile(powerOn);
            else
                powerOutlet.Image = Image.FromFile(powerOff);
        }

        public int RotateState {
            get; set;
        }

        public Point powerOutletLocation {
            get { return powerOutlet.Location; }
            set { powerOutlet.Location = value; }
        }

        public PowerOutlet(Panel mainPanel, bool power = false) {
            uniqId = "Power" + (new Random()).Next(100, 1000);
            powerOutlet = new PictureBox();

            this.mainPanel = mainPanel;
            this.deviceControl = powerOutlet;
            this.findControl = "Power";

            initContextMenu();
            changePowerState(power);

            Predefined = false;
            RotateState = 1;
        }

        public void addControl() {
            powerOutlet.Name = "Power";
            powerOutlet.Size = new Size(23, 21);
            powerOutlet.Image = Image.FromFile(powerOff);

            if (Predefined == false) {
                powerOutlet.Location = new Point(5, 5);
            }

            changePowerState(powerState);

            powerOutlet.MouseClick += control_Click;
            powerOutlet.MouseUp += control_MouseUp;
            powerOutlet.MouseMove += control_MouseMove;

            mainPanel.Controls.Add(powerOutlet);
            enableEditing();
        }

        public override void control_Click(object sender, MouseEventArgs e) { throw new NotImplementedException(); }
        ~PowerOutlet() { }
    }
}