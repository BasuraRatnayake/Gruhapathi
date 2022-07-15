using System;
using System.Drawing;
using System.Windows.Forms;

namespace ForeRunners.Devices.Design {
    public class Light : CoreControls {

        private string uniqId;
        private PictureBox light;

        private const string powerOn = @"images\light_on.png";
        private const string powerOff = @"images\light_off.png";

        public bool powerState { get; set; }
        public void changePowerState(bool power) {
            powerState = power;
            if (powerState)
                light.Image = Image.FromFile(powerOn);
            else
                light.Image = Image.FromFile(powerOff);
        }

        public Point lightLocation {
            get { return light.Location; }
        }

        public Light(Panel mainPanel, bool power = false) {
            uniqId = "Light" + (new Random()).Next(100, 1000);
            light = new PictureBox();

            this.mainPanel = mainPanel;
            this.deviceControl = light;
            this.findControl = "Light";

            initContextMenu(false, true);

            changePowerState(power);
        }

        public  void addControl() {
            light.Name = uniqId;
            light.Size = new Size(19, 25);

            light.Location = new Point(5, 5);

            light.MouseClick += control_Click;
            light.MouseUp += control_MouseUp;
            light.MouseMove += control_MouseMove;

            mainPanel.Controls.Add(light);
            enableEditing();
        }

        public override void control_Click(object sender, MouseEventArgs e) { throw new NotImplementedException(); }
        ~Light() { }
    }
}