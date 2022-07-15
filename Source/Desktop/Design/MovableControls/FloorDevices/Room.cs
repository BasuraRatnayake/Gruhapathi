using System;
using System.Drawing;
using System.Windows.Forms;

namespace ForeRunners.Devices.Design {
    public class Room : CoreControls {
        private int roomNo;

        private Panel pnlRoom;
        public Point roomLocation {
            get { return pnlRoom.Location; }
            set { pnlRoom.Location = value; }
        }
        public Size roomSize {
            get { return pnlRoom.Size; }
            set { pnlRoom.Size = value; }
        }

        public int RotateState {
            get; set;
        }

        private Panel roomDoor;
        public Point doorLocation {
            get { return roomDoor.Location; }
        }
        public Size doorSize {
            get { return roomDoor.Size; }
        }
        public bool DoorState { get; set; }
        public void changeDoorPowerState(bool power) {
            DoorState = power;
            if (DoorState)
                roomDoor.BackColor = Color.Black;
            else
                roomDoor.BackColor = Color.Transparent;
        }

        public bool RoomState { get; set; }
        public void changeRoomPowerState(bool power) {
            RoomState = power;
        }

        private string uniqId;

        private Label lblRoomName;

        public Room(Panel mainPanel, string roomName = null, bool power = false) {
            uniqId = "pnlRoom" + (new Random()).Next(100, 1000);

            pnlRoom = new Panel();
            roomDoor = new Panel();
            lblRoomName = new Label();

            this.mainPanel = mainPanel;
            this.deviceControl = pnlRoom;
            this.findControl = "Room";

            this.DeviceName = "Room " + roomNo;

            if (roomName != null)
                this.DeviceName = roomName;

            changeDoorPowerState(power);
            initContextMenu(true, true);

            Predefined = false;
            RotateState = 1;
        }

        public void addControl() {
            lblRoomName.Name = "pnlLabel";
            lblRoomName.Text = DeviceName;
            lblRoomName.Dock = DockStyle.Fill;
            lblRoomName.BackColor = Color.Transparent;
            lblRoomName.ForeColor = Color.White;
            lblRoomName.SendToBack();
            lblRoomName.TextAlign = ContentAlignment.MiddleCenter;

            roomDoor.Name = "pnlDoor";
            roomDoor.Size = new Size(30, 5);
            roomDoor.Paint += Door_Paint;
            roomDoor.BringToFront();

            pnlRoom.Name = uniqId;

            if (Predefined == false) {
                pnlRoom.Location = new Point(0, 0);
                pnlRoom.Size = new Size(100, 100);
            }

            changeDoorPowerState(DoorState);

            pnlRoom.Controls.Add(roomDoor);
            pnlRoom.Controls.Add(lblRoomName);
            pnlRoom.Paint += DeviceControl_Paint;

            if (RotateState == 1) {
                roomDoor.Location = DoorLocation(pnlRoom, roomDoor);
            } else {
                if (RotateState == 2) {
                    roomDoor.Size = new Size(5, 30);
                    roomDoor.Location = new Point(-1, deviceControl.Height / 2 - roomDoor.Height / 2);
                } else if (RotateState == 3) {
                    roomDoor.Size = new Size(30, 5);
                    roomDoor.Location = new Point(deviceControl.Width / 2 - roomDoor.Width / 2, -1);
                } else if (RotateState == 4) {
                    roomDoor.Size = new Size(5, 30);
                    roomDoor.Location = new Point(deviceControl.Width - 5, deviceControl.Height / 2 - roomDoor.Height / 2);
                }
            }

            lblRoomName.MouseUp += control_MouseUp;
            lblRoomName.MouseMove += control_MouseMove;
            lblRoomName.MouseClick += control_Click;

            pnlRoom.BringToFront();
            mainPanel.Controls.Add(pnlRoom);
            enableEditing();

            roomNo++;
        }

        public override void control_Click(object sender, MouseEventArgs e) { throw new NotImplementedException(); }
        ~Room() { }
    }
}