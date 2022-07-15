using System.Drawing;

namespace ForeRunners.GUI.Buttons {
    public class Buttons  {

        private string deviceId;
        public string DeviceId {
            get { return deviceId; }
            set { deviceId = value; }
        }

        public virtual Point Location {
            get; set;
        }

        public Color Button_On_Color {
            get { return Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(174)))), ((int)(((byte)(0))))); }
        }

        public Color Button_Off_Color {
            get { return Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17))))); }
        }
    }
}
