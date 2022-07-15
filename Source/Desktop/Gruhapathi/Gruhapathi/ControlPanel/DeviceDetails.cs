using ForeRunners.Data;
using ForeRunners.Data.Model;
using ForeRunners.GUI.Buttons;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace Gruhapathi.ControlPanel {
    public partial class DeviceDetails : MetroForm {
        private GUICore gui;

        private ButtonOnOff onOffBut;

        private API api;

        private string auth_token;
        private string floorId;
        private string deviceId;
        private string location;

        private UserDeviceData data;

        private int rotateState;

        public DeviceDetails(API api, string auth_token, string deviceId, string floorId) {
            InitializeComponent();

            gui = new GUICore(this);
            gui.createCoreGUI(5);

            this.deviceId = deviceId;
            this.api = api;
            this.auth_token = auth_token;
            this.floorId = floorId;

            onOffBut = new ButtonOnOff();
            onOffBut.DeviceId = deviceId;
            onOffBut.OnEvent = On_Click;
            onOffBut.OffEvent = Off_Click;
            onOffBut.Location = new Point(305, 218);
            this.Controls.Add(onOffBut.getButton());

            lblLocation.Text = "";

            data = api.getUserDevice(auth_token, int.Parse(floorId), int.Parse(deviceId));
            if (data.status && data.response_code == 200) {
                foreach (dynamic dyn in data.data) {
                    if (dyn.Name == "floor_name" || dyn.Name == "room_name") {
                        if (dyn.Name == "room_name" && string.IsNullOrEmpty(dyn.Value.ToString())) {
                            lblLocation.Text = lblLocation.Text.Replace(" -> ", string.Empty);
                            continue;
                        }
                        if (dyn.Name == "room_name" && !string.IsNullOrEmpty(dyn.Value.ToString())) {
                            lblLocation.Text += dyn.Value;
                        }
                        lblLocation.Text += dyn.Value+" -> ";
                    }

                    if (dyn.Name == "devices") {
                        foreach (dynamic dd in dyn) {
                            lblSerial.Text = dd[0].serial;
                            lblVersion.Text = dd[0].dev_version;

                            string[] dateMan = dd[0].dev_man.ToString().Split(' ');
                            lblManDate.Text = dateMan[0].Replace("-", "/");

                            txtDesc.Text = dd[0].dev_desc;
                            txtName.Text = dd[0].dev_name;
                            location = dd[0].location;
                            rotateState = dd[0].rotate_state;

                            if (dd[0].power_state.ToString() == "1") {
                                onOffBut.On();
                            } else {
                                onOffBut.Off();
                            }

                            string dev_type = dd[0].dev_type.ToString();

                            switch (dev_type) {
                                case "Light":
                                    pnlIcon.BackgroundImage = gui.getImage("light_on_lrg");
                                    break;
                                case "Power Outlet":
                                    pnlIcon.BackgroundImage = gui.getImage("power_on_lrg");
                                    break;
                                case "Door":
                                    pnlIcon.BackgroundImage = gui.getImage("door_lrg");
                                    break;
                                case "Window":
                                    pnlIcon.BackgroundImage = gui.getImage("window_lrg");
                                    break;
                                case "Water Tap":
                                    pnlIcon.BackgroundImage = gui.getImage("water_lrg");
                                    break;
                            }
                        }
                    }
                }
            } else {
                gui.showHide_Form(new Rooms(), this);
            }        

            setChartData();
        }

        private void On_Click(object sender, MouseEventArgs e) {
            onOffBut.On();

            UserDeviceData ud = api.updateUserDevice(auth_token, int.Parse(deviceId), txtName.Text, txtDesc.Text, location, true, rotateState);
            if (!ud.status && ud.response_code != 200) {
                onOffBut.Off();
            }
        }
        private void Off_Click(object sender, MouseEventArgs e) {
            onOffBut.Off();

            UserDeviceData ud = api.updateUserDevice(auth_token, int.Parse(deviceId), txtName.Text, txtDesc.Text, location, false, rotateState);
            if (!ud.status && ud.response_code != 200) {
                onOffBut.On();
            }
        }

        private void setChartData() {
            chrUsage.Series.Clear();

            Series series = chrUsage.Series.Add("Total Income");
            series.ChartType = SeriesChartType.Line;
            series.BorderWidth = 2;
            series.Color = ColorTranslator.FromHtml("#f2b600");
            series.Points.AddXY(1, 100);
            series.Points.AddXY(2, 200);
            series.Points.AddXY(3, 230);
            series.Points.AddXY(4, 235);
            series.Points.AddXY(5, 240);
            series.Points.AddXY(6, 400);
            series.Points.AddXY(7, 100);
            series.Points.AddXY(8, 250);
            series.Points.AddXY(9, 280);
            series.Points.AddXY(10, 310);
            series.Points.AddXY(11, 180);
            series.Points.AddXY(12, 170);
            series.Points.AddXY(30, 110);
        }

        private void butOn_Click(Object sender, MouseEventArgs e, ButtonOnOff data) {
            data.On();
            MessageBox.Show(data.DeviceId);
        }
        private void butOff_Click(Object sender, MouseEventArgs e, ButtonOnOff data) {
            data.Off();
            MessageBox.Show(data.DeviceId);
        }
        
    }
}
