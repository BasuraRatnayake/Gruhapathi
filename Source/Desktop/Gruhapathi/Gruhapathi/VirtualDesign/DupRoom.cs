using ForeRunners.Devices.Design;
using Gruhapathi.Dialogs;
using MetroFramework.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;
using ForeRunners.Data;
using ForeRunners.Data.Model;
using MetroFramework;
using Gruhapathi.ControlPanel;

namespace Gruhapathi.VirtualDesign {
    public class DupRoom: Room {
        private MetroForm owner;
        private RoomDialog roomDialog;

        GUICore core;

        public API api { get; set; }

        public string auth_token { get; set; }
        public string RoomId { get; set; }
        public string FloorId { get; set; }

        public DupRoom(Panel mainPanel, MetroForm owner, string roomName = null, bool power = false) : base(mainPanel, roomName, power) {
            this.owner = owner;

            core = new GUICore(owner);

            roomDialog = new RoomDialog(owner, new Point(0, 0), Off_Click, On_Click, Unlock_Click, Lock_Click, More_Click);
            roomDialog.OwnerPanel = "pnlFloorArea";

            if (power) {
                roomDialog.getLockUnlock.Lock();
            } else {
                roomDialog.getLockUnlock.Unlock();
            }
        }

        public override void control_Click(object sender, MouseEventArgs e) {
            roomDialog.DialogLocation = ((Control)sender).Parent.Location;
            roomDialog.DialogDesc = DeviceDescription;
            roomDialog.DialogTitle = DeviceName;
            roomDialog.showDialog();            
        }

        public override void deleteControl() {
            if(api.removeRoom(auth_token, int.Parse(FloorId), int.Parse(RoomId))) {
                MessageBox.Show("Remove Successfully Removed.", "Gruhapathi Control Panel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else {
                MessageBox.Show("An Unknown Error Occurred.", "Gruhapathi Control Panel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void More_Click(object sender, MouseEventArgs e) {
            Rooms rooms = new Rooms(true, int.Parse(RoomId), int.Parse(FloorId), DeviceName);
            core.showHide_Form(rooms, owner);
        }

        private void Lock_Click(object sender, MouseEventArgs e) {
            roomDialog.getLockUnlock.Lock();
            changeDoorPowerState(true);

            string loc = string.Format("{0},{1}", doorLocation.X, doorLocation.Y);
            UserDeviceData ud = api.updateUserDevice(auth_token, int.Parse(DeviceId), "Room Door", "Door Device of Room " + DeviceName, loc, true, RotateS);
        }
        private void Unlock_Click(object sender, MouseEventArgs e) {
            roomDialog.getLockUnlock.Unlock();
            changeDoorPowerState(false);

            string loc = string.Format("{0},{1}", doorLocation.X, doorLocation.Y);
            UserDeviceData ud = api.updateUserDevice(auth_token, int.Parse(DeviceId), "Room Door", "Door Device of Room " + DeviceName, loc, false, RotateS);
        }

        private void On_Click(object sender, MouseEventArgs e) {
            roomDialog.getOnOff.On();
            changeRoomPowerState(true);

            Console.WriteLine("Power :" + RoomState);
        }
        private void Off_Click(object sender, MouseEventArgs e) {
            roomDialog.getOnOff.Off();
            changeRoomPowerState(false);

            Console.WriteLine("Power :"+ RoomState);
        }
    }
}