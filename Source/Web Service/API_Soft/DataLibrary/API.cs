using ForeRunners.Data.Model;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Net;
using System;

namespace ForeRunners.Data {
    public class API : IAPI {
        protected string api_path;
        public string Path {
            get { return api_path; }
        }

        private ConfigurationFile iniFile;

        public API(string api_path) {
            this.api_path = api_path;

            iniFile = new ConfigurationFile("Gruhapathi.ini");
            iniFile.Write("Path", api_path, "API");
        }

        public bool isInternetAvailable {
            get{
                //try {
                //    using (var client = new WebClient()) {
                //        using (var stream = client.OpenRead("http://www.google.com")) {
                //            return true;
                //        }
                //    }
                //} catch {
                //    return false;
                //}
                return true;
            }            
        }
        public bool isOnline{
            get {
                if (isInternetAvailable) {
                    try {
                        using (var client = new WebClient()) {
                            using (var stream = client.OpenRead(Path)) {
                                return true;
                            }
                        }
                    } catch {
                        return false;
                    }
                } else {
                    return false;
                }
            }                    
        }

        #region Authorization
        public Authentication authenticate(string username, string password) {
            Authentication auth = new Authentication();
            auth.response_code = 404;
            auth.status = false;
            try {
                var client = new RestClient(Path);
                var request = new RestRequest("view/authenticate.php", Method.POST);
                request.AddParameter("username", username);
                request.AddParameter("password", password);

                IRestResponse response = client.Execute(request);
                auth = JsonConvert.DeserializeObject<Authentication>(response.Content.ToString());

                if (auth.status && auth.response_code == 200) {

                    iniFile.Write("Username", auth.auth_username, "Authorization");
                    iniFile.Write("Token", auth.auth_token, "Authorization");
                    iniFile.Write("Refresh_Token", auth.refresh_token, "Authorization");
                    iniFile.Write("Expire", auth.auth_expire.ToString(), "Authorization");
                }

                return auth;
            } catch {
                return auth;
            }
        }

        public bool reAuthenticate() {
            if (File.Exists("Gruhapathi.ini")) {
                string username = iniFile.Read("Username", "Authorization");
                string refreshToken = iniFile.Read("Refresh_Token", "Authorization");

                var client = new RestClient(Path);
                var request = new RestRequest("view/authenticate.php", Method.POST);
                request.AddParameter("username", username);
                request.AddParameter("refresh_token", refreshToken);

                IRestResponse response = client.Execute(request);
                Authentication auth = JsonConvert.DeserializeObject<Authentication>(response.Content.ToString());

                if (auth.status && auth.response_code == 200) {
                    iniFile.Write("Username", auth.auth_username, "Authorization");
                    iniFile.Write("Token", auth.auth_token, "Authorization");
                    iniFile.Write("Refresh_Token", auth.auth_token, "Authorization");
                    iniFile.Write("Expire", auth.auth_expire.ToString(), "Authorization");

                    return true;
                }
            }          
            return false;
        }
        #endregion

        public DeviceData Device(string token, string deviceId = "all", string assigned = "no") {
            var client = new RestClient(Path);
            var request = new RestRequest("view/device/get_device.php", Method.GET);
            request.AddParameter("auth_token", token);
            request.AddParameter("device_id", deviceId);
            request.AddParameter("assigned", assigned);

            IRestResponse response = client.Execute(request);
            DeviceData device = JsonConvert.DeserializeObject<DeviceData>(response.Content.ToString());

            return device;
        }

        public FloorData Floors(string token) {
            var client = new RestClient(Path);
            var request = new RestRequest("view/floor/get_floor.php", Method.GET);
            request.AddParameter("auth_token", token);
            request.AddParameter("data_field", "all");

            IRestResponse response = client.Execute(request);
            FloorData floor = JsonConvert.DeserializeObject<FloorData>(response.Content.ToString());

            return floor;
        }
        public FloorData addFloor(string token, string floorName) {
            var client = new RestClient(Path);
            var request = new RestRequest("view/floor/add_floor.php", Method.POST);
            request.AddParameter("auth_token", token);
            request.AddParameter("floor_name", floorName);
            request.AddParameter("power_state", 0);

            IRestResponse response = client.Execute(request);
            FloorData floor = JsonConvert.DeserializeObject<FloorData>(response.Content.ToString());

            return floor;
        }

