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
            if (powerState)
                powerOutlet.Image = Image.FromFile(powerOn);
            else
                powerOutlet.Image = Image.FromFile(powerOff);
        }

        public Point powerOutletLocation {
            get { return powerOutlet.Location; }
        }

        public PowerOutlet(Panel mainPanel, bool power = false) {
            uniqId = "Power" + (new Random()).Next(100, 1000);
            powerOutlet = new PictureBox();

            this.mainPanel = mainPanel;
            this.deviceControl = powerOutlet;
            this.findControl = "Power";

            initContextMenu(false, true);

            changePowerState(power);
        }

        public void addControl() {
            powerOutlet.Name = "Power";
            powerOutlet.Size = new Size(23, 21);
            powerOutlet.Image = Image.FromFile(powerOff);
            powerOutlet.Location = new Point(5, 5);

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