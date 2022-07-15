using System;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;
using ForeRunners.Data;
using ForeRunners.Data.Model;
using MetroFramework;
using QRCoder;

namespace Gruhapathi {
    public partial class SignIn : MetroForm {
        protected string username;
        protected string password;
        private string qrCode;

        private API api;
        private ConfigurationFile iniFile;
        private GUICore gui;

        public SignIn() {
            InitializeComponent();

            try {
                qrCode = string.Empty;
                gui = new GUICore(this);
                iniFile = new ConfigurationFile("Gruhapathi.ini");
                api = new API(iniFile.Read("Path", "API"));
            } catch { }
        }

        #region Appearance
        private void lblClose_MouseEnter(object sender, EventArgs e) {
            lblClose.ForeColor = Color.DarkRed;
        }
        private void lblClose_MouseLeave(object sender, EventArgs e) {
            lblClose.ForeColor = Color.White;
        }
        private void lblClose_MouseClick(object sender, MouseEventArgs e) {
            pnlQRBig.Visible = false;
        }

        private void imgQR_MouseClick(object sender, MouseEventArgs e) {
            DataCore dCore = new DataCore();
            qrCode = dCore.generateCode(20);

            if (api.add_QRCode(qrCode)) {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCode, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCodeI = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCodeI.GetGraphic(20);

                imgQRB.Image = qrCodeImage;

                pnlQRBig.Location = new Point(0, 0);
                pnlQRBig.Visible = true;
                checkAlt.Enabled = true;
                checkAlt.Start();
            }            
        }
        #endregion

        private void btnLogin_Click(object sender, EventArgs e) {
            try {
                username = txtUsername.Text == "" ? "test" : txtUsername.Text;
                password = txtPassword.Text == "" ? "test" : txtPassword.Text;

                if (api.isOnline) {
                    Authentication auth = api.authenticate(username, password);
                    if (auth.status && auth.response_code == 200) {
                        new ControlPanel.Summary().Show();
                        this.Hide();
                    } else {
                        MetroMessageBox.Show(this, "Incorrect Username or Password", "Gruhapathi Control Panel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } else {
                    new StartUp().Show();
                    this.Hide();
                }
            } catch{ }            
        }

        private void SignIn_FormClosed(object sender, FormClosedEventArgs e) {
            Application.Exit();
        }

        private int countDown = 0;
        private void checkAlt_Tick(object sender, EventArgs e) {
            if(countDown >= 12) {
                checkAlt.Stop();
                checkAlt.Enabled = false;
                pnlQRBig.Visible = false;
                countDown = 0;
                return;
            }
            Authentication auth = api.get_QRCode(qrCode);
            if(auth.status && auth.response_code == 200) {
                checkAlt.Stop();
                checkAlt.Enabled = false;

                iniFile.Write("Username", auth.auth_username, "Authorization");
                iniFile.Write("Token", auth.auth_token, "Authorization");
                iniFile.Write("Refresh_Token", auth.refresh_token, "Authorization");
                iniFile.Write("Expire", auth.auth_expire.ToString(), "Authorization");
                pnlQRBig.Visible = false;

                new ControlPanel.Summary().Show();
                this.Hide();
            }
            countDown++;
        }
    }
}