        public RoomData Rooms(string token, int floorId) {
            var client = new RestClient(Path);
            var request = new RestRequest("view/room/get_room.php", Method.GET);
            request.AddParameter("auth_token", token);
            request.AddParameter("floor_id", floorId);
            request.AddParameter("data_field", "all");

            IRestResponse response = client.Execute(request);
            RoomData room = JsonConvert.DeserializeObject<RoomData>(response.Content.ToString());

            return room;
        }
        public RoomData addRoom(string token, int floorId, string roomName, string roomDesc, string roomLoc, string roomSize, bool powerState, int rotateState) {
            var client = new RestClient(Path);
            var request = new RestRequest("view/room/add_room.php", Method.POST);
            request.AddParameter("auth_token", token);
            request.AddParameter("floor_id", floorId);
            request.AddParameter("room_name", roomName);
            request.AddParameter("room_desc", roomDesc);
            request.AddParameter("loc_xy", roomLoc);
            request.AddParameter("size_hw", roomSize);
            request.AddParameter("rotate_state", rotateState);
            request.AddParameter("power_state", powerState ? 1 : 0);

            IRestResponse response = client.Execute(request);
            RoomData room = JsonConvert.DeserializeObject<RoomData>(response.Content.ToString());

            return room;
        }
        public RoomData updateRoom(string token, int floorId, int roomId, string roomName, string roomDesc, string roomLoc, string roomSize, bool powerState, int rotateState) {
            var client = new RestClient(Path);
            var request = new RestRequest("view/room/update_room.php", Method.POST);
            request.AddParameter("auth_token", token);
            request.AddParameter("floor_id", floorId);
            request.AddParameter("room_id", roomId);
            request.AddParameter("room_name", roomName);
            request.AddParameter("room_desc", roomDesc);
            request.AddParameter("loc_xy", roomLoc);
            request.AddParameter("size_hw", roomSize);
            request.AddParameter("rotate_state", rotateState);
            request.AddParameter("power_state", powerState);

            IRestResponse response = client.Execute(request);
            RoomData room = JsonConvert.DeserializeObject<RoomData>(response.Content.ToString());

            return room;
        }
        public bool removeRoom(string token, int floorId, int roomId) {
            var client = new RestClient(Path);
            var request = new RestRequest("view/room/remove_room.php", Method.POST);
            request.AddParameter("auth_token", token);
            request.AddParameter("floor_id", floorId);
            request.AddParameter("room_id", roomId);

            IRestResponse response = client.Execute(request);
            RoomData room = JsonConvert.DeserializeObject<RoomData>(response.Content.ToString());

            if(room.status && room.response_code == 200) {
                return true;
            }
            return false;
        }

        public UserDeviceData updateUserDevice(string token, int devId, string devName, string devDesc, string loc, bool state = false, int rotateState = 1) {
            var client = new RestClient(Path);
            var request = new RestRequest("view/udevice/update_udevice.php", Method.POST);
            request.AddParameter("auth_token", token);
            request.AddParameter("device_id", devId);
            request.AddParameter("dev_name", devName);
            request.AddParameter("dev_desc", devDesc);
            request.AddParameter("loc_xy", loc);
            request.AddParameter("power_state", state ? 1 : 0);
            request.AddParameter("rotate_state", rotateState);

            IRestResponse response = client.Execute(request);
            UserDeviceData device = JsonConvert.DeserializeObject<UserDeviceData>(response.Content.ToString());

            return device;
        }

