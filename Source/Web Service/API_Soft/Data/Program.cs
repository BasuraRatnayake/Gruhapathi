using ForeRunners.Data;
using ForeRunners.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data {
    class Program {

        static void Main(string[] args) {
            API api = new API("http://localhost/API/");
            if (api.isOnline) {

                Authentication auth = api.authenticate("BasuraR", "mywins13");
                if (auth.status && auth.response_code == 200) {
                    //FloorData floor = api.Floors(auth.auth_token);
                    //RoomData room = api.Rooms(auth.auth_token, 35);
                    //dynamic dy = room.getRooms;
                    //DeviceData devices = api.Device(auth.auth_token);       

                    //RoomData room = api.addRoom(auth.auth_token, 35, "asdasd", "asdasd", "12,12", "50,60", false, 2);
                    //UserDeviceData data = api.addUserDevice(auth.auth_token, 8, 35, 7, "Room Door", "Door Device of Room", "0,0", false);
                    //RoomData room = api.updateRoom(auth.auth_token, 35, 14, "My Room", "My Room Desc", "305,278", "129,62", false, 4);

                    bool data = api.add_QRCode("asdasdasd");

                    //api.add_QRCode("asdasd");
                    //api.get_UsageData(auth.auth_token, "2017-4-16", "2017-4-30", "W");


                    Console.WriteLine();
                } 
            }
        }
    }
}