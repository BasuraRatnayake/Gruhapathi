using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ForeRunners.Devices.Design {
    public class Window:CoreControls {

        private string uniqId;
        private Panel window = new Panel();

        public bool powerState { get; set; }
        public Point windowLocation {
            get { return window.Location; }
            set { window.Location = value; }
        }
        public void changePowerState(bool power) {
            powerState = power;
            if (powerState)
                window.BackColor = ColorTranslator.FromHtml("#f2b600");
            else
                window.BackColor = Color.Transparent;
        }

        public int RotateState {
            get; set;
        }

        public Window(Panel mainPanel, bool power= false) {
            uniqId = "Window" + (new Random()).Next(100, 1000);
            window = new Panel();

            this.mainPanel = mainPanel;
            this.deviceControl = window;
            this.findControl = "Window";

            initContextMenu(false, true);
            changePowerState(power);

            Predefined = false;
            RotateState = 1;
        }

        public void window_Paint(object sender, PaintEventArgs e) {
            Rectangle rect = e.ClipRectangle;
            rect.Width--;
            rect.Height--;
            Pen pen = new Pen(ColorTranslator.FromHtml("#000000"));
            pen.DashStyle = DashStyle.Dash;
            e.Graphics.DrawRectangle(pen, rect);
        }

        public void addControl() {
            window.Name = uniqId;
            window.Size = new Size(30, 5);
            window.Paint += window_Paint;
            window.BringToFront();

            if (Predefined == false) {
                window.Location = new Point(10, 10);
            }

            changePowerState(powerState);

            if (RotateState == 1) {
                window.Size = new Size(30, 5);
            } else {
                if (RotateState == 2) {
                    window.Size = new Size(5, 30);
                } else if (RotateState == 3) {
                    window.Size = new Size(30, 5);
                } else if (RotateState == 4) {
                    window.Size = new Size(5, 30);
                }
            }

            window.MouseUp += control_MouseUp;
            window.MouseMove += control_MouseMove;
            window.MouseClick += control_Click;

            mainPanel.Controls.Add(window);
            enableEditing();
        }

        public override void control_Click(object sender, MouseEventArgs e) { throw new NotImplementedException(); }
        ~Window() { }
    }
}