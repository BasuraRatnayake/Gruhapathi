using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForeRunners.Devices.Design {
    public interface IFloor {
        void addDoor(string deviceId = null, string deviceName = null, string description = null, bool power = false);
        void addRoom(string deviceId = null, string deviceName = null, string description = null, bool power = false);
        void addLight(string deviceId = null, string deviceName = null, string description = null, bool power = false);
        void addWaterTap(string deviceId = null, string deviceName = null, string description = null, bool power = false);
        void addWindow(string deviceId = null, string deviceName = null, string description = null, bool power = false);
        void addPowerOutlet(string deviceId = null, string deviceName = null, string description = null, bool power = false);

        void enableEditing();
        void disableEditing();

        void getRooms();

        void getDevices(int roomId = 0);
        void getLights(dynamic data);
        void getDoors(dynamic data);
        void getWindows(dynamic data);
        void getPowerOutlets(dynamic data);
        void getWaterTaps(dynamic data);
    }
}