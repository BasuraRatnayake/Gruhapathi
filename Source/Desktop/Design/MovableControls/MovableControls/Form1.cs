using ForeRunners.Devices.Design;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MovableControls {
    public partial class Form1 : Form {       

        public Form1() {
            InitializeComponent();

            //floor = new Floor(panel1);
            //light = new Light(panel1);
            //power = new PowerOutlet(panel1);
            //door = new Door(panel1);
            //water = new WaterTap(panel1);
            //window = new Window(panel1);

            floor = new Floor(panel1);

            panel1.Paint += DeviceControl_Paint;
        }

        public void DeviceControl_Paint(object sender, PaintEventArgs e) {
            Rectangle rect = e.ClipRectangle;
            rect.Width--;
            rect.Height--;
            e.Graphics.DrawRectangle(new Pen(ColorTranslator.FromHtml("#f5b800")), rect);
        }

        List<Room> rooms = new List<Room>();
        List<Door> doors = new List<Door>();
        List<Light> lights = new List<Light>();
        List<PowerOutlet> powerOutlets = new List<PowerOutlet>();
        List<WaterTap> waterTaps = new List<WaterTap>();
        List<Window> windows = new List<Window>();

        Floor floor; Class1 c;

        private void button1_Click(object sender, EventArgs e) {
            //Room room1 = new Room(panel1, "Room " + (rooms.Count + 1));
            //room1.addControl();
            //rooms.Add(room1);
            floor.addRoom("Room ");
        }

        private void button2_Click(object sender, EventArgs e) {
            floor.enableEditing();
        }

        private void button3_Click(object sender, EventArgs e) {
            floor.disableEditing();
            //c.disableEditing();
        }

        private void button4_Click(object sender, EventArgs e) {
            //Console.WriteLine(floor.getRooms());
            c = new Class1(panel1);
            c.addControl();
        }

        private void button7_Click(object sender, EventArgs e) {
            floor.addLight();
            //Light light1 = new Light(panel1);
            //light1.addControl();
            //lights.Add(light1);
        }

        private void button6_Click(object sender, EventArgs e) {
            PowerOutlet power1 = new PowerOutlet(panel1);
            power1.addControl();
            powerOutlets.Add(power1);
        }

        private void button5_Click(object sender, EventArgs e) {
            //Door door1 = new Door(panel1);
            //door1.addControl();
            //doors.Add(door1);

            floor.addDoor();
        }

        private void button8_Click(object sender, EventArgs e) {
            //WaterTap waterTap = new WaterTap(panel1);
            //waterTap.addControl();
            //waterTaps.Add(waterTap);
            floor.addWaterTap();
        }

        private void button9_Click(object sender, EventArgs e) {
            //Window window = new Window(panel1);
            //window.addControl();
            //windows.Add(window);
            floor.addWindow();
        }

        private void button10_Click(object sender, EventArgs e) {
            Room room1 = new Room(panel1, "Room " + (rooms.Count + 1));
            room1.Predefined = true;
            room1.roomSize = new Size(200, 150);
            room1.roomLocation = new Point(100, 50);
            room1.RotateState = 1;
            room1.addControl();
            rooms.Add(room1);
        }
    }
}