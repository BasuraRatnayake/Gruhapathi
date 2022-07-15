using System;
using System.Drawing;
using System.Windows.Forms;

namespace ForeRunners.Devices.Design {
    public class CoreControls{

        public Panel mainPanel { get; set; }
        public Control deviceControl { get; set; }
        public string findControl { get; set; }

        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceDescription { get; set; }

        private bool drag;
        private bool resize;

        private int rotateState = 1;

        private Point MouseLocation;

        public void DeviceControl_Paint(object sender, PaintEventArgs e) {
            Rectangle rect = e.ClipRectangle;
            rect.Width--;
            rect.Height--;
            e.Graphics.DrawRectangle(new Pen(ColorTranslator.FromHtml("#f5b800")), rect);
        }
        public void Door_Paint(object sender, PaintEventArgs e) {
            Rectangle rect = e.ClipRectangle;
            rect.Width--;
            rect.Height--;
            e.Graphics.DrawRectangle(new Pen(ColorTranslator.FromHtml("#000000")), rect);
        }

        public Point DoorLocation(Control panel, Control door) {
            return new Point(panel.Width / 2 - door.Width / 2, panel.Height - 5);
        }

        public virtual void control_Click(object sender, MouseEventArgs e) { throw new NotImplementedException(); }
        public void control_MouseUp(object sender, MouseEventArgs e) {
            drag = false;
            if (resize) {
                Panel door = (Panel)deviceControl.Controls[0];
                if (rotateState == 1) {
                    door.Size = new Size(30, 5);
                    door.Location = DoorLocation(deviceControl, door);
                } else if (rotateState == 2) {
                    door.Size = new Size(5, 30);
                    door.Location = new Point(-1, deviceControl.Height / 2 - door.Height / 2);
                } else if (rotateState == 3) {
                    door.Size = new Size(30, 5);
                    door.Location = new Point(deviceControl.Width / 2 - door.Width / 2, -1);
                } else if (rotateState == 4) {
                    door.Size = new Size(5, 30);
                    door.Location = new Point(deviceControl.Width - 5, deviceControl.Height / 2 - door.Height / 2);
                }
            }
            resize = false;
        }
        public void control_MouseMove(object sender, MouseEventArgs e) {
            if (drag) {
                deviceControl.Cursor = Cursors.SizeAll;
                bool moveOkay = true;

                int[] curLocation = new int[] { e.X + deviceControl.Left - MouseLocation.X, e.Y + deviceControl.Top - MouseLocation.Y };
                int[] allowLocation = new int[] { mainPanel.Width - deviceControl.Width, mainPanel.Height - deviceControl.Height };

                if (curLocation[0] >= allowLocation[0] + 1 || curLocation[0] < -1)
                    moveOkay = false;

                if (curLocation[1] >= allowLocation[1] + 1 || curLocation[1] < -1)
                    moveOkay = false;

                if (moveOkay)
                    deviceControl.Location = new Point(curLocation[0], curLocation[1]);
                mainPanel.Invalidate();
            } else if (resize) {
                deviceControl.Cursor = Cursors.Cross;
                bool sizeOkay = true;

                if (e.X >= 150 || e.Y >= 150)
                    sizeOkay = false;

                if (e.X <= 40 || e.Y <= 40)
                    sizeOkay = false;

                if (sizeOkay) {
                    deviceControl.Size = new Size(e.X + 5, e.Y + 5);
                }
                deviceControl.Invalidate();
                mainPanel.Invalidate();
            }
        }        
        public void rotateRoom() {
            if (findControl == "Door")
                deviceControl.Size = new Size(deviceControl.Height, deviceControl.Width);
            else if (findControl == "Window")
                deviceControl.Size = new Size(deviceControl.Height, deviceControl.Width);
            else {
                Panel door = new Panel();
                deviceControl.Size = new Size(deviceControl.Height, deviceControl.Width);

                door = (Panel)deviceControl.Controls.Find("pnlDoor", true)[0];

                if (rotateState == 1) {
                    door.Size = new Size(30, 5);
                    door.Location = DoorLocation(deviceControl, door);
                } else if (rotateState == 2) {
                    door.Size = new Size(5, 30);
                    door.Location = new Point(-1, deviceControl.Height / 2 - door.Height / 2);
                } else if (rotateState == 3) {
                    door.Size = new Size(30, 5);
                    door.Location = new Point(deviceControl.Width / 2 - door.Width / 2, -1);
                } else if (rotateState == 4) {
                    door.Size = new Size(5, 30);
                    door.Location = new Point(deviceControl.Width - 5, deviceControl.Height / 2 - door.Height / 2);
                }

                if (rotateState >= 4)
                    rotateState = 0;
                rotateState++;
            }

            mainPanel.Invalidate();
            deviceControl.Invalidate();
        }

