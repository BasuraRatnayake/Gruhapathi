using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using ForeRunners.Data;

namespace Gruhapathi {
    public partial class StartUp : MetroForm {

        private API api;
        private ConfigurationFile iniFile;

        public StartUp() {
            InitializeComponent();

            try {
                iniFile = new ConfigurationFile("Gruhapathi.ini");
                if (iniFile.KeyExists("Path", "API")) {
                    api = new API(iniFile.Read("Path", "API"));
                } else {
                    api = new API("http://gruhapathitest.000webhostapp.com/API/");
                }
            } catch (Exception ex) { }
            timer.Start();
        }

        private void checkProgress(Panel panel, Label label, Timer tim) {
            try {
                int width = 0;
                string color = "#f5b800";
                if (api.isInternetAvailable) {
                    width += 170;
                    color = "#f5b800";
                    lblLoading.Text = "Checking Internet Connection.";
                    if (api.isOnline) {
                        width += 170;
                        color = "#f5b800";
                        label.Text = "Establishing Server Connection.";
                        tim.Enabled = false;
                        new SignIn().Show();
                        this.Hide();
                    } else {
                        label.Text = "Failed to Establish Server Connection.";
                        color = "#c00000";
                    }
                } else {
                    lblLoading.Text = "No Internet Connection.";
                    color = "#c00000";
                }

                panel.BackColor = ColorTranslator.FromHtml(color);
                panel.Size = new Size(width, 5);
            } catch (Exception ex) { }
        }

        private void timer_Tick(object sender, EventArgs e) {
            checkProgress(pnlLoading, lblLoading, timer);
        }
    }
}
