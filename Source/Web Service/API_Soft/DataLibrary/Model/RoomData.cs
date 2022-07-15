using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeRunners.Data.Model {
    public class RoomData : CommonData {

        public dynamic data { get; set; }
        public int floor_id {
            get {
                return data.floor_id;
            }
        }
        private List<Tuple<int, string, string, Point, Size, int, int, Tuple<int, int>>> rooms;

        public List<Tuple<int, string, string, Point, Size, int, int, Tuple<int, int>>> getRooms {
            get {
                rooms = new List<Tuple<int, string, string, Point, Size, int, int, Tuple<int, int>>>();
                rooms.Clear();
                foreach (dynamic dyn in data.rooms) {
                    int id = dyn.room_id;
                    string name = dyn.room_name;
                    string desc = dyn.room_desc;

                    object[] obj = dyn.room_loc.ToString().Split(',');
                    Point loc = new Point(Int32.Parse(obj[0].ToString()), Int32.Parse(obj[1].ToString()));

                    obj = dyn.room_size.ToString().Split(',');
                    Size size = new Size(Int32.Parse(obj[0].ToString()), Int32.Parse(obj[1].ToString()));

                    int rotate = dyn.rotate_state;
                    int power = dyn.power_state;
                    int doorState = dyn.door_state;
                    int udeviceId = dyn.device_id;

                    rooms.Add(new Tuple<int, string, string, Point, Size, int, int, Tuple<int, int>>(id, name, desc, loc, size, rotate, power, new Tuple<int, int>(doorState, udeviceId)));
                }

                return rooms;
            }
        }
    }
}
