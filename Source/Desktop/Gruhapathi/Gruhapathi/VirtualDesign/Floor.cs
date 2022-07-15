using System;
using System.Collections.Generic;
using ForeRunners.Devices.Design;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Drawing;
using ForeRunners.Data;
using ForeRunners.Data.Model;

namespace Gruhapathi.VirtualDesign {
    public class Floor : IFloor {
        private List<DupRoom> rooms;
        public List<DupRoom> Rooms {
            get {
                return rooms;
            }
        }

        private List<DupDoor> doors;
        public List<DupDoor> Doors {
            get {
                return doors;
            }
        }

        private List<DupPowerOutLet> powerOutlets;
        public List<DupPowerOutLet> PowerOutlets {
            get {
                return powerOutlets;
            }
        }

        private List<DupLight> lights;
        public List<DupLight> Lights {
            get {
                return lights;
            }
        }

        private List<DupWaterTap> waterTaps;
        public List<DupWaterTap> WaterTaps {
            get {
                return waterTaps;
            }
        }

        private List<DupWindow> windows;
        public List<DupWindow> Windows {
            get {
                return windows;
            }
        }

        private Panel mainPanel;

        private MetroForm owner;

        public List<int> newRooms { get; set; }
        public List<int> newDoors { get; set; }
        public List<int> newPowerOutlets { get; set; }
        public List<int> newWaterTaps { get; set; }
        public List<int> newWindows { get; set; }
        public List<int> newLights { get; set; }

        public string FloorName { get; set; }
        public string FloorId { get; set; }

        public string auth_token { get; set; }
        public API api { get; set; }

        public Floor(Panel mainPanel, MetroForm owner) {
            this.owner = owner;
            this.mainPanel = mainPanel;

            rooms = new List<DupRoom>();
            doors = new List<DupDoor>();
            lights = new List<DupLight>();
            powerOutlets = new List<DupPowerOutLet>();
            waterTaps = new List<DupWaterTap>();
            windows = new List<DupWindow>();

            newRooms = new List<int>();
            newDoors = new List<int>();
            newPowerOutlets = new List<int>();
            newWaterTaps = new List<int>();
            newWindows = new List<int>();
            newLights = new List<int>();
        }

        public enum DeviceInFloor {
            Lights,
            PowerOutlet,
            Rooms,
            WaterTap,
            Window,
            Door
        }

        public void addDoor(string deviceId = null, string deviceName = null, string description = null, bool power = false) {
            DupDoor door = new DupDoor(mainPanel, owner, power);
            door.DeviceId = deviceId;
            door.DeviceName = deviceName;
            door.DeviceDescription = description;
            door.addControl();
            doors.Add(door);
            newDoors.Add(doors.Count - 1);
        }
        public void addRoom(string deviceId = null, string deviceName = null, string description = null, bool power = false) {
            if (deviceName == null)
                deviceName = "Room " + (rooms.Count + 1);
            DupRoom room = new DupRoom(mainPanel, owner, deviceName, power);
            room.DeviceId = deviceId;
            room.DeviceName = deviceName;
            room.DeviceDescription = description;
            room.addControl();
            rooms.Add(room);
            newRooms.Add(rooms.Count-1);
        }
        public void addLight(string deviceId = null, string deviceName = null, string description = null, bool power = false) {
            DupLight light = new DupLight(mainPanel, owner, power);
            light.DeviceId = deviceId;
            light.DeviceName = deviceName;
            light.DeviceDescription = description;
            light.addControl();
            lights.Add(light);
            newLights.Add(lights.Count - 1);
        }
        public void addWaterTap(string deviceId = null, string deviceName = null, string description = null, bool power = false) {
            DupWaterTap waterTap = new DupWaterTap(mainPanel, owner, power);
            waterTap.DeviceId = deviceId;
            waterTap.DeviceName = deviceName;
            waterTap.DeviceDescription = description;
            waterTap.addControl();
            waterTaps.Add(waterTap);
            newWaterTaps.Add(waterTaps.Count - 1);
        }
        public void addWindow(string deviceId = null, string deviceName = null, string description = null, bool power = false) {
            DupWindow window = new DupWindow(mainPanel, owner, power);
            window.DeviceId = deviceId;
            window.DeviceName = deviceName;
            window.DeviceDescription = description;
            window.addControl();
            windows.Add(window);
            newWindows.Add(windows.Count - 1);
        }
        public void addPowerOutlet(string deviceId = null, string deviceName = null, string description = null, bool power = false) {
            DupPowerOutLet powerOutlet = new DupPowerOutLet(mainPanel, owner, power);
            powerOutlet.DeviceId = deviceId;
            powerOutlet.DeviceName = deviceName;
            powerOutlet.DeviceDescription = description;
            powerOutlet.addControl();
            powerOutlets.Add(powerOutlet);
            newPowerOutlets.Add(powerOutlets.Count - 1);
        }
        
