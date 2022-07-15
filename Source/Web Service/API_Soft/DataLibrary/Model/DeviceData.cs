using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeRunners.Data.Model {
    public class DeviceData : CommonData {
        public dynamic data { get; set; }
        private List<Tuple<int, string, string, string, string>> devices;

        public List<Tuple<int, string, string, string, string>> getDevices {
            get {
                devices = new List<Tuple<int, string, string, string, string>>();
                foreach (dynamic dyn in data.devices) {
                    int id = dyn.device_id;
                    string verId = dyn.version;
                    string serial = dyn.device_serial;
                    string type = dyn.device_type;
                    string assigned = dyn.device_assigned;

                    devices.Add(new Tuple<int, string, string, string, string>(id, verId, serial, type, assigned));
                }
                return devices;
            }
        }
    }
}
