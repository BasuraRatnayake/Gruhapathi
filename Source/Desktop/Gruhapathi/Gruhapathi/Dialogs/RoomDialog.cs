using ForeRunners.GUI.Buttons;
using ForeRunners.GUI.GoldButton;
using MetroFramework.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gruhapathi.Dialogs {
    public class RoomDialog {
        private Panel pnlRoomDialog;
        private Panel pnlContainer;
        private Panel pnlTitleContainer;

        private Label lblPower;
        private Label lblDoor;
        private Label lblDesc;
        private Label lblDescLabel;
        private Label lblClose;
        private Label lblTitle;

        private GoldButton goldBut;

        private ButtonLockUnlock lockUnlockBut;
        public ButtonLockUnlock getLockUnlock { get { return lockUnlockBut; } }
        
        private ButtonOnOff onOffBut;
        public ButtonOnOff getOnOff { get { return onOffBut; } }

        private MetroForm owner;

        private int uniqueId;

        private Point dialogLoc;
        public Point DialogLocation {
            get { return dialogLoc; }
            set {
                dialogLoc = value;
                pnlRoomDialog.Location = dialogLoc;
            }
        }

        private string description;
        public string DialogDesc {
            get { return description; }
            set {
                description = value;
                lblDesc.Text = description;
            }
        }

        private string dialogTit;
        public string DialogTitle {
            get { return dialogTit; }
            set {
                dialogTit = value;
                lblTitle.Text = dialogTit;
            }
        }

        private string ownerPanel;
        public string OwnerPanel {
            get { return ownerPanel; }
            set {
                ownerPanel = value;
            }
        }

        public RoomDialog(MetroForm owner, Point dialogLocation, MouseEventHandler offBut, MouseEventHandler onBut, MouseEventHandler unlockBut, MouseEventHandler lockBut, MouseEventHandler butClick, string DialogTitle = null, string DialogDesc = null) {
            this.owner = owner;

            dialogLoc = new Point();

            lockUnlockBut = new ButtonLockUnlock();
            lockUnlockBut.LockEvent = lockBut;
            lockUnlockBut.UnlockEvent = unlockBut;

            onOffBut = new ButtonOnOff();
            onOffBut.OnEvent = onBut;
            onOffBut.OffEvent = offBut;

            initControls();
            this.DialogLocation = dialogLocation;
            this.DialogDesc = (DialogDesc == null)? string.Empty: DialogDesc;
            this.DialogTitle = (DialogTitle == null) ? string.Empty : DialogTitle;

            uniqueId = new Random().Next(100, 1000);

            goldBut.MouseClick += butClick;
        }

        private void initControls() {
            pnlRoomDialog = new Panel();
            pnlRoomDialog.BackColor = Color.Transparent;
            pnlRoomDialog.Location = DialogLocation;
            pnlRoomDialog.Name = "pnlDialogBox";
            pnlRoomDialog.Size = new Size(256, 146);
            pnlRoomDialog.Visible = false;

            pnlContainer = new Panel();
            pnlContainer.BackColor = Color.Transparent;
            pnlContainer.Location = new Point(0, 0);
            pnlContainer.Name = "pnlContainer";
            pnlContainer.BorderStyle = BorderStyle.None;
            pnlContainer.Size = new Size(256, 121);
            pnlContainer.Visible = true;
            pnlRoomDialog.Controls.Add(pnlContainer);
            pnlContainer.Paint += (sender2, e2) => {
                Rectangle rect = e2.ClipRectangle;
                rect.Width--;
                rect.Height--;
                e2.Graphics.DrawRectangle(new Pen(ColorTranslator.FromHtml("#f2b600")), rect);
            };

            lblPower = new Label();
            lblPower.BackColor = Color.Transparent;
            lblPower.BackColor = ColorTranslator.FromHtml("#111111");
            lblPower.Font = new Font("Arial", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblPower.ForeColor = Color.White;
            lblPower.Location = new Point(154, 65);
            lblPower.Name = "label18";
            lblPower.Size = new Size(45, 14);
            lblPower.Text = "POWER";
            pnlContainer.Controls.Add(lblPower);

            lblDoor = new Label();
            lblDoor.AutoSize = true;
            lblDoor.BackColor = ColorTranslator.FromHtml("#111111");
            lblDoor.Font = new Font("Arial", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblDoor.ForeColor = Color.White;
            lblDoor.Location = new Point(18, 65);
            lblDoor.Name = "label17";
            lblDoor.Size = new Size(37, 14);
            lblDoor.Text = "DOOR";
            pnlContainer.Controls.Add(lblDoor);

            lblDesc = new Label();
            lblDesc.AutoSize = true;
            lblDesc.BackColor = ColorTranslator.FromHtml("#111111");
            lblDesc.Font = new Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lblDesc.ForeColor = Color.White;
            lblDesc.Location = new Point(7, 39);
            lblDesc.Name = "lblDesc";
            lblDesc.Size = new Size(125, 14);
            lblDesc.Text = DialogDesc;
            pnlContainer.Controls.Add(lblDesc);

            lblDescLabel = new Label();
            lblDescLabel.AutoSize = true;
            lblDescLabel.BackColor = ColorTranslator.FromHtml("#111111");
            lblDescLabel.Font = new Font("Arial", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblDescLabel.ForeColor = Color.White;
            lblDescLabel.BorderStyle = BorderStyle.None;
            lblDescLabel.Location = new Point(7, 25);
            lblDescLabel.Name = "lblDescLabel";
            lblDescLabel.Size = new Size(83, 14);
            lblDescLabel.Text = "DESCRIPTION: ";
            pnlContainer.Controls.Add(lblDescLabel);

            pnlTitleContainer = new Panel();
            pnlTitleContainer.BackColor = Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            pnlTitleContainer.Dock = DockStyle.Top;
            pnlTitleContainer.Location = new Point(0, 0);
            pnlTitleContainer.Name = "panel12";
            pnlTitleContainer.Size = new Size(256, 20);
            pnlContainer.Controls.Add(pnlTitleContainer);

            lblClose = new Label();
            lblClose.BackColor = Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            lblClose.Cursor = Cursors.Hand;
            lblClose.Font = new Font("Arial", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblClose.ForeColor = Color.Black;
            lblClose.ImageAlign = ContentAlignment.MiddleLeft;
            lblClose.Location = new Point(237, 1);
            lblClose.Name = "lblClose";
            lblClose.Size = new Size(14, 19);
            lblClose.Text = "X";
            lblClose.TextAlign = ContentAlignment.MiddleCenter;
            lblClose.Click += LblClose_Click;
            pnlTitleContainer.Controls.Add(lblClose);

            lblTitle = new Label();
            lblTitle.BackColor = Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            lblTitle.Font = new Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lblTitle.ForeColor = Color.Black;
            lblTitle.ImageAlign = ContentAlignment.MiddleLeft;
            lblTitle.Location = new Point(7, 3);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(201, 19);
            lblTitle.Text = DialogTitle;
            pnlTitleContainer.Controls.Add(lblTitle);

            lockUnlockBut.Location = new Point(21, 82);
            pnlContainer.Controls.Add(lockUnlockBut.getButton());

            onOffBut.Location = new Point(158, 82);
            pnlContainer.Controls.Add(onOffBut.getButton());

            goldBut = new GoldButton();
            goldBut.Cursor = Cursors.Hand;
            goldBut.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            goldBut.ForeColor = Color.Black;
            goldBut.Location = new Point(0, 121);
            goldBut.Name = "GoldButton";
            goldBut.Size = new Size(106, 24);
            goldBut.Text = "GO TO ROOM";
            goldBut.TextAlign = ContentAlignment.MiddleCenter;
            goldBut.Visible = true;
            pnlRoomDialog.Controls.Add(goldBut);

            owner.Controls.Add(pnlRoomDialog);
        }

        private void LblClose_Click(object sender, EventArgs e) {
            foreach (Control con in owner.Controls.Find(pnlRoomDialog.Name, true))
                owner.Controls.Remove(con);
            pnlRoomDialog.Visible = false;
            owner.Controls.Find(OwnerPanel, false)[0].Invalidate();
        }

        public void showDialog() {
            foreach (Control con in owner.Controls.Find(pnlRoomDialog.Name, true))
                owner.Controls.Remove(con);

            owner.Controls.Find(OwnerPanel, false)[0].Invalidate();

            pnlRoomDialog.Visible = true;
            owner.Controls.Add(pnlRoomDialog);
            pnlRoomDialog.BringToFront();
        }
    }
}