        public void addRoom(Point location, Size size, int rotateState, string deviceId, string deviceName = null, string description = null, bool power = false) {
            DupRoom room = new DupRoom(mainPanel, owner, deviceName, power);
            room.Predefined = true;
            room.roomSize = size;
            room.RotateState = rotateState;
            room.DoorState = power;
            room.roomLocation = location;
            room.DeviceId = deviceId;
            room.DeviceName = deviceName;
            room.DeviceDescription = description;
            room.addControl();
            room.FloorId = FloorId;
            rooms.Add(room);
        }
        public void addDoor(Point location, int rotateState, string deviceId, string deviceName, string description, bool power) {
            DupDoor door = new DupDoor(mainPanel, owner, power);
            door.Predefined = true;
            door.RotateState = rotateState;
            door.doorLocation = location;
            door.DeviceId = deviceId;
            door.DeviceName = deviceName;
            door.DeviceDescription = description;
            door.addControl();
            doors.Add(door);
        }
        public void addWindow(Point location, int rotateState, string deviceId = null, string deviceName = null, string description = null, bool power = false) {
            DupWindow window = new DupWindow(mainPanel, owner, power);
            window.Predefined = true;
            window.RotateState = rotateState;
            window.windowLocation = location;
            window.DeviceId = deviceId;
            window.DeviceName = deviceName;
            window.DeviceDescription = description;
            window.addControl();
            windows.Add(window);
        }
        public void addPowerOutlet(Point location, string deviceId, string deviceName, string description, bool power) {
            DupPowerOutLet powerOutlet = new DupPowerOutLet(mainPanel, owner, power);
            powerOutlet.Predefined = true;
            powerOutlet.powerOutletLocation = location;
            powerOutlet.DeviceId = deviceId;
            powerOutlet.DeviceName = deviceName;
            powerOutlet.DeviceDescription = description;
            powerOutlet.addControl();
            powerOutlets.Add(powerOutlet);
        }
        public void addLight(Point location, string deviceId, string deviceName, string description, bool power) {
            DupLight light = new DupLight(mainPanel, owner, power);
            light.Predefined = true;
            light.lightLocation = location;
            light.DeviceId = deviceId;
            light.DeviceName = deviceName;
            light.DeviceDescription = description;
            light.addControl();
            lights.Add(light);
        }
        public void addWaterTap(Point location, string deviceId = null, string deviceName = null, string description = null, bool power = false) {
            DupWaterTap waterTap = new DupWaterTap(mainPanel, owner, power);
            waterTap.Predefined = true;
            waterTap.waterTapLocation = location;
            waterTap.DeviceId = deviceId;
            waterTap.DeviceName = deviceName;
            waterTap.DeviceDescription = description;
            waterTap.addControl();
            waterTaps.Add(waterTap);
        }

        public int getCountByDevice(DeviceInFloor device) {
            int count = 0;
            switch (device) {
                case DeviceInFloor.Door:
                    break;
                case DeviceInFloor.Lights:
                    break;
                case DeviceInFloor.PowerOutlet:
                    break;
                case DeviceInFloor.Rooms:
                    break;
                case DeviceInFloor.WaterTap:
                    break;
                case DeviceInFloor.Window:
                    break;
            }
            return count;
        }

        public void getRooms() {
            RoomData roomData = api.Rooms(auth_token, int.Parse(FloorId));
            if (roomData.status && roomData.response_code == 200) {
                foreach (dynamic dyn in roomData.getRooms) {
                    addRoom(dyn.Item4, dyn.Item5, dyn.Item6, dyn.Rest.Item2.ToString(), dyn.Item2, dyn.Item3, (dyn.Rest.Item1 == 0) ? false : true);
                    rooms[rooms.Count - 1].RoomId = dyn.Item1.ToString();
                    rooms[rooms.Count - 1].api = api;
                    rooms[rooms.Count - 1].auth_token = auth_token;
                }
                disableEditing();
            } else if (roomData.response_code == 410) {
                if (api.reAuthenticate()) {
                    getRooms();
                } else {
                    Application.Restart();
                }
            } else {
                return;
            }
        }

