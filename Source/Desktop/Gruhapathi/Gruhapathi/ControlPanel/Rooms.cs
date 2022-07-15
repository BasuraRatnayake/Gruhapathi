using ForeRunners.Data;
using ForeRunners.Data.Model;
using Gruhapathi.Dialogs;
using Gruhapathi.VirtualDesign;
using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gruhapathi.ControlPanel {
    public partial class Rooms : MetroForm {

        private GUICore gui;
        private Floor floor;

        private API api;
        private ConfigurationFile iniFile;

        protected string auth_token;

        private FloorData floorData;
        private RoomData roomData;

        private bool isRoom;
        private int roomId;
        private bool firstTime = false;
        private int floorId;
        private string roomName;

        public Rooms(bool isRoom = false, int roomId = 0, int floorId = 0, string roomName = "") {
            InitializeComponent();
            try {
                this.isRoom = isRoom;
                this.roomId = roomId;
                this.floorId = floorId;
                this.roomName = roomName;

                gui = new GUICore(this);
                floor = new Floor(pnlFloorArea, this);

                initControls();

                iniFile = new ConfigurationFile("Gruhapathi.ini");
                api = new API(iniFile.Read("Path", "API"));
                auth_token = iniFile.Read("Token", "Authorization");

                cmbFloors.Items.Clear();

                if(isRoom && roomId != 0) {
                    firstTime = true;
                } 

                getFloors();
            } catch (Exception ex) { }
        }

        private void getFloors() {
            try {
                floorData = api.Floors(auth_token);
                if(floorData.status && floorData.response_code == 200) {
                    foreach (dynamic dyn in floorData.getFloors) {
                        cmbFloors.Items.Add(dyn.Item2.ToString().ToUpper());
                    }
                    lblFloor.Text = cmbFloors.Items[0].ToString();
                    lblFloorName.Text = cmbFloors.Items[0].ToString();
                    if (isRoom && roomId != 0 && firstTime) {
                        foreach(dynamic dyn in floorData.getFloors) {
                            if(dyn.Item1 == floorId) {
                                cmbFloors.SelectedIndexChanged -= cmbFloors_SelectedIndexChanged;
                                cmbFloors.SelectedIndex = cmbFloors.FindString(lblFloor.Text);
                                cmbFloors.SelectedIndexChanged += cmbFloors_SelectedIndexChanged;

                                lblFloorName.Text = lblFloorName.Text + " -> " + roomName;
                                lblFloor.Text = roomName;

                                floor = new Floor(pnlFloorArea, this);
                                floor.auth_token = auth_token;
                                floor.api = api;
                                pnlFloorArea.Controls.Clear();
                                pnlFloorArea.Refresh();

                                floor.FloorId = floorData.getFloors[cmbFloors.SelectedIndex].Item1.ToString();
                                floor.FloorName = floorData.getFloors[cmbFloors.SelectedIndex].Item2.ToString();
                                break;
                            }
                        }
                        floor.getDevices(roomId);
                        firstTime = false;
                    } else {
                        cmbFloors.SelectedIndex = 0;
                    }
                } else if (floorData.response_code == 410) {
                    if (api.reAuthenticate()) {
                        getFloors();
                    } else {
                        Application.Restart();
                    }
                } else {
                    return;
                }
            } catch (Exception ex) { }
        }

        private void cmbFloors_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                floor = new Floor(pnlFloorArea, this);
                floor.auth_token = auth_token;
                floor.api = api;
                pnlFloorArea.Controls.Clear();
                pnlFloorArea.Refresh();

                floor.FloorId = floorData.getFloors[cmbFloors.SelectedIndex].Item1.ToString();
                floor.FloorName = floorData.getFloors[cmbFloors.SelectedIndex].Item2.ToString();

                lblFloor.Text = floor.FloorName;
                lblFloorName.Text = floor.FloorName;

                floor.getRooms();
                floor.getDevices();

                pnlFloor.Enabled = true;
                pnlRoom.Enabled = true;
                pnlDoor.Enabled = true;
                lblA2H.Text = "ADD TO HOUSE";
                isRoom = false;
            } catch (Exception ex) { }
        }

        private void initControls() {
            gui.createCoreGUI(4);
            pnlFloorArea.Paint += (sender2, e2) => gui.pnlI1_Paint(sender2, e2, "f2b600");
            pnlLegends.Paint += (sender2, e2) => gui.pnlI1_Paint(sender2, e2, "f2b600");
            pnlAddTO.Paint += (sender2, e2) => gui.pnlI1_Paint(sender2, e2, "f2b600");

            panel6.BackgroundImage = gui.getImage("floor");
            panel8.BackgroundImage = gui.getImage("floor_h");

            pnlR.BackgroundImage = gui.getImage("room");
            pnlW.BackgroundImage = gui.getImage("window");
            pnlD.BackgroundImage = gui.getImage("door");
            pnlWT.BackgroundImage = gui.getImage("water");
            pnlP.BackgroundImage = gui.getImage("power_off");
            pnlL.BackgroundImage = gui.getImage("light_off");
            pnlF.BackgroundImage = gui.getImage("floor_h");

            panel10.BackgroundImage = gui.getImage("modify");
            panel3.BackgroundImage = gui.getImage("legends");
        }

        private void pnlLClose_MouseClick(object sender, MouseEventArgs e) {
            pnlLegends.Visible = false;
            pnlLegends.Size = new Size(0, 0);
            pnlFloorArea.Invalidate();
            pnlFloorArea.BringToFront();
        }

        private void pnlLHelp_MouseClick(object sender, MouseEventArgs e) {
            pnlLegends.Visible = !pnlLegends.Visible;
            if(pnlLegends.Visible)
                pnlLegends.Size = new Size(135, 314);
            else
                pnlLegends.Size = new Size(0, 0);
            pnlFloorArea.Invalidate();
            pnlLegends.BringToFront();
        }

        private void pnlRoom_MouseEnter(object sender, EventArgs e) {
            if (((Control)sender).Controls.Count == 2)
                ((Control)sender).BackColor = ColorTranslator.FromHtml("#1c1c1c");
            else if (((Control)sender).Parent.Controls.Count == 2) 
                ((Control)sender).Parent.BackColor = ColorTranslator.FromHtml("#1c1c1c");            
        }
        private void pnlRoom_MouseLeave(object sender, EventArgs e) {
            if (((Control)sender).Controls.Count == 2)
                ((Control)sender).BackColor = ColorTranslator.FromHtml("#111111");
            else if (((Control)sender).Parent.Controls.Count == 2) 
                ((Control)sender).Parent.BackColor = ColorTranslator.FromHtml("#111111");            
        }

        private void pnlModify_MouseClick(object sender, MouseEventArgs e) {
            show_AddBox();
            floor.enableEditing();
        }
        private void btnSave_MouseClick(object sender, MouseEventArgs e) {
            show_AddBox();
            floor.disableEditing();
        }
        private void show_AddBox() {
            if (isRoom) {
                pnlFloor.Enabled = false;
                pnlRoom.Enabled = false;
                pnlDoor.Enabled = false;
                lblA2H.Text = "ADD TO ROOM";
            } else {
                pnlFloor.Enabled = true;
                pnlRoom.Enabled = true;
                pnlDoor.Enabled = true;
                lblA2H.Text = "ADD TO HOUSE";
            }

            btnSave.Visible = !btnSave.Visible;
            btnSave.BringToFront();
            pnlAddTO.Visible = !pnlAddTO.Visible;
            if (pnlAddTO.Visible)
                pnlAddTO.Size = new Size(164, 274);
            else
                pnlAddTO.Size = new Size(0, 0);
            pnlAddTO.BringToFront();
        }

        #region Add Devices
        private void pnlRoom_Click(object sender, EventArgs e) {
            RoomAddDialog roomD = new RoomAddDialog();
            roomD.ShowDialog();

            if (roomD.isValid()) {
                string deviceId = roomD.DeviceId;
                string roomName = roomD.RoomName;
                string roomDesc = roomD.RoomDesc;

                floor.addRoom(deviceId, roomName, roomDesc, false);
            } else {
                MetroMessageBox.Show(this, "You don't have any Door Devices Registered." + Environment.NewLine + "Buy and Register Door Devices Inorder to Add More Rooms", "Gruhapathi Control Panel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }

        private void pnlDoor_Click(object sender, EventArgs e) {
            DeviceAddDialog doorD = new DeviceAddDialog("Door");
            doorD.ShowDialog();

            if (doorD.isValid()) {
                string deviceId = doorD.DeviceId;
                string deviceName = doorD.RoomName;
                string deviceDesc = doorD.RoomDesc;

                floor.addDoor(deviceId, deviceName, deviceDesc);
            } else {
                MetroMessageBox.Show(this, "You don't have any Door Devices Registered." + Environment.NewLine + "Buy and Register Door Devices Inorder to Add Doors", "Gruhapathi Control Panel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pnlWindow_Click(object sender, EventArgs e) {
            DeviceAddDialog doorD = new DeviceAddDialog("Window");
            doorD.ShowDialog();

            if (doorD.isValid()) {
                string deviceId = doorD.DeviceId;
                string deviceName = doorD.RoomName;
                string deviceDesc = doorD.RoomDesc;

                floor.addWindow(deviceId, deviceName, deviceDesc);
            } else {
                MetroMessageBox.Show(this, "You don't have any Window Devices Registered." + Environment.NewLine + "Buy and Register Window Devices Inorder to Add Window", "Gruhapathi Control Panel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pnlPower_Click(object sender, EventArgs e) {
            DeviceAddDialog powerD = new DeviceAddDialog("Power Outlet");
            powerD.ShowDialog();

            if (powerD.isValid()) {
                string deviceId = powerD.DeviceId;
                string deviceName = powerD.RoomName;
                string deviceDesc = powerD.RoomDesc;

                floor.addPowerOutlet(deviceId, deviceName, deviceDesc);
            } else {
                MetroMessageBox.Show(this, "You don't have any Power Outlet Devices Registered." + Environment.NewLine + "Buy and Register Power Outlet Devices Inorder to Add Power Outlet", "Gruhapathi Control Panel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pnlLight_Click(object sender, EventArgs e) {
            DeviceAddDialog powerD = new DeviceAddDialog("Light");
            powerD.ShowDialog();

            if (powerD.isValid()) {
                string deviceId = powerD.DeviceId;
                string deviceName = powerD.RoomName;
                string deviceDesc = powerD.RoomDesc;

                floor.addLight(deviceId, deviceName, deviceDesc);
            } else {
                MetroMessageBox.Show(this, "You don't have any Light Devices Registered." + Environment.NewLine + "Buy and Register Light Devices Inorder to Add Light", "Gruhapathi Control Panel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pnlWaterT_Click(object sender, EventArgs e) {
            DeviceAddDialog powerD = new DeviceAddDialog("Water Tap");
            powerD.ShowDialog();

            if (powerD.isValid()) {
                string deviceId = powerD.DeviceId;
                string deviceName = powerD.RoomName;
                string deviceDesc = powerD.RoomDesc;

                floor.addWaterTap(deviceId, deviceName, deviceDesc);
            } else {
                MetroMessageBox.Show(this, "You don't have any Water Tap Devices Registered." + Environment.NewLine + "Buy and Register Water Tap Devices Inorder to Add Water Tap", "Gruhapathi Control Panel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pnlFloor_Click(object sender, EventArgs e) {
            InputDialog inputBox = new InputDialog();
            inputBox.Text = "New Floor";
            inputBox.Description = "Please Enter a Name for the Floor";
            inputBox.InputLabel = "Floor Name:";
            inputBox.ShowDialog(this);

            FloorData fd = api.addFloor(auth_token, inputBox.InputText);
            if(fd.status && fd.response_code == 200) {
                cmbFloors.Items.Add(inputBox.InputText);

                lblFloor.Text = cmbFloors.Items[cmbFloors.Items.Count-1].ToString();
                cmbFloors.SelectedIndex = cmbFloors.Items.Count - 1;
                lblFloorName.Text = cmbFloors.Items[cmbFloors.Items.Count - 1].ToString();
            }
        }
        #endregion
        private void btnSave_Click(object sender, EventArgs e) {
            #region Rooms
            for (int i = 0; i < floor.Rooms.Count; i++) {
                DupRoom room = floor.Rooms[i];
                int roomId = floor.newRooms.FindIndex(x => x == i);
                string loc = string.Format("{0},{1}", room.roomLocation.X, room.roomLocation.Y);
                string size = string.Format("{0},{1}", room.roomSize.Width, room.roomSize.Height);
                RoomData rd;
                if (roomId != -1) {//Add new 
                    rd = api.addRoom(auth_token, int.Parse(floor.FloorId), room.DeviceName, room.DeviceDescription, loc, size, false, room.RotateS);
                    if(rd.status && rd.response_code == 200) {
                        floor.Rooms[i].RoomId = rd.data.room_id;     
                                        
                        UserDeviceData ud = api.addUserDevice(auth_token, int.Parse(room.DeviceId), int.Parse(floor.FloorId), int.Parse(room.RoomId), "Room Door", "Door Device of Room " + room.DeviceName, "0,0", room.RoomState, true, room.RotateS);

                        if(ud.status && ud.response_code == 200) {
                            floor.Rooms[i].DeviceId = ud.data.user_device_id.ToString();
                        }
                    }
                } else {//Update
                    rd = api.updateRoom(auth_token, int.Parse(floor.FloorId), int.Parse(room.RoomId), room.DeviceName, room.DeviceDescription, loc, size, room.RoomState, room.RotateState);
                    if(!rd.status && rd.response_code != 200) {
                        rd = api.updateRoom(auth_token, int.Parse(floor.FloorId), int.Parse(room.RoomId), room.DeviceName, room.DeviceDescription, loc, size, room.RoomState, room.RotateState);
                    }
                }
            }
            floor.newRooms.Clear();
            #endregion
            #region Doors
            for (int i = 0; i < floor.Doors.Count; i++) {
                DupDoor door = floor.Doors[i];
                int doorId = floor.newDoors.FindIndex(x => x == i);
                string loc = string.Format("{0},{1}", door.doorLocation.X, door.doorLocation.Y);

                if (isRoom == false) {
                    if (doorId != -1) {//Add new 
                        UserDeviceData ud = api.addUserDevice(auth_token, int.Parse(door.DeviceId), int.Parse(floor.FloorId), 0, door.DeviceName, door.DeviceDescription, loc, false, true, door.RotateS);

                        if (ud.status && ud.response_code == 200) {
                            floor.Doors[i].DeviceId = ud.data.user_device_id.ToString();
                        }
                    } else {//Update
                        UserDeviceData ud = api.updateUserDevice(auth_token, int.Parse(door.DeviceId), door.DeviceName, door.DeviceDescription, loc, door.DoorState, door.RotateS);
                        if (!ud.status && ud.response_code != 200) {
                            ud = api.updateUserDevice(auth_token, int.Parse(door.DeviceId), door.DeviceName, door.DeviceDescription, loc, door.DoorState, door.RotateS);
                        }
                    }
                } else {
                    if (doorId != -1) {//Add new
                        UserDeviceData ud = api.addUserDevice(auth_token, int.Parse(door.DeviceId), int.Parse(floor.FloorId), 0, door.DeviceName, door.DeviceDescription, loc, false, true, door.RotateS);

                        if (ud.status && ud.response_code == 200) {
                            floor.PowerOutlets[i].DeviceId = ud.data.user_device_id.ToString();
                        }
                    } else {//Update
                        UserDeviceData ud = api.updateUserDevice(auth_token, int.Parse(door.DeviceId), door.DeviceName, door.DeviceDescription, loc, door.DoorState, door.RotateS);
                        if (!ud.status && ud.response_code != 200) {
                            ud = api.updateUserDevice(auth_token, int.Parse(door.DeviceId), door.DeviceName, door.DeviceDescription, loc, door.DoorState, door.RotateS);
                        }
                    }
                }
            }
            floor.newDoors.Clear();
            #endregion
            #region PowerOutlets
            for (int i = 0; i < floor.PowerOutlets.Count; i++) {
                DupPowerOutLet power = floor.PowerOutlets[i];
                int powerId = floor.newPowerOutlets.FindIndex(x => x == i);
                string loc = string.Format("{0},{1}", power.powerOutletLocation.X, power.powerOutletLocation.Y);

                if (isRoom == false) {
                    if (powerId != -1) {//Add new
                        UserDeviceData ud = api.addUserDevice(auth_token, int.Parse(power.DeviceId), int.Parse(floor.FloorId), 0, power.DeviceName, power.DeviceDescription, loc, false, false, power.RotateS);

                        if (ud.status && ud.response_code == 200) {
                            floor.PowerOutlets[i].DeviceId = ud.data.user_device_id.ToString();
                        }
                    } else {//Update
                        UserDeviceData ud = api.updateUserDevice(auth_token, int.Parse(power.DeviceId), power.DeviceName, power.DeviceDescription, loc, power.powerState, power.RotateS);
                        if (!ud.status && ud.response_code != 200) {
                            ud = api.updateUserDevice(auth_token, int.Parse(power.DeviceId), power.DeviceName, power.DeviceDescription, loc, power.powerState, power.RotateS);
                        }
                    }
                } else {
                    if (powerId != -1) {//Add new
                        UserDeviceData ud = api.addUserDevice(auth_token, int.Parse(power.DeviceId), int.Parse(floor.FloorId), roomId, power.DeviceName, power.DeviceDescription, loc, false, false, power.RotateS);

                        if (ud.status && ud.response_code == 200) {
                            floor.PowerOutlets[i].DeviceId = ud.data.user_device_id.ToString();
                        }
                    } else {//Update
                        UserDeviceData ud = api.updateUserDevice(auth_token, int.Parse(power.DeviceId), power.DeviceName, power.DeviceDescription, loc, power.powerState, power.RotateS);

                        if (!ud.status && ud.response_code != 200) {
                            ud = api.updateUserDevice(auth_token, int.Parse(power.DeviceId), power.DeviceName, power.DeviceDescription, loc, power.powerState, power.RotateS);
                        }
                    }
                }
            }
            floor.newPowerOutlets.Clear();
            #endregion
            #region Windows
            for (int i = 0; i < floor.Windows.Count; i++) {
                DupWindow window = floor.Windows[i];
                int windowId = floor.newWindows.FindIndex(x => x == i);
                string loc = string.Format("{0},{1}", window.windowLocation.X, window.windowLocation.Y);

                if (isRoom == false) {
                    if (windowId != -1) {//Add new
                        UserDeviceData ud = api.addUserDevice(auth_token, int.Parse(window.DeviceId), int.Parse(floor.FloorId), 0, window.DeviceName, window.DeviceDescription, loc, false, false, window.RotateS);

                        if (ud.status && ud.response_code == 200) {
                            floor.Windows[i].DeviceId = ud.data.user_device_id.ToString();
                        }
                    } else {//Update
                        UserDeviceData ud = api.updateUserDevice(auth_token, int.Parse(window.DeviceId), window.DeviceName, window.DeviceDescription, loc, window.powerState, window.RotateS);
                        if (!ud.status && ud.response_code != 200) {
                            ud = api.updateUserDevice(auth_token, int.Parse(window.DeviceId), window.DeviceName, window.DeviceDescription, loc, window.powerState, window.RotateS);
                        }
                    }
                } else {
                    if (windowId != -1) {//Add new
                        UserDeviceData ud = api.addUserDevice(auth_token, int.Parse(window.DeviceId), int.Parse(floor.FloorId), roomId, window.DeviceName, window.DeviceDescription, loc, false, false, window.RotateS);

                        if (ud.status && ud.response_code == 200) {
                            floor.Windows[i].DeviceId = ud.data.user_device_id.ToString();
                        }
                    } else {//Update
                        UserDeviceData ud = api.updateUserDevice(auth_token, int.Parse(window.DeviceId), window.DeviceName, window.DeviceDescription, loc, window.powerState, window.RotateS);
                        if (!ud.status && ud.response_code != 200) {
                            ud = api.updateUserDevice(auth_token, int.Parse(window.DeviceId), window.DeviceName, window.DeviceDescription, loc, window.powerState, window.RotateS);
                        }
                    }
                }
            }
            floor.newWindows.Clear();
            #endregion
            #region Light
            for (int i = 0; i < floor.Lights.Count; i++) {
                DupLight light = floor.Lights[i];
                int lightId = floor.newLights.FindIndex(x => x == i);
                string loc = string.Format("{0},{1}", light.lightLocation.X, light.lightLocation.Y);

                if (isRoom == false) {
                    if (lightId != -1) {//Add new
                        UserDeviceData ud = api.addUserDevice(auth_token, int.Parse(light.DeviceId), int.Parse(floor.FloorId), 0, light.DeviceName, light.DeviceDescription, loc, false, false, light.RotateS);

                        if (ud.status && ud.response_code == 200) {
                            floor.Lights[i].DeviceId = ud.data.user_device_id.ToString();
                        }
                    } else {//Update
                        UserDeviceData ud = api.updateUserDevice(auth_token, int.Parse(light.DeviceId), light.DeviceName, light.DeviceDescription, loc, light.powerState, light.RotateS);
                        if (!ud.status && ud.response_code != 200) {
                            ud = api.updateUserDevice(auth_token, int.Parse(light.DeviceId), light.DeviceName, light.DeviceDescription, loc, light.powerState, light.RotateS);
                        }
                    }
                } else {
                    if (lightId != -1) {//Add new
                        UserDeviceData ud = api.addUserDevice(auth_token, int.Parse(light.DeviceId), int.Parse(floor.FloorId), roomId, light.DeviceName, light.DeviceDescription, loc, false, false, light.RotateS);

                        if (ud.status && ud.response_code == 200) {
                            floor.Lights[i].DeviceId = ud.data.user_device_id.ToString();
                        }
                    } else {//Update
                        UserDeviceData ud = api.updateUserDevice(auth_token, int.Parse(light.DeviceId), light.DeviceName, light.DeviceDescription, loc, light.powerState, light.RotateS);
                        if (!ud.status && ud.response_code != 200) {
                            ud = api.updateUserDevice(auth_token, int.Parse(light.DeviceId), light.DeviceName, light.DeviceDescription, loc, light.powerState, light.RotateS);
                        }
                    }
                }
            }
            floor.newLights.Clear();
            #endregion
            #region Water Taps
            for (int i = 0; i < floor.WaterTaps.Count; i++) {
                DupWaterTap water = floor.WaterTaps[i];
                int waterId = floor.newWaterTaps.FindIndex(x => x == i);
                string loc = string.Format("{0},{1}", water.waterTapLocation.X, water.waterTapLocation.Y);

                if (isRoom == false) {
                    if (waterId != -1) {//Add new
                        UserDeviceData ud = api.addUserDevice(auth_token, int.Parse(water.DeviceId), int.Parse(floor.FloorId), 0, water.DeviceName, water.DeviceDescription, loc, false, false, water.RotateS);

                        if (ud.status && ud.response_code == 200) {
                            floor.WaterTaps[i].DeviceId = ud.data.user_device_id.ToString();
                        }
                    } else {//Update
                        UserDeviceData ud = api.updateUserDevice(auth_token, int.Parse(water.DeviceId), water.DeviceName, water.DeviceDescription, loc, water.powerState, water.RotateS);
                        if (!ud.status && ud.response_code != 200) {
                            ud = api.updateUserDevice(auth_token, int.Parse(water.DeviceId), water.DeviceName, water.DeviceDescription, loc, water.powerState, water.RotateS);
                        }
                    }
                } else {
                    if (waterId != -1) {//Add new
                        UserDeviceData ud = api.addUserDevice(auth_token, int.Parse(water.DeviceId), int.Parse(floor.FloorId), roomId, water.DeviceName, water.DeviceDescription, loc, false, false, water.RotateS);

                        if (ud.status && ud.response_code == 200) {
                            floor.WaterTaps[i].DeviceId = ud.data.user_device_id.ToString();
                        }
                    } else {//Update
                        UserDeviceData ud = api.updateUserDevice(auth_token, int.Parse(water.DeviceId), water.DeviceName, water.DeviceDescription, loc, water.powerState, water.RotateS);
                        if (!ud.status && ud.response_code != 200) {
                            ud = api.updateUserDevice(auth_token, int.Parse(water.DeviceId), water.DeviceName, water.DeviceDescription, loc, water.powerState, water.RotateS);
                        }
                    }
                }
            }
            floor.newWaterTaps.Clear();
            #endregion
            floor.disableEditing();
        }
    }
}