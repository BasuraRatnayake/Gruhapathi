using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ForeRunners.Devices.Design {
    public class Floor:IFloor {
        private List<Room> rooms;
        private List<Door> doors;
        private List<Light> lights;
        private List<PowerOutlet> powerOutlets;
        private List<WaterTap> waterTaps;
        private List<Window> windows;

        private Panel mainPanel;

        public void enableEditing() {
            foreach(Room ctrl in rooms) 
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

        public string FloorName { get; set; }
        public string FloorId { get; set; }

        public Floor(Panel mainPanel) {
            this.mainPanel = mainPanel;

            rooms = new List<Room>();
            doors = new List<Door>();
            lights = new List<Light>();
            powerOutlets = new List<PowerOutlet>();
            waterTaps = new List<WaterTap>();
            windows = new List<Window>();
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
            Door door = new Door(mainPanel, power);
            door.DeviceId = deviceId;
            door.DeviceName = deviceName;
            door.DeviceDescription = description;
            door.addControl();
            doors.Add(door);
        }
        public void addRoom(string deviceId = null, string deviceName = null, string description = null, bool power = false) {
            Room room = new Room(mainPanel, deviceName, power);
            room.DeviceId = deviceId;
            room.DeviceName = deviceName;
            room.DeviceDescription = description;
            room.addControl();
            rooms.Add(room);     
        }
        public void addLight(string deviceId = null, string deviceName = null, string description = null, bool power = false) {
            Light light = new Light(mainPanel, power);
            light.DeviceId = deviceId;
            light.DeviceName = deviceName;
            light.DeviceDescription = description;
            light.addControl();
            lights.Add(light);
        }
        public void addWaterTap(string deviceId = null, string deviceName = null, string description = null, bool power = false) {
            WaterTap waterTap = new WaterTap(mainPanel, power);
            waterTap.DeviceId = deviceId;
            waterTap.DeviceName = deviceName;
            waterTap.DeviceDescription = description;
            waterTap.addControl();
            waterTaps.Add(waterTap);
        }
        public void addWindow(string deviceId = null, string deviceName = null, string description = null, bool power = false) {
            Window window = new Window(mainPanel, power);
            window.DeviceId = deviceId;
            window.DeviceName = deviceName;
            window.DeviceDescription = description;
            window.addControl();
            windows.Add(window);                
        }
        public void addPowerOutlet(string deviceId = null, string deviceName = null, string description = null, bool power = false) {
            PowerOutlet powerOutlet = new PowerOutlet(mainPanel, power);
            powerOutlet.DeviceId = deviceId;
            powerOutlet.DeviceName = deviceName;
            powerOutlet.DeviceDescription = description;
            powerOutlet.addControl();
            powerOutlets.Add(powerOutlet);
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

        public string getRooms() {
            throw new NotImplementedException();
        }

        public string getLights() {
            throw new NotImplementedException();
        }

        public string getDoors() {
            throw new NotImplementedException();
        }

        public string getWindows() {
            throw new NotImplementedException();
        }

        public string getPowerOutlets() {
            throw new NotImplementedException();
        }

        public string getWaterTaps() {
            throw new NotImplementedException();
        }
    }
}