        public void getDevices(int roomId = 0) {
            UserDeviceData data = api.getUserDevice(auth_token, int.Parse(FloorId), 0, roomId);
            if (data.status && data.response_code == 200) {
                foreach (dynamic dyn in data.getDevices) {
                    string isRoom = dyn.Item7.ToString().Split(',')[3];
                    if (isRoom == "True" && roomId == 0) {
                        continue;
                    }

                    string devType = dyn.Item6.ToString();
                    switch (devType) {
                        case "Door":
                            getDoors(dyn);
                            break;
                        case "Power Outlet":
                            getPowerOutlets(dyn);
                            break;
                        case "Window":
                            getWindows(dyn);
                            break;
                        case "Light":
                            getLights(dyn);
                            break;
                        case "Water Tap":
                            getWaterTaps(dyn);
                            break;
                    }
                }
            } else if (data.response_code == 410) {
                if (api.reAuthenticate()) {
                    getDevices();
                } else {
                    Application.Restart();
                }
            } else {
                disableEditing();
                return;
            }
            disableEditing();
        }
        public void getLights(dynamic data) {
            string[] sepData = data.Item7.ToString().Split(',');

            bool power = false;
            if (sepData[0] == "1") power = true;

            addLight(data.Item5, data.Item1.ToString(), data.Item3, data.Item4, power);
            lights[lights.Count - 1].api = api;
            lights[lights.Count - 1].auth_token = auth_token;
            lights[lights.Count - 1].floorId = FloorId;
        }
        public void getDoors(dynamic data) {
            string[] sepData = data.Item7.ToString().Split(',');

            bool power = false;
            if (sepData[0] == "1") power = true;

            addDoor(data.Item5, int.Parse(sepData[2]), data.Item1.ToString(), data.Item3, data.Item4, power);
            doors[doors.Count - 1].api = api;
            doors[doors.Count - 1].auth_token = auth_token;
            doors[doors.Count - 1].floorId = FloorId;
        }
        public void getWindows(dynamic data) {
            string[] sepData = data.Item7.ToString().Split(',');

            bool power = false;
            if (sepData[0] == "1") power = true;

            addWindow(data.Item5, int.Parse(sepData[2]), data.Item1.ToString(), data.Item3, data.Item4, power);
            windows[windows.Count - 1].api = api;
            windows[windows.Count - 1].auth_token = auth_token;
            windows[windows.Count - 1].floorId = FloorId;
        }
        public void getPowerOutlets(dynamic data) {
            string[] sepData = data.Item7.ToString().Split(',');

            bool power = false;
            if (sepData[0] == "1") power = true;

            addPowerOutlet(data.Item5, data.Item1.ToString(), data.Item3, data.Item4, power);
            powerOutlets[powerOutlets.Count - 1].api = api;
            powerOutlets[powerOutlets.Count - 1].auth_token = auth_token;
            powerOutlets[powerOutlets.Count - 1].floorId = FloorId;
        }
        public void getWaterTaps(dynamic data) {
            string[] sepData = data.Item7.ToString().Split(',');

            bool power = false;
            if (sepData[0] == "1") power = true;

            addWaterTap(data.Item5, data.Item1.ToString(), data.Item3, data.Item4, power);
            waterTaps[waterTaps.Count - 1].api = api;
            waterTaps[waterTaps.Count - 1].auth_token = auth_token;
            waterTaps[waterTaps.Count - 1].floorId = FloorId;
        }

        public void enableEditing() {
            foreach (Room ctrl in rooms)
                ctrl.enableEditing();

            foreach (Door ctrl in doors)
                ctrl.enableEditing();

            foreach (Light ctrl in lights)
                ctrl.enableEditing();

            foreach (PowerOutlet ctrl in powerOutlets)
                ctrl.enableEditing();

            foreach (WaterTap ctrl in waterTaps)
                ctrl.enableEditing();

            foreach (Window ctrl in windows)
                ctrl.enableEditing();

            foreach (Room ctrl in rooms)
                ctrl.enableEditing();
        }

        public void disableEditing() {
            foreach (Room ctrl in rooms)
                ctrl.disableEditing();

            foreach (Door ctrl in doors)
                ctrl.disableEditing();

            foreach (Light ctrl in lights)
                ctrl.disableEditing();

            foreach (PowerOutlet ctrl in powerOutlets)
                ctrl.disableEditing();

            foreach (WaterTap ctrl in waterTaps)
                ctrl.disableEditing();

            foreach (Window ctrl in windows)
                ctrl.disableEditing();

            foreach (Room ctrl in rooms)
                ctrl.disableEditing();
        }
    }
}