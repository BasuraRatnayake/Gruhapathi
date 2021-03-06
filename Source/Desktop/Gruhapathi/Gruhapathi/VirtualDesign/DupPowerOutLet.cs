using ForeRunners.Data;
using ForeRunners.Data.Model;
using ForeRunners.Devices.Design;
using Gruhapathi.ControlPanel;
using Gruhapathi.Dialogs;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gruhapathi.VirtualDesign {
    public class DupPowerOutLet : PowerOutlet {

        private MetroForm owner;
        private DeviceDialog deviceDiag;

        private GUICore core;

        public API api { get; set; }
        public string auth_token { get; set; }

        public string floorId { get; set; }

        public DupPowerOutLet(Panel mainPanel, MetroForm owner, bool power = false) : base(mainPanel, power) {
            this.owner = owner;

            deviceDiag = new DeviceDialog(owner, new Point(0, 0), Off_Click, On_Click, More_Click, "power_off");
            deviceDiag.OwnerPanel = "pnlFloorArea";

            if (power)
                deviceDiag.getOnOff.On();

            core = new GUICore(owner);
        }
        public override void control_Click(object sender, MouseEventArgs e) {
            deviceDiag.DialogLocation = ((Control)sender).Parent.Location;
            deviceDiag.DialogDesc = DeviceDescription;
            deviceDiag.DialogTitle = DeviceName;
            deviceDiag.DialogIcon = "power_off";
            deviceDiag.showDialog();
        }

        private void More_Click(object sender, MouseEventArgs e) {
            core.showHide_Form(new DeviceDetails(api, auth_token, DeviceId, floorId), owner);
        }

        private void On_Click(object sender, MouseEventArgs e) {
            deviceDiag.getOnOff.On();
            changePowerState(true);

            string loc = string.Format("{0},{1}", powerOutletLocation.X, powerOutletLocation.Y);
            UserDeviceData ud = api.updateUserDevice(auth_token, int.Parse(DeviceId), DeviceName, DeviceDescription, loc, true, RotateS);
            if (!ud.status && ud.response_code != 200) {
                deviceDiag.getOnOff.Off();
                changePowerState(false);
            }
        }
        private void Off_Click(object sender, MouseEventArgs e) {
            deviceDiag.getOnOff.Off();
            changePowerState(false);

            string loc = string.Format("{0},{1}", powerOutletLocation.X, powerOutletLocation.Y);
            UserDeviceData ud = api.updateUserDevice(auth_token, int.Parse(DeviceId), DeviceName, DeviceDescription, loc, false, RotateS);
            if (!ud.status && ud.response_code != 200) {
                deviceDiag.getOnOff.On();
                changePowerState(true);
            }
        }
    }
}