        public UserDeviceData addUserDevice(string token, int devId, int floorId, int roomId, string devName, string devDesc, string loc, bool state, bool isDoor, int rotateState) {
            var client = new RestClient(Path);
            var request = new RestRequest("view/udevice/add_udevice.php", Method.POST);
            request.AddParameter("auth_token", token);
            request.AddParameter("device_id", devId);
            request.AddParameter("floor_id", floorId);

            if(roomId == 0)
                request.AddParameter("room_id", "null");
            else
                request.AddParameter("room_id", roomId);

            request.AddParameter("dev_name", devName);
            request.AddParameter("dev_desc", devDesc);
            request.AddParameter("loc_xy", loc);
            request.AddParameter("power_state", state ? 1 : 0);
            request.AddParameter("is_door", isDoor ? 1 : 0);
            request.AddParameter("rotate_state", rotateState);

            IRestResponse response = client.Execute(request);
            UserDeviceData device = JsonConvert.DeserializeObject<UserDeviceData>(response.Content.ToString());

            return device;
        } 
        public UserDeviceData getUserDevice(string token, int floorId, int deviceId = 0, int roomId = 0) {
            var client = new RestClient(Path);
            var request = new RestRequest("view/udevice/get_udevice.php", Method.GET);
            request.AddParameter("auth_token", token);
            request.AddParameter("floor_id", floorId);

            if (roomId == 0)
                request.AddParameter("room_id", "null"); 
            else
                request.AddParameter("room_id", roomId);

            if (deviceId == 0)
                request.AddParameter("device_id", "null");
            else
                request.AddParameter("device_id", deviceId);

            IRestResponse response = client.Execute(request);
            UserDeviceData device = JsonConvert.DeserializeObject<UserDeviceData>(response.Content.ToString());

            return device;
        }

        public bool add_QRCode(string qrCode) {
            var client = new RestClient(Path);
            var request = new RestRequest("view/altlogin/add_altlogin.php", Method.POST);
            request.AddParameter("qr_code", qrCode);

            IRestResponse response = client.Execute(request);
            CommonData data = JsonConvert.DeserializeObject<CommonData>(response.Content.ToString());

            if(data.status && data.response_code == 200) {
                return true;
            }
            return false;
        }

        public Authentication get_QRCode(string qrCode) {
            var client = new RestClient(Path);
            var request = new RestRequest("view/altlogin/get_pc_altlogin.php", Method.POST);
            request.AddParameter("qr_code", qrCode);

            IRestResponse response = client.Execute(request);
            Authentication auth = JsonConvert.DeserializeObject<Authentication>(response.Content.ToString());

            return auth;
        }

        public CommonData updateUserDevice_power(string jsonData) {
            var client = new RestClient(Path);
            var request = new RestRequest("view/altlogin/update_udevice_power.php", Method.POST);
            request.AddParameter("power_state", jsonData);

            IRestResponse response = client.Execute(request);
            CommonData common = JsonConvert.DeserializeObject<CommonData>(response.Content.ToString());

            return common;
        }

        public CommonData add_UsageData(string token, int deviceId, int value) {
            var client = new RestClient(Path);
            var request = new RestRequest("view/usage/add_usage.php", Method.POST);
            request.AddParameter("auth_token", token);
            request.AddParameter("device_id", deviceId);
            request.AddParameter("usage", value);

            IRestResponse response = client.Execute(request);
            CommonData device = JsonConvert.DeserializeObject<CommonData>(response.Content.ToString());

            return device;
        }
        public UsageData get_UsageData(string token, string start_date, string end_date, string power_type = "E") {
            var client = new RestClient(Path);
            var request = new RestRequest("view/usage/get_usage.php", Method.GET);
            request.AddParameter("auth_token", token);
            request.AddParameter("start_date", start_date);
            request.AddParameter("end_date", end_date);
            request.AddParameter("power_type", power_type);

            IRestResponse response = client.Execute(request);
            UsageData device = JsonConvert.DeserializeObject<UsageData>(response.Content.ToString());

            return device;
        }

        public bool change_power(string token, string power_id) {
            var client = new RestClient(Path);
            var request = new RestRequest("view/udevice/update_udevice_power.php", Method.POST);
            request.AddParameter("auth_token", token);
            power_id = "[{" + power_id + "}]";
            request.AddParameter("power_state", power_id);

            IRestResponse response = client.Execute(request);
            CommonData device = JsonConvert.DeserializeObject<CommonData>(response.Content.ToString());
            if (device.status && device.response_code == 200)
                return true;
            return false;
        }
    }
}