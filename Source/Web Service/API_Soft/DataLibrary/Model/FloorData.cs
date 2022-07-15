using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeRunners.Data.Model {
    public class FloorData : CommonData {
        public dynamic data { get; set; }
        public int house_id {
            get {
                return data.house_id;
            }
        }

        public int floor_id {
            get {
                return data.floor_id;
            }
        }

        private List<Tuple<int, string, int>> floors;

        public List<Tuple<int, string, int>> getFloors{
            get {
                floors = new List<Tuple<int, string, int>>();
                foreach (dynamic dyn in data.floors) {
                    int id = dyn.floor_id;
                    string name = dyn.floor_name;
                    int power = dyn.floor_power;

                    floors.Add(new Tuple<int, string, int>(id, name, power));
                }
                return floors;
            }            
        }
    }
}
