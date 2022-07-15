using MetroFramework.Forms;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ForeRunners.GUI;
using System.ComponentModel;
using ForeRunners.GUI.Calender;
using Gruhapathi.Dialogs;

namespace Gruhapathi {
    public class GUICore {

        private MetroForm frmOwner;
        private Timer timer;

        private Calender calender;

        public string activateCommand { get; set; }

        public GUICore(MetroForm form) {
            frmOwner = form;
            frmOwner.FormClosed += FrmOwner_FormClosed;
            frmOwner.Icon = new Icon("images/logo.ico");
            timer = new Timer();
            timer.Start();
        }

        private void FrmOwner_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        #region Calender
        public void setCalender(Point location, int year) {
            calender = new Calender(frmOwner, location, year);
        }
        public string getCalenderDate() {
            return calender.CurrentDate;
        }
        #endregion
        public void pnlI1_Paint(object sender, PaintEventArgs e, string color) {
            Rectangle rect = e.ClipRectangle;
            rect.Width--;
            rect.Height--;
            e.Graphics.DrawRectangle(new Pen(ColorTranslator.FromHtml("#" + color)), rect);
        }

        private void Timer_Tick(object sender, EventArgs e) {
            lblTime.Text = DateTime.Now.ToShortTimeString();
            lblDate.Text = DateTime.Now.ToLongDateString();
        }

        public Image getImage(string image) {
            return Image.FromFile("images/" + image + ".png");
        }
        
