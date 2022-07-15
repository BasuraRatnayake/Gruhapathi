using System.Drawing;
using System.Windows.Forms;

namespace ForeRunners.GUI.Buttons {
    public class ButtonOnOff : Buttons {

        private Label lblOff;
        private Label lblOn;

        private Panel but;

        private Point butLocation;

        private bool butPower;
        public bool ButPowerState {
            get { return butPower; }
        }

        public ButtonOnOff() {
            lblOff = new Label();
            lblOn = new Label();

            but = new Panel();

            butLocation = new Point();
            initButton();
        }

        public override Point Location {
            get { return butLocation; }
            set {
                butLocation = value;
                but.Location = butLocation;
            }
        }

        private MouseEventHandler offBut;
        public MouseEventHandler OffEvent {
            get { return offBut; }
            set {
                offBut = value;
                Off();
                lblOff.MouseClick += offBut;
            }
        }

        private MouseEventHandler onBut;
        public MouseEventHandler OnEvent {
            get { return onBut; }
            set {
                onBut = value;
                On();
                lblOn.MouseClick += onBut;
            }
        }

        private void initButton() {
            lblOn.BackColor = Button_Off_Color;
            lblOn.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblOn.ForeColor = Color.White;
            lblOn.Location = new Point(1, 1);
            lblOn.Size = new Size(31, 18);
            lblOn.Text = "ON";
            lblOn.TextAlign = ContentAlignment.MiddleCenter;
            lblOn.MouseClick += OnEvent;

            lblOff.BackColor = Button_On_Color;
            lblOff.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblOff.ForeColor = Color.Black;
            lblOff.Location = new Point(34, 1);
            lblOff.Size = new Size(37, 18);
            lblOff.Text = "OFF";
            lblOff.TextAlign = ContentAlignment.MiddleCenter;
            lblOff.MouseClick += OffEvent;

            but.BorderStyle = BorderStyle.None;
            but.Location = butLocation;
            but.Size = new Size(72, 20);
            but.Controls.Add(lblOn);
            but.Controls.Add(lblOff);
            but.Paint += (sender, e) => {
                Rectangle rect = e.ClipRectangle;
                rect.Width--;
                rect.Height--;
                e.Graphics.DrawRectangle(new Pen(ColorTranslator.FromHtml("#f2b600")), rect);
            };
        }

        public Panel getButton() {
            return but;
        }

        public void Off() {
            lblOn.BackColor = Button_Off_Color;
            lblOn.ForeColor = Color.White;

            lblOff.BackColor = Button_On_Color;
            lblOff.ForeColor = Color.Black;

            butPower = false;
        }
        public void On() {
            lblOn.BackColor = Button_On_Color;
            lblOn.ForeColor = Color.Black;

            lblOff.BackColor = Button_Off_Color;
            lblOff.ForeColor = Color.White;

            butPower = true;
        }
    }
}
