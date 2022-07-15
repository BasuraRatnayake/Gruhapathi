using System;
using System.Drawing;
using System.Windows.Forms;

namespace ForeRunners.Devices.Design {
    public class WaterTap:CoreControls {

        private string uniqId;
        private PictureBox waterTap;

        private const string powerOn = @"images\water.png";
        private const string powerOff = @"images\water_h.png";
        public bool powerState { get; set; }
        public void changePowerState(bool power) {
            powerState = power;
            if (powerState)
                waterTap.Image = Image.FromFile(powerOn);
            else
                waterTap.Image = Image.FromFile(powerOff);
        }

        public int RotateState {
            get; set;
        }

        public Point waterTapLocation {
            get { return waterTap.Location; }
            set { waterTap.Location = value; }
        }

        public WaterTap(Panel mainPanel, bool power=false) {
            uniqId = "Water" + (new Random()).Next(100, 1000);
            waterTap = new PictureBox();

            this.mainPanel = mainPanel;
            this.deviceControl = waterTap;
            this.findControl = "Water";

            initContextMenu(false, true);
            changePowerState(power);

            Predefined = false;
            RotateState = 1;
        }

        public void addControl() {
            waterTap.Name = uniqId;
            waterTap.BorderStyle = BorderStyle.None;
            waterTap.Size = new Size(25, 25);
            waterTap.SizeMode = PictureBoxSizeMode.StretchImage;

            if (Predefined == false) {
                waterTap.Location = new Point(5, 5);
            }

            changePowerState(powerState);

            waterTap.MouseClick += control_Click;
            waterTap.MouseUp += control_MouseUp;
            waterTap.MouseMove += control_MouseMove;

            mainPanel.Controls.Add(waterTap);
            enableEditing();
        } 

        public override void control_Click(object sender, MouseEventArgs e) { throw new NotImplementedException(); }

        ~WaterTap() { }
    }
}