        public Font defaultFontBold { get { return new Font("Arial", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))); } }
        public Font defaultFont { get { return new Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))); } }

        public void showHide_Form(MetroForm form, MetroForm parent) {
            form.Show();
            parent.Hide();
        }
        
       
        #region LeftMain Navigation
        public void createCoreGUI(int selectedBut = 0) {
            initNavigation(selectedBut);
            initTopBanner();

            Panel pnlTopBanner = new Panel();
            pnlTopBanner.BackColor = Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(174)))), ((int)(((byte)(0)))));
            pnlTopBanner.Location = new Point(0, -2);
            pnlTopBanner.Name = "pnlTopBanner";
            pnlTopBanner.Size = new Size(800, 7);
            frmOwner.Controls.Add(pnlTopBanner);
        }

        #region TopBanner Panel
        private Panel topBannerLeft;
        private Panel topBannerRight;
        private Label lblTime;
        private Label lblDate;

        private void initTopBanner() {
            buildTBContainer();
            buildTBControls_LeftBanner();
            frmOwner.Controls.Add(topBannerLeft);
            buildTBControls_RightBanner();
            frmOwner.Controls.Add(topBannerRight);
        }
        private void buildTBContainer() {
            topBannerLeft = new Panel();
            topBannerLeft.Location = new Point(10, 11);
            topBannerLeft.Name = "pblTopLeft";
            topBannerLeft.Size = new Size(371, 84);

            topBannerRight = new Panel();
            topBannerRight.Location = new Point(641, 25);
            topBannerRight.Name = "pnlTopRight";
            topBannerRight.Size = new Size(140, 65);
        }
        private void buildTBControls_LeftBanner() {
            PictureBox picLogo = new PictureBox();
            picLogo.Image = getImage("logo"); 
            picLogo.Location = new Point(7, 16);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(98, 53);
            topBannerLeft.Controls.Add(picLogo);

            Label label = new Label();
            label.AutoSize = true;
            label.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            label.ForeColor = Color.White;
            label.Location = new Point(111, 22);
            label.Name = "PName";
            label.Text = "GRUHA PATHI";
            topBannerLeft.Controls.Add(label);

            Label lblControl = new Label();
            lblControl.AutoSize = true;
            lblControl.Font = new Font("Arial", 21.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lblControl.ForeColor = Color.White;
            lblControl.Location = new Point(106, 33);
            lblControl.Name = "lblControlPane";
            lblControl.Text = "CONTROL PANEL";
            topBannerLeft.Controls.Add(lblControl);
        }

        private void buildTBControls_RightBanner() {
            lblDate = new Label();
            lblDate.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lblDate.ForeColor = Color.White;
            lblDate.Location = new Point(2, 43);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(128, 15);
            lblDate.Text = "28 November 2016";
            lblDate.TextAlign = ContentAlignment.MiddleRight;
            lblDate.BringToFront();
            topBannerRight.Controls.Add(lblDate);

            lblTime = new Label();
            lblTime.AutoSize = true;
            lblTime.Font = new Font("Arial", 26.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            lblTime.ForeColor = Color.White;
            lblTime.Location = new Point(35, 7);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(103, 40);
            lblTime.Text = "07:59";
            lblDate.SendToBack();
            topBannerRight.Controls.Add(lblTime);
            timer.Tick += Timer_Tick;
        }

        #endregion
        #region Navigation Panel
        private Panel NavigationPanel;
        private void initNavigation(int selectedBut = 0) {
            buildContainer();
            buildButtons(selectedBut);

            frmOwner.Controls.Add(NavigationPanel);
        }

        #region GUI
        private Panel[] buttons;
        private void buildContainer() {
            NavigationPanel = new Panel();
            NavigationPanel.Location = new Point(0, 116);
            NavigationPanel.Name = "NavigationPanel";
            NavigationPanel.Size = new Size(164, 358);
            NavigationPanel.BackgroundImage = getImage("navBorder"); 
        }
        private void buildButtons(int selectedBut = 0) {
            buttons = new Panel[7];
            string[] butNames = new string[] { "SECURITY", "UTILITY USAGE", "SYSTEM", "SETTINGS", "FLOORS", "DEVICES", "SUMMARY" };
            string[] butImages = new string[] { "security_Un", "electricity", "logo", "floor_h", "floor_h", "power_on", "floor_h" };
            int butY = 272;
            butNames.Reverse();
            for (int i=0;i<buttons.Length;i++){
                buttons[i] = new Panel();
                buttons[i].Cursor = Cursors.Hand;
                buttons[i].Location = new Point(0, butY);
                buttons[i].Name = "pnlButton";
                buttons[i].Size = new Size(162, 37);

                Panel pnlbutIcon = new Panel();
                pnlbutIcon.Location = new Point(6, 4);
                //pnlbutIcon.BackgroundImage = getImage(butImages[i]);
                //pnlbutIcon.BackgroundImageLayout = ImageLayout.Stretch;
                pnlbutIcon.Name = "pnlButIcon";
                pnlbutIcon.Size = new Size(33, 30);

                Label lblBut = new Label();
                lblBut.AutoSize = true;
                lblBut.Font = new Font("Arial", 11.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                lblBut.ForeColor = Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(174)))), ((int)(((byte)(0)))));
                lblBut.Location = new Point(39, 10);
                lblBut.Name = "lblBut";
                lblBut.Text = butNames[i];

                butY -= 36;

                if (selectedBut == i) {
                    lblBut.ForeColor = Color.Black;
                    buttons[i].BackColor = ColorTranslator.FromHtml("#f5b800");
                    pnlbutIcon.Paint += (sender2, e2) => pnlI1_Paint(sender2, e2, "000000");
                } else {
                    buttons[i].MouseEnter += pnlIconBack1_MouseEnter;
                    buttons[i].MouseLeave += pnlIconBack1_MouseLeave;
                    buttons[i].Click += nav_buttonClick;

                    pnlbutIcon.MouseEnter += pnlI1_MouseEnter;
                    pnlbutIcon.MouseLeave += pnlI1_MouseLeave;
                    pnlbutIcon.Click += nav_buttonClick;
                    pnlbutIcon.Paint += (sender2, e2) => pnlI1_Paint(sender2, e2, "f5b800");

                    lblBut.MouseEnter += lblText1_MouseEnter;
                    lblBut.MouseLeave += lblText1_MouseLeave;
                    lblBut.Click += nav_buttonClick;
                }

                buttons[i].Controls.Add(pnlbutIcon);
                buttons[i].Controls.Add(lblBut);
                NavigationPanel.Controls.Add(buttons[i]);
            }
        }
        #endregion
        #region ButtonsEvents
        private void pnlIconBack1_MouseEnter(object sender, EventArgs e) {
            ((Panel)sender).BackColor = ColorTranslator.FromHtml("#f5b800");

            ((Panel)sender).Controls[1].ForeColor = Color.Black;
            ((Panel)sender).Controls[0].Paint += (sender2, e2) => pnlI1_Paint(sender2, e2, "000000");
        }
        private void pnlIconBack1_MouseLeave(object sender, EventArgs e) {
            ((Panel)sender).BackColor = Color.Transparent;

            ((Panel)sender).Controls[1].ForeColor = ColorTranslator.FromHtml("#f5b800");
            ((Panel)sender).Controls[0].Paint += (sender2, e2) => pnlI1_Paint(sender2, e2, "f5b800");
        }
        private void nav_buttonClick(object sender, EventArgs e) {
            Control ctrl = (Control)sender;
            Panel but = null;
            string butName = string.Empty;
            if (ctrl is Label) 
                butName = ctrl.Text;
            else {
                if (ctrl.Name == "pnlButIcon")
                    but = (Panel)ctrl.Parent;
                else
                    but = (Panel)ctrl;
                butName = but.Controls[1].Text;
            }
            nav_Path_Search(butName);
        }
        private void nav_Path_Search(string butName) {
            switch (butName) {
                case "SECURITY":
                    showHide_Form(new ControlPanel.Security(), frmOwner);
                    break;
                case "UTILITY USAGE":
                    showHide_Form(new ControlPanel.Usage(), frmOwner);
                    break;
                case "SYSTEM":
                    showHide_Form(new ControlPanel.GruSys(), frmOwner);
                    break;
                case "SETTINGS":
                    showHide_Form(new ControlPanel.Settings(), frmOwner);
                    break;
                case "FLOORS":
                    showHide_Form(new ControlPanel.Rooms(), frmOwner);
                    break;
                case "DEVICES":
                    showHide_Form(new ControlPanel.Devices(), frmOwner);
                    break;
                case "SUMMARY":
                    showHide_Form(new ControlPanel.Summary(), frmOwner);
                    break;
            }
        }

        private void lblText1_MouseEnter(object sender, EventArgs e) {
            ((Label)sender).ForeColor = Color.Black;

            ((Label)sender).Parent.BackColor = ColorTranslator.FromHtml("#f5b800");
            ((Label)sender).Parent.Controls[0].Paint += (sender2, e2) => pnlI1_Paint(sender2, e2, "000000");
        }
        private void lblText1_MouseLeave(object sender, EventArgs e) {
            ((Label)sender).ForeColor = ColorTranslator.FromHtml("#000000");

            ((Label)sender).Parent.BackColor = Color.Transparent;
            ((Label)sender).Parent.Controls[0].Paint += (sender2, e2) => pnlI1_Paint(sender2, e2, "f5b800");
        }
       
        private void pnlI1_MouseEnter(object sender, EventArgs e) {
            ((Panel)sender).Paint += (sender2, e2) => pnlI1_Paint(sender2, e2, "000000");

            ((Panel)sender).Parent.BackColor = ColorTranslator.FromHtml("#f5b800");
            ((Panel)sender).Parent.Controls[1].ForeColor = Color.Black;
        }
        private void pnlI1_MouseLeave(object sender, EventArgs e) {
            ((Panel)sender).Paint += (sender2, e2) => pnlI1_Paint(sender2, e2, "f5b800");

            ((Panel)sender).Parent.BackColor = Color.Transparent;
            ((Panel)sender).Parent.Controls[1].ForeColor = ColorTranslator.FromHtml("#f5b800");
        }
        #endregion
        #endregion
        #endregion
    }
}