        public virtual void disableEditing() {
            foreach (var lbl in mainPanel.Controls.Find(deviceControl.Name, true)) {
                if (deviceControl.Name.Contains("pnlRoom"))
                    lbl.Controls[1].MouseClick += control_Click;
                lbl.ContextMenuStrip = null;
                lbl.MouseClick += control_Click;
            }
        }
        public virtual void enableEditing() {
            foreach (var lbl in mainPanel.Controls.Find(deviceControl.Name, true)) {
                if(deviceControl.Name.Contains("pnlRoom"))
                    lbl.Controls[1].MouseClick -= control_Click;
                lbl.ContextMenuStrip = conMenu;
                lbl.MouseClick -= control_Click;
            }
        }

        #region ContextMenu
        private ContextMenuStrip conMenu;
        public void initContextMenu(bool blResize = false, bool blRotate = false) {
            conMenu = new ContextMenuStrip();
            ToolStripMenuItem moveToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem resizeToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem rotateToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem deleteToolStripMenuItem = new ToolStripMenuItem();

            moveToolStripMenuItem.Name = "moveToolStripMenuItem";
            moveToolStripMenuItem.Size = new Size(108, 22);
            moveToolStripMenuItem.Text = "Move";
            moveToolStripMenuItem.Click += new EventHandler(moveToolStripMenuItem_Click);

            resizeToolStripMenuItem.Name = "resizeToolStripMenuItem";
            resizeToolStripMenuItem.Size = new Size(108, 22);
            resizeToolStripMenuItem.Text = "Resize";
            resizeToolStripMenuItem.Click += new EventHandler(resizeToolStripMenuItem_Click);
            resizeToolStripMenuItem.Enabled = blResize;

            rotateToolStripMenuItem.Name = "rotateToolStripMenuItem";
            rotateToolStripMenuItem.Size = new Size(108, 22);
            rotateToolStripMenuItem.Click += new EventHandler(rotateToolStripMenuItem_Click);
            rotateToolStripMenuItem.Text = "Rotate";
            rotateToolStripMenuItem.Enabled = blRotate;

            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(108, 22);
            deleteToolStripMenuItem.Click += new EventHandler(DeleteToolStripMenuItem_Click);
            deleteToolStripMenuItem.Text = "Delete";

            conMenu.Items.AddRange(new ToolStripItem[] { moveToolStripMenuItem, resizeToolStripMenuItem, rotateToolStripMenuItem, deleteToolStripMenuItem });
            conMenu.Name = "conMenu";
            conMenu.Size = new Size(153, 92);
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Are You Sure to Want Remove this Device ?", "Gruhapathi Control Panel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                if (deviceControl.Controls.Count == 0)
                    mainPanel.Controls.Remove(deviceControl);
                else
                    if (deviceControl.Name != mainPanel.Name)
                        mainPanel.Controls.Remove(deviceControl);

                mainPanel.Invalidate();
            }
        }

        private void moveToolStripMenuItem_Click(object sender, EventArgs e) {
            drag = true;
        }
        private void rotateToolStripMenuItem_Click(object sender, EventArgs e) {
            rotateRoom();
        }
        private void resizeToolStripMenuItem_Click(object sender, EventArgs e) {
            resize = true;
        }
        #endregion
    }
}