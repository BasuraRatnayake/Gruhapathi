using ForeRunners.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeRunners.Data {
    public interface IAPI {
        Authentication authenticate(string username, string password);
        bool reAuthenticate();

        DeviceData Device(string token, string deviceId = "all", string assigned = "no");

        FloorData Floors(string token);
        FloorData addFloor(string token, string floorName);

        RoomData Rooms(string token, int floorId);
        RoomData addRoom(string token, int floorId, string roomName, string roomDesc, string roomLoc, string roomSize, bool powerState, int rotateState);
        RoomData updateRoom(string token, int floorId, int roomId, string roomName, string roomDesc, string roomLoc, string roomSize, bool powerState, int rotateState);
        bool removeRoom(string token, int floorId, int roomId);

        UserDeviceData updateUserDevice(string token, int devId, string devName, string devDesc, string loc, bool state = false, int rotateState = 1);
        UserDeviceData addUserDevice(string token, int devId, int floorId, int roomId, string devName, string devDesc, string loc, bool state, bool isDoor, int rotateState);
        UserDeviceData getUserDevice(string token, int floorId, int deviceId = 0, int roomId = 0);
        CommonData updateUserDevice_power(string jsonData);
        CommonData add_UsageData(string token, int deviceId, int value);
        UsageData get_UsageData(string token, string start_date, string end_date, string power_type = "E");

        bool add_QRCode(string qrCode);
        Authentication get_QRCode(string qrCode);

        bool change_power(string token, string power_id);
    }
}
