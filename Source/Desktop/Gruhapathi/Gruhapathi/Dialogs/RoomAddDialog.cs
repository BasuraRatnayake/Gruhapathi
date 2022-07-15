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

namespace Gruhapathi.Dialogs {
    public partial class RoomAddDialog : MetroForm {

        private List<string> devId;
        private List<string> serials;
        private List<string> versions;

        private API api;
        private ConfigurationFile iniFile;

        protected string auth_token;

        public string RoomName {
            get {
                return txtRoomName.Text;
            }
        }
        public string RoomDesc {
            get {
                return txtRoomDesc.Text;
            }
        }
        public string DeviceId {
            get {
                return devId[cmbDoors.SelectedIndex].ToString();
            }
        }

        private DeviceData deviceData;

        public RoomAddDialog() {
            InitializeComponent();

            devId = new List<string>();
            serials = new List<string>();
            versions = new List<string>();

            iniFile = new ConfigurationFile("Gruhapathi.ini");
            api = new API(iniFile.Read("Path", "API"));
            auth_token = iniFile.Read("Token", "Authorization");
            getDoors();
        }

        public bool isValid() {
            if (devId.Count > 0)
                return true;
            return false;
        }

        private void getDoors() {
            deviceData = api.Device(auth_token, "all", "no");
            if (deviceData.status && deviceData.response_code == 200) {
                int doorCount = 1;
                foreach (dynamic dyn in deviceData.getDevices) {
                    if(dyn.Item4 == "Door") {
                        cmbDoors.Items.Add("Door " + doorCount);
                        devId.Add(dyn.Item1.ToString());
                        serials.Add(dyn.Item3.ToString());
                        versions.Add(dyn.Item2.ToString());
                        doorCount++;
                    }
                }
                if(devId.Count > 0)
                    cmbDoors.SelectedIndex = 0;
            } else if (deviceData.response_code == 410) {
                if (!api.reAuthenticate()) {
                    new StartUp().Show();
                    this.Hide();
                } else {
                    getDoors();
                }
            } else {
                new StartUp().Show();
                this.Hide();
            }
        }

        private void cmbDoors_SelectedIndexChanged(object sender, EventArgs e) {
            lblDevice.Text = String.Format("Selected Door Device Details" + Environment.NewLine + Environment.NewLine + "Device Serial: {0}" + Environment.NewLine + "Device Version: {1}", serials[cmbDoors.SelectedIndex], versions[cmbDoors.SelectedIndex]);
        }

        private void gldRoom_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void RoomAddDialog_Shown(object sender, EventArgs e) {
            if (!isValid())
                this.Close();
        }
    }
}
