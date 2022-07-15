using ForeRunners.Data;
using ForeRunners.Data.Model;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gruhapathi.ControlPanel {
    public partial class Security : MetroForm {

        private GUICore gui;

        private API api;
        private ConfigurationFile iniFile;

        protected string auth_token;

        private UserDeviceData deviceData;
        private FloorData floorData;
        private RoomData roomData;

        public Security() {
            InitializeComponent();

            gui = new GUICore(this);
            gui.createCoreGUI(0);

            iniFile = new ConfigurationFile("Gruhapathi.ini");
            api = new API(iniFile.Read("Path", "API"));
            auth_token = iniFile.Read("Token", "Authorization");

            pnlIcon.Paint += (sender2, e2) => gui.pnlI1_Paint(sender2, e2, "f2b600");
            pnlIcon.BackgroundImage = gui.getImage("door");
            getFloors();
        }

        private void getFloors() {
            floorData = api.Floors(auth_token);
            if (floorData.status && floorData.response_code == 200) {
                cmbFloor.Items.Clear();
                cmbRoom.Items.Clear();
                foreach (dynamic dyn in floorData.getFloors) {
                    cmbFloor.Items.Add(dyn.Item2.ToString().ToUpper());
                }
                cmbFloor.SelectedIndex = 0;
            }
        }

        private List<int> roomIds;
        private void getRooms(int FloorId) {
            roomData = api.Rooms(auth_token, int.Parse(floorData.getFloors[FloorId].Item1.ToString()));
            if (roomData.status && roomData.response_code == 200) {
                roomIds = new List<int>();
                cmbRoom.Items.Clear();
                foreach (dynamic dyn in roomData.getRooms) {
                    cmbRoom.Items.Add(dyn.Item2);
                    roomIds.Add(dyn.Item1);
                }
            }
        }

        private List<int> deviceIds;
        private void getDoors(int floorId, int RoomId = 0, int DeviceId = 0) {
            floorId = floorData.getFloors[floorId].Item1;
            deviceData = api.getUserDevice(auth_token, floorId, DeviceId, RoomId);
            if (deviceData.status && deviceData.response_code == 200) {
                deviceIds = new List<int>();
                cmbDevice.Items.Clear();
                foreach (dynamic dyn in deviceData.getDevices) {
                    if (dyn.Item6 == "Door" || dyn.Item6 == "Window") {
                        cmbDevice.Items.Add(dyn.Item3);
                        deviceIds.Add(dyn.Item1);
                    }
                }
                if(DeviceId == 0)
                    cmbDevice.SelectedIndex = 0;
            } else if (deviceData.response_code == 410) {
                if (!api.reAuthenticate()) {
                    new StartUp().Show();
                    this.Hide();
                } else {
                    getDoors(floorId);
                }
            } else {
                new StartUp().Show();
                this.Hide();
            }
        }

        private void cmbFloor_SelectedIndexChanged(object sender, EventArgs e) {
            cmbRoom.Text = "N/A";
            getDoors(cmbFloor.SelectedIndex);
            getRooms(cmbFloor.SelectedIndex);
        }

        private void cmbRoom_SelectedIndexChanged(object sender, EventArgs e) {
            getDoors(cmbFloor.SelectedIndex, roomIds[cmbRoom.SelectedIndex]);
        }

        private void cmbDevice_SelectedIndexChanged(object sender, EventArgs e) {
            if(roomIds == null || cmbRoom.SelectedIndex == -1) {
                deviceData = api.getUserDevice(auth_token, floorData.getFloors[cmbFloor.SelectedIndex].Item1, deviceIds[cmbDevice.SelectedIndex]);
            } else {
                deviceData = api.getUserDevice(auth_token, floorData.getFloors[cmbFloor.SelectedIndex].Item1, deviceIds[cmbDevice.SelectedIndex], roomIds[cmbRoom.SelectedIndex]);
            }
            
            if (deviceData.status && deviceData.response_code == 200) {
                foreach (dynamic dyn in deviceData.getDevices) {
                    if (dyn.Item6 == "Door" || dyn.Item6 == "Window") {
                        lblDevName.Text = dyn.Item3;
                        lblLoc.Text = cmbFloor.Text + "/" + (cmbRoom.Text != "N/A" ? "" : cmbRoom.Text);
                        lblDesc.Text = dyn.Item4;
                        if(dyn.Item6 == "Door") {
                            pnlIcon.BackgroundImage = gui.getImage("door");
                        } else {
                            pnlIcon.BackgroundImage = gui.getImage("window");
                        }

                        string power = dyn.Item7.ToString().Split(',')[0];
                        if(power == "0") {
                            lblPower.Text = "Not Locked";
                        } else {
                            lblPower.Text = "Locked";
                        }
                    }
                }
            }
        }

        private void lblLock_Click(object sender, EventArgs e) {
            if(api.change_power(auth_token, "\"power_state\": 1, \"device_id\":" + deviceIds[cmbDevice.SelectedIndex])) {
                lblPower.Text = "Locked";
            } else {
                lblPower.Text = "Not Locked";
            }
        }

        private void lblUnLock_Click(object sender, EventArgs e) {
            if (api.change_power(auth_token, "\"power_state\": 0, \"device_id\":" + deviceIds[cmbDevice.SelectedIndex])) {
                lblPower.Text = "Not Locked";
            } else {
                lblPower.Text = "Locked";
            }
        }
    }
}