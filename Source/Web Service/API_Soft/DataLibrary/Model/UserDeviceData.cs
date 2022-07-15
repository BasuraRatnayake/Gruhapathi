using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeRunners.Data.Model {
    public class UserDeviceData : CommonData {

        public dynamic data { get; set; }
        public int floor_id {
            get {
                return data.floor_id;
            }
        }
        public int room_id {
            get {
                return data.room_id;
            }
        }

        public int device_id {
            get {
                return data.device_id;
            }
        }

        private List<Tuple<int, int, string, string, Point, string, string>> device;

        public List<Tuple<int, int, string, string, Point, string, string>> getDevices {
            get {
                device = new List<Tuple<int, int, string, string, Point, string, string>>();
                foreach (dynamic dyn in data.devices) {
                    int devId = dyn.device_id;
                    int uId = dyn.u_deviceId;
                    string devName = dyn.dev_name;
                    string desc = dyn.dev_desc;
                    string type = dyn.dev_type;
                    int isDoor = dyn.is_door;
                    int power = dyn.power_state;
                    int rotate = dyn.rotate_state;
                    bool isRoom = string.IsNullOrWhiteSpace(dyn.isIn_room.ToString()) ? false : true;

                    object[] obj = dyn.location.ToString().Split(',');
                    Point loc = new Point(Int32.Parse(obj[0].ToString()), Int32.Parse(obj[1].ToString()));
                    device.Add(new Tuple<int, int, string, string, Point, string, string>(uId, devId, devName, desc, loc, type, power+","+ isDoor + "," + rotate+","+ isRoom));
                }

                return device;
            }
        }
    }
}
