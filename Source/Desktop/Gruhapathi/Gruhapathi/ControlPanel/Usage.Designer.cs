using ForeRunners.GUI.GoldButton;

namespace Gruhapathi.ControlPanel {
    partial class Usage {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea13 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend13 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series13 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.pnlSomeB = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblShowUsage = new ForeRunners.GUI.GoldButton.GoldButton();
            this.lblElec = new System.Windows.Forms.Label();
            this.lblWate = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chrUsage = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblUnits = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtStartDate = new MetroFramework.Controls.MetroTextBox();
            this.txtEnd = new MetroFramework.Controls.MetroTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chrUsage)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSomeB
            // 
            this.pnlSomeB.BackColor = System.Drawing.Color.Transparent;
            this.pnlSomeB.Location = new System.Drawing.Point(0, 116);
            this.pnlSomeB.Name = "pnlSomeB";
            this.pnlSomeB.Size = new System.Drawing.Size(164, 358);
            this.pnlSomeB.TabIndex = 18;
            this.pnlSomeB.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(10, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(371, 84);
            this.panel1.TabIndex = 19;
            this.panel1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Location = new System.Drawing.Point(641, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(140, 65);
            this.panel2.TabIndex = 20;
            this.panel2.Visible = false;
            // 
            // lblShowUsage
            // 
            this.lblShowUsage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            this.lblShowUsage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblShowUsage.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShowUsage.ForeColor = System.Drawing.Color.Black;
            this.lblShowUsage.Location = new System.Drawing.Point(564, 215);
            this.lblShowUsage.Name = "lblShowUsage";
            this.lblShowUsage.Size = new System.Drawing.Size(165, 19);
            this.lblShowUsage.TabIndex = 21;
            this.lblShowUsage.Text = "SHOW USAGE";
            this.lblShowUsage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblShowUsage.Click += new System.EventHandler(this.lblShowUsage_Click);
            // 
            // lblElec
            // 
            this.lblElec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.lblElec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblElec.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElec.ForeColor = System.Drawing.Color.White;
            this.lblElec.Location = new System.Drawing.Point(567, 175);
            this.lblElec.Name = "lblElec";
            this.lblElec.Size = new System.Drawing.Size(85, 23);
            this.lblElec.TabIndex = 28;
            this.lblElec.Text = "ELECTRICITY";
            this.lblElec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblElec.Click += new System.EventHandler(this.lblElec_Click);
            // 
            // lblWate
            // 
            this.lblWate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            this.lblWate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblWate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWate.ForeColor = System.Drawing.Color.Black;
            this.lblWate.Location = new System.Drawing.Point(658, 174);
            this.lblWate.Name = "lblWate";
            this.lblWate.Size = new System.Drawing.Size(60, 23);
            this.lblWate.TabIndex = 29;
            this.lblWate.Text = "WATER";
            this.lblWate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWate.Click += new System.EventHandler(this.lblWate_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(564, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 15);
            this.label6.TabIndex = 30;
            this.label6.Text = "USAGE DATA ON";
            // 
            // chrUsage
            // 
            this.chrUsage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.chrUsage.BorderlineColor = System.Drawing.Color.Black;
            chartArea13.AlignmentOrientation = ((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations)((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical | System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal)));
            chartArea13.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea13.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea13.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            chartArea13.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            chartArea13.AxisX.IsLabelAutoFit = false;
            chartArea13.AxisX.IsMarginVisible = false;
            chartArea13.AxisX.LabelStyle.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea13.AxisX.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            chartArea13.AxisX.LabelStyle.Format = "d";
            chartArea13.AxisX.LabelStyle.Interval = 0D;
            chartArea13.AxisX.LabelStyle.IntervalOffset = 0D;
            chartArea13.AxisX.LabelStyle.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea13.AxisX.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea13.AxisX.LabelStyle.TruncatedLabels = true;
            chartArea13.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            chartArea13.AxisX.MajorGrid.Enabled = false;
            chartArea13.AxisX.MajorGrid.Interval = 0D;
            chartArea13.AxisX.MajorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            chartArea13.AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            chartArea13.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(215)))), ((int)(((byte)(101)))));
            chartArea13.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea13.AxisX.MajorTickMark.Interval = 0D;
            chartArea13.AxisX.MajorTickMark.IntervalOffset = 0D;
            chartArea13.AxisX.MajorTickMark.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea13.AxisX.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            chartArea13.AxisX.MajorTickMark.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            chartArea13.AxisX.MajorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.AcrossAxis;
            chartArea13.AxisX.ScaleBreakStyle.Enabled = true;
            chartArea13.AxisX.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea13.AxisX.TitleFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea13.AxisX.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            chartArea13.AxisX2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            chartArea13.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea13.AxisY.IsLabelAutoFit = false;
            chartArea13.AxisY.IsMarginVisible = false;
            chartArea13.AxisY.LabelStyle.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea13.AxisY.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            chartArea13.AxisY.LabelStyle.Interval = 0D;
            chartArea13.AxisY.LabelStyle.IntervalOffset = 0D;
            chartArea13.AxisY.LabelStyle.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea13.AxisY.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea13.AxisY.LabelStyle.TruncatedLabels = true;
            chartArea13.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            chartArea13.AxisY.MajorGrid.Enabled = false;
            chartArea13.AxisY.MajorGrid.Interval = 0D;
            chartArea13.AxisY.MajorGrid.IntervalOffset = 0D;
            chartArea13.AxisY.MajorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea13.AxisY.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea13.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(215)))), ((int)(((byte)(101)))));
            chartArea13.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea13.AxisY.MajorTickMark.Interval = 0D;
            chartArea13.AxisY.MajorTickMark.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            chartArea13.AxisY.MajorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.AcrossAxis;
            chartArea13.AxisY.ScaleBreakStyle.LineColor = System.Drawing.Color.White;
            chartArea13.AxisY.TitleAlignment = System.Drawing.StringAlignment.Near;
            chartArea13.AxisY.TitleFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            chartArea13.AxisY.TitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            chartArea13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            chartArea13.BorderColor = System.Drawing.Color.White;
            chartArea13.Name = "ChartArea1";
            this.chrUsage.ChartAreas.Add(chartArea13);
            legend13.Enabled = false;
            legend13.HeaderSeparatorColor = System.Drawing.Color.White;
            legend13.ItemColumnSeparatorColor = System.Drawing.Color.White;
            legend13.Name = "Legend1";
            legend13.TitleForeColor = System.Drawing.Color.White;
            legend13.TitleSeparatorColor = System.Drawing.Color.White;
            this.chrUsage.Legends.Add(legend13);
            this.chrUsage.Location = new System.Drawing.Point(178, 311);
            this.chrUsage.Name = "chrUsage";
            series13.BorderWidth = 3;
            series13.ChartArea = "ChartArea1";
            series13.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series13.Color = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            series13.Legend = "Legend1";
            series13.Name = "Series1";
            this.chrUsage.Series.Add(series13);
            this.chrUsage.Size = new System.Drawing.Size(567, 198);
            this.chrUsage.TabIndex = 33;
            this.chrUsage.Text = "chart1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(726, 472);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 34;
            this.label5.Text = "Hours";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            this.label7.Location = new System.Drawing.Point(205, 298);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 16);
            this.label7.TabIndex = 35;
            this.label7.Text = "Units";
            // 
            // lblUnits
            // 
            this.lblUnits.AutoSize = true;
            this.lblUnits.BackColor = System.Drawing.Color.Transparent;
            this.lblUnits.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblUnits.ForeColor = System.Drawing.Color.White;
            this.lblUnits.Location = new System.Drawing.Point(314, 267);
            this.lblUnits.Name = "lblUnits";
            this.lblUnits.Size = new System.Drawing.Size(15, 16);
            this.lblUnits.TabIndex = 56;
            this.lblUnits.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            this.label13.Location = new System.Drawing.Point(205, 266);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(114, 16);
            this.label13.TabIndex = 55;
            this.label13.Text = "Units Consumed:";
            // 
            // lblDuration
            // 
            this.lblDuration.BackColor = System.Drawing.Color.Transparent;
            this.lblDuration.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblDuration.ForeColor = System.Drawing.Color.White;
            this.lblDuration.Location = new System.Drawing.Point(326, 248);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(128, 16);
            this.lblDuration.TabIndex = 53;
            this.lblDuration.Text = "1/5/2017 - Now";
            this.lblDuration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            this.label10.Location = new System.Drawing.Point(205, 248);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(262, 16);
            this.label10.TabIndex = 52;
            this.label10.Text = "USAGE HISTORY (                                  )";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblAmount.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblAmount.ForeColor = System.Drawing.Color.White;
            this.lblAmount.Location = new System.Drawing.Point(448, 267);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(49, 16);
            this.lblAmount.TabIndex = 51;
            this.lblAmount.Text = "Rs, 0.0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(368, 267);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 16);
            this.label8.TabIndex = 50;
            this.label8.Text = "Bill Amount:";
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.BackColor = System.Drawing.Color.Transparent;
            this.lblProgress.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblProgress.ForeColor = System.Drawing.Color.White;
            this.lblProgress.Location = new System.Drawing.Point(599, 267);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(26, 16);
            this.lblProgress.TabIndex = 58;
            this.lblProgress.Text = "0.0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            this.label14.Location = new System.Drawing.Point(536, 267);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 16);
            this.label14.TabIndex = 57;
            this.label14.Text = "Progress:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            this.label11.Location = new System.Drawing.Point(205, 158);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(118, 15);
            this.label11.TabIndex = 60;
            this.label11.Text = "START DATE RANGE";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            this.label15.Location = new System.Drawing.Point(368, 156);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(104, 15);
            this.label15.TabIndex = 62;
            this.label15.Text = "END DATE RANGE";
            // 
            // txtStartDate
            // 
            // 
            // 
            // 
            this.txtStartDate.CustomButton.Image = null;
            this.txtStartDate.CustomButton.Location = new System.Drawing.Point(125, 1);
            this.txtStartDate.CustomButton.Name = "";
            this.txtStartDate.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtStartDate.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtStartDate.CustomButton.TabIndex = 1;
            this.txtStartDate.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtStartDate.CustomButton.UseSelectable = true;
            this.txtStartDate.CustomButton.Visible = false;
            this.txtStartDate.ForeColor = System.Drawing.Color.White;
            this.txtStartDate.Lines = new string[0];
            this.txtStartDate.Location = new System.Drawing.Point(208, 176);
            this.txtStartDate.MaxLength = 32767;
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.PasswordChar = '\0';
            this.txtStartDate.ReadOnly = true;
            this.txtStartDate.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtStartDate.SelectedText = "";
            this.txtStartDate.SelectionLength = 0;
            this.txtStartDate.SelectionStart = 0;
            this.txtStartDate.ShortcutsEnabled = true;
            this.txtStartDate.Size = new System.Drawing.Size(147, 23);
            this.txtStartDate.TabIndex = 63;
            this.txtStartDate.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtStartDate.UseSelectable = true;
            this.txtStartDate.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtStartDate.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtStartDate.Click += new System.EventHandler(this.txtStartDate_Click);
            // 
            // txtEnd
            // 
            // 
            // 
            // 
            this.txtEnd.CustomButton.Image = null;
            this.txtEnd.CustomButton.Location = new System.Drawing.Point(125, 1);
            this.txtEnd.CustomButton.Name = "";
            this.txtEnd.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtEnd.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtEnd.CustomButton.TabIndex = 1;
            this.txtEnd.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtEnd.CustomButton.UseSelectable = true;
            this.txtEnd.CustomButton.Visible = false;
            this.txtEnd.ForeColor = System.Drawing.Color.White;
            this.txtEnd.Lines = new string[0];
            this.txtEnd.Location = new System.Drawing.Point(371, 176);
            this.txtEnd.MaxLength = 32767;
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.PasswordChar = '\0';
            this.txtEnd.ReadOnly = true;
            this.txtEnd.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtEnd.SelectedText = "";
            this.txtEnd.SelectionLength = 0;
            this.txtEnd.SelectionStart = 0;
            this.txtEnd.ShortcutsEnabled = true;
            this.txtEnd.Size = new System.Drawing.Size(147, 23);
            this.txtEnd.TabIndex = 64;
            this.txtEnd.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.txtEnd.UseSelectable = true;
            this.txtEnd.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtEnd.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtEnd.Click += new System.EventHandler(this.txtEnd_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(205, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(313, 20);
            this.label1.TabIndex = 65;
            this.label1.Text = "CONSUMPTION PERIOD";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(564, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 20);
            this.label2.TabIndex = 66;
            this.label2.Text = "CONSUMPTION TYPE";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(536, 283);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 16);
            this.label3.TabIndex = 67;
            this.label3.Text = "than the previous period";
            // 
            // Usage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 520);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEnd);
            this.Controls.Add(this.txtStartDate);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblUnits);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chrUsage);
            this.Controls.Add(this.lblWate);
            this.Controls.Add(this.lblElec);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblShowUsage);
            this.Controls.Add(this.pnlSomeB);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.DisplayHeader = false;
            this.MaximizeBox = false;
            this.Name = "Usage";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.Resizable = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Usage";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            ((System.ComponentModel.ISupportInitialize)(this.chrUsage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlSomeB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private GoldButton lblShowUsage;
        private System.Windows.Forms.Label lblElec;
        private System.Windows.Forms.Label lblWate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataVisualization.Charting.Chart chrUsage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblUnits;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label15;
        private MetroFramework.Controls.MetroTextBox txtStartDate;
        private MetroFramework.Controls.MetroTextBox txtEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}