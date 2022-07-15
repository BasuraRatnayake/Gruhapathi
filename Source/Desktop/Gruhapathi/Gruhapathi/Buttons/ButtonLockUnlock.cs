using System.Drawing;
using System.Windows.Forms;

namespace ForeRunners.GUI.Buttons {
    public class ButtonLockUnlock : Buttons {

        private Label lblUnlock;
        private Label lblLock;

        private Panel but;

        private Point butLocation;

        private bool butPower;
        public bool ButPowerState {
            get { return butPower; }
        }

        public ButtonLockUnlock() {
            lblUnlock = new Label();
            lblLock = new Label();

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

        private MouseEventHandler unlockBut;
        public MouseEventHandler UnlockEvent {
            get { return unlockBut; }
            set {
                unlockBut = value;
                Unlock();
                lblUnlock.MouseClick += unlockBut;
            }
        }
        
        private MouseEventHandler lockBut;
        public MouseEventHandler LockEvent {
            get { return lockBut; }
            set {
                lockBut = value;
                Lock();
                lblLock.MouseClick += lockBut;
            }
        }

        private void initButton() {
            lblUnlock.BackColor = Button_On_Color;
            lblUnlock.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblUnlock.ForeColor = Color.White;
            lblUnlock.Location = new Point(1, 1);
            lblUnlock.Size = new Size(55, 18);
            lblUnlock.Text = "UNLOCK";
            lblUnlock.TextAlign = ContentAlignment.MiddleCenter;
            lblUnlock.MouseClick += UnlockEvent;

            lblLock.BackColor = Button_Off_Color;
            lblLock.Font = lblUnlock.Font;
            lblLock.ForeColor = Color.Black;
            lblLock.Location = new Point(57, 1);
            lblLock.Size = new Size(43, 18);
            lblLock.Text = "LOCK";
            lblLock.TextAlign = ContentAlignment.MiddleCenter;
            lblLock.MouseClick += LockEvent;

            but.BorderStyle = BorderStyle.None;
            but.Location = butLocation;
            but.Size = new Size(101, 20);
            but.Controls.Add(lblLock);
            but.Controls.Add(lblUnlock);
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
        
        public void Unlock() {
            lblLock.BackColor = Button_Off_Color;
            lblLock.ForeColor = Color.White;

            lblUnlock.BackColor = Button_On_Color;
            lblUnlock.ForeColor = Color.Black;

            butPower = false;
        }
        public void Lock() {
            lblLock.BackColor = Button_On_Color;
            lblLock.ForeColor = Color.Black;

            lblUnlock.BackColor = Button_Off_Color;
            lblUnlock.ForeColor = Color.White;

            butPower = true;
        }
    }
}