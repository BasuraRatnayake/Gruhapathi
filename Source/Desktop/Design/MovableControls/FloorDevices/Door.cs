using System;
using System.Drawing;
using System.Windows.Forms;

namespace ForeRunners.Devices.Design {
    public class Door : CoreControls {

        private string uniqId;
        private Panel door;

        public Point doorLocation {
            get { return door.Location; }
            set { door.Location = value; }
        }
        public Size doorSize {
            get { return door.Size; }
        }

        public int RotateState {
            get; set;
        }

        public bool DoorState { get; set; }
        public void changePowerState(bool power) {
            DoorState = power;
            if (DoorState)
                door.BackColor = Color.Black;
            else
                door.BackColor = Color.Transparent;
        }

        public Door(Panel mainPanel, bool doorState=false) {
            uniqId = "Door" + (new Random()).Next(100, 1000);

            door = new Panel();

            this.mainPanel = mainPanel;
            this.deviceControl = door;
            this.findControl = "Door";

            initContextMenu(false, true);
            changePowerState(doorState);

            Predefined = false;
            RotateState = 1;
        }

        public void addControl() {
            door.Name = uniqId;
            door.Paint += Door_Paint;
            door.BringToFront();

            if (Predefined == false) {
                door.Location = new Point(10, 10);
            }

            if (RotateState == 1) {
                door.Size = new Size(30, 5);
            } else {
                if (RotateState == 2) {
                    door.Size = new Size(5, 30);
                } else if (RotateState == 3) {
                    door.Size = new Size(30, 5);
                } else if (RotateState == 4) {
                    door.Size = new Size(5, 30);
                }
            }

            changePowerState(DoorState);

            door.MouseUp += control_MouseUp;
            door.MouseMove += control_MouseMove;
            door.MouseClick += control_Click;

            mainPanel.Controls.Add(door);
            enableEditing();
        }

        public override void control_Click(object sender, MouseEventArgs e) { throw new NotImplementedException(); }
        ~Door() { }
    }
}