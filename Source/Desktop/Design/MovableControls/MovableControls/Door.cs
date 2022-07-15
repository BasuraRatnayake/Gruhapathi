using System;
using System.Drawing;
using System.Windows.Forms;

namespace ForeRunners.Devices.Design {
    public class Door : CoreControls {

        private string uniqId;

        private Panel door;

        public Point doorLocation {
            get { return door.Location; }
        }
        public Size doorSize {
            get { return door.Size; }
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
        }

        public void addControl() {
            door.Name = uniqId;
            door.Size = new Size(30, 5);
            door.Paint += Door_Paint;
            door.BringToFront();
            door.Location = new Point(10, 10);

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