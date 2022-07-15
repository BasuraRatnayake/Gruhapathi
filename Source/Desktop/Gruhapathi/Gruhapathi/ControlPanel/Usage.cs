using ForeRunners.Data;
using ForeRunners.Data.Model;
using Gruhapathi.Dialogs;
using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Gruhapathi.ControlPanel {
    public partial class Usage : MetroForm {

        private GUICore gui;
        private API api;
        private ConfigurationFile iniFile;
        private Series series;

        protected string auth_token;
        private string power_type = "E";
        private string start_date = string.Empty;
        private string end_date = string.Empty;

        private int units = 0;
        private double income = 0.0;
        private double progress = 0.0;

        private void setChartData() {
            try {
                chrUsage.Series.Clear();
                units = 0;
                income = 0.0;
                progress = 0.0;

                series = chrUsage.Series.Add("Total Income");
                series.ChartType = SeriesChartType.Line;
                series.BorderWidth = 2;
                series.Color = ColorTranslator.FromHtml("#f2b600");

                UsageData usageData = api.get_UsageData(auth_token, txtStartDate.Text, txtEnd.Text, power_type);
                int hours = 1;

                foreach (UsageD dyn in usageData.data) {
                    units += dyn.units;
                    income += dyn.price;

                    if (dyn.progress_data.direction == "+") {
                        progress += dyn.progress_data.progress;
                    } else {
                        progress -= dyn.progress_data.progress;
                    }

                    series.Points.AddXY(dyn.date_recorded, dyn.units);
                    hours++;
                }

                lblUnits.Text = units.ToString();
                lblAmount.Text = "Rs, " + Math.Round(income, 2);
                lblProgress.Text = progress + "% ";
                if (progress < 0) {
                    lblProgress.Text += " Increase";
                } else {
                    lblProgress.Text += " Decrease";
                }
            } catch(Exception ex) {
                lblUnits.Text = "0";
                lblAmount.Text = "Rs, 0.0";
                lblProgress.Text = "0.0";
            }            
        }

        public Usage() {
            InitializeComponent();

            gui = new GUICore(this);
            gui.createCoreGUI(1);          

            lblWate.Paint += (sender2, e2) => gui.pnlI1_Paint(sender2, e2, "f2b600");
            lblElec.Paint += (sender2, e2) => gui.pnlI1_Paint(sender2, e2, "f2b600");

            iniFile = new ConfigurationFile("Gruhapathi.ini");
            api = new API(iniFile.Read("Path", "API"));
            auth_token = iniFile.Read("Token", "Authorization");

            lblElec.ForeColor = Color.Black;
            lblElec.BackColor = ColorTranslator.FromHtml("#f2b600");

            lblWate.ForeColor = Color.White;
            lblWate.BackColor = Color.Transparent;
            power_type = "E";

            start_date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.AddDays((DateTime.Now.Day*-1)+1).Day.ToString();
            end_date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + (DateTime.Now.Day+1).ToString();

            txtEnd.Text = end_date;
            txtStartDate.Text = start_date;

            lblDuration.Text = start_date.Replace("-","/")+" - "+ end_date.Replace("-", "/");

            lblUnits.Text = units.ToString();
            lblAmount.Text = "Rs, "+ income;
            lblProgress.Text = "0.0";

            setChartData();
        }     

        private void lblElec_Click(object sender, EventArgs e) {
            lblElec.ForeColor = Color.Black;
            lblElec.BackColor = ColorTranslator.FromHtml("#f2b600");

            lblWate.ForeColor = Color.White;
            lblWate.BackColor = Color.Transparent;
            power_type = "E";
        }
        private void lblWate_Click(object sender, EventArgs e) {
            lblWate.ForeColor = Color.Black;
            lblWate.BackColor = ColorTranslator.FromHtml("#f2b600");

            lblElec.ForeColor = Color.White;
            lblElec.BackColor = Color.Transparent;
            power_type = "W";
        }

        private void lblShowUsage_Click(object sender, EventArgs e) {
            if(string.IsNullOrWhiteSpace(txtStartDate.Text) && string.IsNullOrWhiteSpace(txtEnd.Text)) {
                MetroMessageBox.Show(this, "Please Select Date Range", "Gruhapathi Control Panel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            setChartData();
        }

        private void txtStartDate_Click(object sender, EventArgs e) {
            DateInputDialog di = new DateInputDialog();
            di.ShowDialog();

            txtStartDate.Text = di.getSelectedDate();
        }
        private void txtEnd_Click(object sender, EventArgs e) {
            DateInputDialog di = new DateInputDialog();
            di.ShowDialog();

            txtEnd.Text = di.getSelectedDate();
        }
    }
}