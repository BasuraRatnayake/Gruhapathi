using ForeRunners.GUI.Buttons;
using ForeRunners.GUI.GoldButton;
using MetroFramework.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gruhapathi.Dialogs {
    public class DeviceDialog {
        private Panel pnlDialogBox;
        private Panel pnlIcon;
        private Panel pnlDialog;
        private Label lblDesT;
        private Label lblDes;
        private Panel pnlTopBanner;
        private Label lblClose;
        private Label lblTitle;

        private MetroForm owner;

        private GoldButton goldBut;

        private ButtonOnOff onOffBut;
        public ButtonOnOff getOnOff { get { return onOffBut; } }

        private int uniqueId;

        private Point dialogLoc;
        public Point DialogLocation {
            get { return dialogLoc; }
            set {
                dialogLoc = value;
                pnlDialogBox.Location = dialogLoc;
            }
        }

        private string description;
        public string DialogDesc {
            get { return description; }
            set {
                description = value;
                lblDesT.Text = description;
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

        private string dialogIcon;
        public string DialogIcon {
            get {
                if (dialogIcon == null) dialogIcon = "light_off";
                return "images/" + dialogIcon + ".png";
            }
            set {
                dialogIcon = value;
                pnlIcon.BackgroundImage = Image.FromFile("images/" + dialogIcon + ".png");                
            }
        }

        private string ownerPanel;
        public string OwnerPanel {
            get { return ownerPanel; }
            set {
                ownerPanel = value;
            }
        }

        private bool hide;

        public DeviceDialog(MetroForm owner, Point dialogLocation, MouseEventHandler offBut, MouseEventHandler onBut, MouseEventHandler butClick, string DialogIcon = null, string DialogTitle = null, string DialogDesc = null, bool hide = true) {
            this.owner = owner;
            this.hide = hide;

            dialogLoc = new Point();

            onOffBut = new ButtonOnOff();
            onOffBut.OnEvent = onBut;
            onOffBut.OffEvent = offBut;

            initControls();
            this.DialogLocation = dialogLocation;
            this.DialogDesc = (DialogDesc == null) ? string.Empty : DialogDesc;
            this.DialogTitle = (DialogTitle == null) ? string.Empty : DialogTitle;
            this.DialogIcon = (DialogIcon == null) ? string.Empty : DialogIcon;

            uniqueId = new Random().Next(100, 1000);

            goldBut.MouseClick += butClick;
        }

        private void initControls() {
            pnlDialogBox = new Panel();
            pnlDialogBox.Location = DialogLocation;
            pnlDialogBox.Name = "pnlDialogBox";
            pnlDialogBox.Size = new Size(276, 134);

            pnlIcon = new Panel();
            pnlIcon.BackgroundImage = Image.FromFile(DialogIcon);
            pnlIcon.BackgroundImageLayout = ImageLayout.Center;
            pnlIcon.Location = new Point(2, 2);
            pnlIcon.Name = "pnlIcon";
            pnlIcon.Size = new Size(35, 39);
            pnlDialogBox.Controls.Add(pnlIcon);

            goldBut = new GoldButton();
            goldBut.Location = new Point(39, 111);
            goldBut.Name = "GoldButton";
            goldBut.Size = new Size(75, 20);
            goldBut.Text = "MORE INFO";
            goldBut.TextAlign = ContentAlignment.MiddleCenter;
            goldBut.Visible = true;
            if(hide)
                pnlDialogBox.Controls.Add(goldBut);

            pnlDialog = new Panel();
            pnlDialog.Location = new Point(39, 2);
            pnlDialog.Name = "pnlDialog";
            pnlDialog.Size = new Size(235, 110);
            pnlDialog.BorderStyle = BorderStyle.None;
            pnlDialog.Paint += (sender2, e2) => {
                Rectangle rect = e2.ClipRectangle;
                rect.Width--;
                rect.Height--;
                e2.Graphics.DrawRectangle(new Pen(ColorTranslator.FromHtml("#f2b600")), rect);
            };
            pnlDialogBox.Controls.Add(pnlDialog);

            onOffBut.Location = new Point(155, 80);
            if (hide)
                pnlDialog.Controls.Add(onOffBut.getButton());

            lblDesT = new Label();
            lblDesT.Font = new Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lblDesT.ForeColor = Color.White;
            lblDesT.Location = new Point(3, 40);
            lblDesT.Name = "lblDesT";
            lblDesT.Size = new Size(227, 28);
            lblDesT.Text = DialogDesc;
            pnlDialog.Controls.Add(lblDesT);

            lblDes = new Label();
            lblDes.AutoSize = true;
            lblDes.Font = new Font("Arial", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblDes.ForeColor = Color.White;
            lblDes.Location = new Point(3, 26);
            lblDes.Name = "lblDes";
            lblDes.Size = new Size(77, 14);
            lblDes.Text = "DESCRIPTION";
            lblDes.TextAlign = ContentAlignment.MiddleLeft;
            pnlDialog.Controls.Add(lblDes);

            pnlTopBanner = new Panel();
            pnlTopBanner.BackColor = Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(174)))), ((int)(((byte)(0)))));
            pnlTopBanner.Dock = DockStyle.Top;
            pnlTopBanner.Location = new Point(0, 0);
            pnlTopBanner.Name = "pnlTopBanner";
            pnlTopBanner.Size = new Size(233, 20);
            pnlDialog.Controls.Add(pnlTopBanner);

            lblClose = new Label();
            lblClose.Dock = DockStyle.Right;
            lblClose.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblClose.ForeColor = Color.Black;
            lblClose.Location = new Point(211, 0);
            lblClose.Name = "lblClose";
            lblClose.Size = new Size(22, 20);
            lblClose.Cursor = Cursors.Hand;
            lblClose.Text = "X";
            lblClose.TextAlign = ContentAlignment.MiddleCenter;
            lblClose.Click += LblClose_Click;
            pnlTopBanner.Controls.Add(lblClose);

            lblTitle = new Label();
            lblTitle.Dock = DockStyle.Left;
            lblTitle.Font = new Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lblTitle.ForeColor = Color.Black;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(119, 20);
            lblTitle.Text = DialogTitle;
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            pnlTopBanner.Controls.Add(lblTitle);
        }

        private void LblClose_Click(object sender, EventArgs e) {
            foreach (Control con in owner.Controls.Find(pnlDialogBox.Name, true))
                owner.Controls.Remove(con);
            pnlDialogBox.Visible = false;

            if(!string.IsNullOrEmpty(OwnerPanel))
                owner.Controls.Find(OwnerPanel, false)[0].Invalidate();
        }

        public void showDialog() {
            foreach (Control con in owner.Controls.Find(pnlDialogBox.Name, true))
                owner.Controls.Remove(con);

            if (!string.IsNullOrEmpty(OwnerPanel))
                owner.Controls.Find(OwnerPanel, false)[0].Invalidate();

            pnlDialogBox.Visible = true;
            owner.Controls.Add(pnlDialogBox);
            pnlDialogBox.BringToFront();
        }
    }
}
