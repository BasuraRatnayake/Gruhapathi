using ForeRunners.Data;
using ForeRunners.Data.Model;
using Gruhapathi.Dialogs;
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
    public partial class Devices : MetroForm {

        private GUICore gui;
        private DeviceDialog deviceDiag;

        private bool assigned = true;

        private API api;
        private ConfigurationFile iniFile;
        protected string auth_token;

        private List<Tuple<int, int, string, string, string>> assignedDevices;
        private List<Tuple<int, int, string, string, string>> unAssignedDevices;

        public Devices() {
            InitializeComponent();

            iniFile = new ConfigurationFile("Gruhapathi.ini");
            api = new API(iniFile.Read("Path", "API"));
            auth_token = iniFile.Read("Token", "Authorization");

            gui = new GUICore(this);
            gui.createCoreGUI(5);

            panel3.BackgroundImage = gui.getImage("unAssign_Sel");
            panel4.BackgroundImage = gui.getImage("unAssign");

            assignedDevices = new List<Tuple<int, int, string, string, string>>();
            unAssignedDevices = new List<Tuple<int, int, string, string, string>>();

            devices = new List<Panel>();

            getData();

            devicesAssigned();
        }

        private void getData() {
            try {
                DeviceData dData = api.Device(auth_token, "all", "yes");
                if (dData.status && dData.response_code == 200) {
                    foreach (dynamic data in dData.data.devices) {
                        int id = data.device_id;
                        int verId = data.version_id;
                        string serial = data.device_serial;
                        string type = data.device_type;
                        string version = data.version;

                        assignedDevices.Add(new Tuple<int, int, string, string, string>(id, verId, serial, version, type));
                    }
                }

                dData = api.Device(auth_token, "all", "no");
                if (dData.status && dData.response_code == 200) {
                    foreach (dynamic data in dData.data.devices) {
                        int id = data.device_id;
                        int verId = data.version_id;
                        string serial = data.device_serial;
                        string type = data.device_type;
                        string version = data.version;

                        unAssignedDevices.Add(new Tuple<int, int, string, string, string>(id, verId, serial, version, type));
                    }
                }
            } catch (Exception ex) { }
        }


        #region DeviceResults
        private int cur_devRes_x = 187, cur_devRes_y = 177;
        private int devRes_count = 1;
        private void set_devRes_points() {
            cur_devRes_x += 204;
            if (cur_devRes_x > 595) {
                cur_devRes_x = 187;
                cur_devRes_y += 45;
            }
            devRes_count++;
        }

        private List<Panel> devices;

        private void setDevicePanel(string devImg, string devName, string devLoc, string devId, string desc) {
            Panel device = new Panel();
            device.Cursor = Cursors.Hand;
            device.Location = new Point(cur_devRes_x, cur_devRes_y);
            device.Size = new Size(187, 39);
            device.MouseClick += (sender2, e2) => Device_Click(sender2, e2, devImg, devName, desc, devId);

            Panel devImage = new Panel();
            devImage.BackgroundImage = gui.getImage(devImg);
            devImage.BackgroundImageLayout = ImageLayout.Center;
            devImage.Dock = DockStyle.Left;
            devImage.Location = new Point(0, 0);
            devImage.Size = new Size(35, 39);
            devImage.MouseClick += (sender2, e2) => Device_Click(sender2, e2, devImg, devName, desc, devId);
            device.Controls.Add(devImage);

            Label lblDevName = new Label();
            lblDevName.Font = gui.defaultFont;
            lblDevName.ForeColor = Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(181)))), ((int)(((byte)(0)))));
            lblDevName.Location = new Point(35, 5);
            lblDevName.Size = new Size(149, 14);
            lblDevName.Name = "lblDevName";
            lblDevName.Text = devName;
            lblDevName.MouseClick += (sender2, e2) => Device_Click(sender2, e2, devImg, devName, desc, devId);
            device.Controls.Add(lblDevName);

            Label lblLocName = new Label();
            lblLocName.AutoSize = true;
            lblLocName.Font = lblDevName.Font;
            lblLocName.ForeColor = lblDevName.ForeColor;
            lblLocName.Location = new Point(35, 20);
            lblLocName.Size = new Size(57, 14);
            lblLocName.Text = "Serial Number: "+ devLoc;
            lblLocName.MouseClick += (sender2, e2) => Device_Click(sender2, e2, devImg, devName, desc, devId);
            device.Controls.Add(lblLocName);

            set_devRes_points();
            devices.Add(device);
            this.Controls.Add(device);
        }

        private void Device_Click(object sender, MouseEventArgs e, string devIcon, string devName, string devDesc, string devId) {
            deviceDiag = new DeviceDialog(this, new Point(((Control)sender).Parent.Location.X-140, ((Control)sender).Parent.Location.Y), Off_Click, On_Click, More_Click, devIcon, devName, devDesc, false);
            deviceDiag.OwnerPanel = "";
            deviceDiag.getOnOff.DeviceId = devId;
            deviceDiag.showDialog();
        }
        #endregion

        private void More_Click(object sender, MouseEventArgs e) {
            MessageBox.Show("asdas");
        }
        
        private void On_Click(object sender, MouseEventArgs e) {
            deviceDiag.getOnOff.On();

            Console.WriteLine("Power :" + deviceDiag.getOnOff);
        }

        private void devicesAssigned() {
            assigned = true;
            panel3.BackgroundImage = gui.getImage("unAssign");
            panel4.BackgroundImage = gui.getImage("unAssign_Sel");

            label1.ForeColor = ColorTranslator.FromHtml("#f5b800");
            label2.ForeColor = ColorTranslator.FromHtml("#f5b800");
            label3.ForeColor = ColorTranslator.FromHtml("#ffffff");
            label4.ForeColor = ColorTranslator.FromHtml("#ffffff");

            cur_devRes_x = 187;
            cur_devRes_y = 177;
            devRes_count = 1;
            foreach (Control con in devices) {
                this.Controls.Remove(con);
            }

            for (int i = 0; i < assignedDevices.Count; i++) {
                string icon = string.Empty;
                switch (assignedDevices[i].Item5) {
                    case "Control Hub":
                        icon = "modify";
                        break;
                    case "Door":
                        icon = "door";
                        break;
                    case "Window":
                        icon = "window";
                        break;
                    case "Water Tap":
                        icon = "water";
                        break;
                    case "Power Outlet":
                        icon = "power_on";
                        break;
                    case "Light":
                        icon = "light_on";
                        break;
                }
                string desc = "Version: " + assignedDevices[i].Item4 + Environment.NewLine + "Serial Number: " + assignedDevices[i].Item3 + Environment.NewLine + "Device Type: " + assignedDevices[i].Item5;
                setDevicePanel(icon, assignedDevices[i].Item5, assignedDevices[i].Item3, assignedDevices[i].Item1.ToString(), desc);
            }
            lblDeviceA.Text = "DEVICES FOUND: " + assignedDevices.Count;
        }
        private void pnlADevices_Click(object sender, EventArgs e) {
            devicesAssigned();
        }
        private void devicesUnAssigned() {
            assigned = false;
            panel3.BackgroundImage = gui.getImage("unAssign_Sel");
            panel4.BackgroundImage = gui.getImage("unAssign");

            label1.ForeColor = ColorTranslator.FromHtml("#ffffff");
            label2.ForeColor = ColorTranslator.FromHtml("#ffffff");
            label3.ForeColor = ColorTranslator.FromHtml("#f5b800");
            label4.ForeColor = ColorTranslator.FromHtml("#f5b800");

            cur_devRes_x = 187;
            cur_devRes_y = 177;
            devRes_count = 1;
            foreach (Control con in devices) {
                this.Controls.Remove(con);
            }

            for (int i = 0; i < unAssignedDevices.Count; i++) {
                string icon = string.Empty;
                switch (unAssignedDevices[i].Item5) {
                    case "Control Hub":
                        icon = "modify";
                        break;
                    case "Door":
                        icon = "door";
                        break;
                    case "Window":
                        icon = "window";
                        break;
                    case "Water Tap":
                        icon = "water";
                        break;
                    case "Power Outlet":
                        icon = "power_on";
                        break;
                    case "Light":
                        icon = "light_on";
                        break;
                }
                string desc = "Version: " + unAssignedDevices[i].Item4 + Environment.NewLine + "Serial Number: " + unAssignedDevices[i].Item3 + Environment.NewLine + "Device Type: " + unAssignedDevices[i].Item5;
                setDevicePanel(icon, unAssignedDevices[i].Item5, unAssignedDevices[i].Item3, unAssignedDevices[i].Item1.ToString(), desc);
            }
            lblDeviceA.Text = "DEVICES FOUND: " + unAssignedDevices.Count;
        }
        private void pnlUADevices_Click(object sender, EventArgs e) {
            devicesUnAssigned();
        }

        private void Off_Click(object sender, MouseEventArgs e) {
            deviceDiag.getOnOff.Off();

            Console.WriteLine("Power :" + deviceDiag.getOnOff);
        }
    }


}
