using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForeRunners.Devices.Design {
    interface IFloor {
        void addDoor(string deviceId = null, string deviceName = null, string description = null, bool power = false);
        void addRoom(string deviceId = null, string deviceName = null, string description = null, bool power = false);
        void addLight(string deviceId = null, string deviceName = null, string description = null, bool power = false);
        void addWaterTap(string deviceId = null, string deviceName = null, string description = null, bool power = false);
        void addWindow(string deviceId = null, string deviceName = null, string description = null, bool power = false);
        void addPowerOutlet(string deviceId = null, string deviceName = null, string description = null, bool power = false);

        void enableEditing();
        void disableEditing();

        string getRooms();
        string getLights();
        string getDoors();
        string getWindows();
        string getPowerOutlets();
        string getWaterTaps();
    }
}