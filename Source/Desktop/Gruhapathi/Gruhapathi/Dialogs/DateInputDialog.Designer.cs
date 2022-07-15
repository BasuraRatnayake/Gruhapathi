namespace Gruhapathi.Dialogs {
    partial class DateInputDialog {
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
            this.components = new System.ComponentModel.Container();
            this.btnDate = new ForeRunners.GUI.GoldButton.GoldButton();
            this.lblDate = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tim = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnDate
            // 
            this.btnDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            this.btnDate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDate.ForeColor = System.Drawing.Color.Black;
            this.btnDate.Location = new System.Drawing.Point(23, 221);
            this.btnDate.Name = "btnDate";
            this.btnDate.Size = new System.Drawing.Size(178, 22);
            this.btnDate.TabIndex = 22;
            this.btnDate.Text = "SET DATE";
            this.btnDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDate.Click += new System.EventHandler(this.btnDate_Click);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.White;
            this.lblDate.Location = new System.Drawing.Point(123, 55);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(42, 15);
            this.lblDate.TabIndex = 64;
            this.lblDate.Text = "DATE: ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            this.label11.Location = new System.Drawing.Point(23, 55);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 15);
            this.label11.TabIndex = 63;
            this.label11.Text = "SELECTED DATE: ";
            // 
            // tim
            // 
            this.tim.Enabled = true;
            this.tim.Interval = 300;
            this.tim.Tick += new System.EventHandler(this.tim_Tick);
            // 
            // DateInputDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 256);
            this.ControlBox = false;
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnDate);
            this.MaximizeBox = false;
            this.Name = "DateInputDialog";
            this.Resizable = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Style = MetroFramework.MetroColorStyle.Yellow;
            this.Text = "Select Date";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ForeRunners.GUI.GoldButton.GoldButton btnDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Timer tim;
    }
}