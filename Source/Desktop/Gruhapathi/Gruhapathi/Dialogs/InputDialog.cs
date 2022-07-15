using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Gruhapathi.Dialogs {
    public class InputDialog: MetroForm {
        private MetroLabel lblDesc;
        private MetroLabel lblInputName;
        private MetroTextBox txtInput;
        private Panel pnlStatus;

        public InputDialog():base() {
            initControls();
        }

        [Category("InputDialog")]
        public override string Text {
            get {
                return base.Text == string.Empty ? "Title Goes Here" : base.Text; ;
            }
            set {
                base.Text = value;
            }
        }
        [Category("InputDialog")]
        public string Description {
            get {
                return lblDesc.Text == string.Empty ? "Description Goes Here" : lblDesc.Text;
            }
            set {
                lblDesc.Text = value;
            }
        }

        [Category("InputDialog")]
        public string InputLabel {
            get {
                return lblInputName.Text == string.Empty ? "Input Label Here" : lblInputName.Text;
            }
            set {
                lblInputName.Text = value;
            }
        }
        [Category("InputDialog")]
        public char InputFieldMask {
            get {
                return txtInput.PasswordChar == '\0' ? '\0' : txtInput.PasswordChar;
            }
            set {
                txtInput.PasswordChar = value;
            }
        }


        private string inputText;
        public string InputText {
            get { return inputText; }
        }

        private string matchText;
        public string MatchText {
            get {
                return matchText;
            }
            set {
                matchText = value;
            }
        }

        private void initControls() {
            lblDesc = new MetroLabel();
            lblInputName = new MetroLabel();
            txtInput = new MetroTextBox();
            pnlStatus = new Panel();

            lblDesc = new MetroLabel();
            lblDesc.AutoSize = true;
            lblDesc.ForeColor = Color.White;
            lblDesc.Location = new Point(22, 53);
            lblDesc.Size = new Size(266, 19);
            lblDesc.TabIndex = 0;
            lblDesc.Text = Description;
            lblDesc.Theme = MetroThemeStyle.Dark;
            lblDesc.UseCustomForeColor = true;

            lblInputName = new MetroLabel();
            lblInputName.FontWeight = MetroLabelWeight.Bold;
            lblInputName.ForeColor = Color.White;
            lblInputName.Location = new Point(11, 98);
            lblInputName.Size = new Size(166, 22);
            lblInputName.Text = InputLabel;
            lblInputName.TextAlign = ContentAlignment.MiddleRight;
            lblInputName.Theme = MetroThemeStyle.Dark;
            lblInputName.UseCustomForeColor = true;

            txtInput = new MetroTextBox();
            txtInput.ForeColor = Color.White;
            txtInput.Lines = new string[0];
            txtInput.Location = new Point(183, 98);
            txtInput.MaxLength = 32767;
            txtInput.PasswordChar = InputFieldMask;
            txtInput.ScrollBars = ScrollBars.None;
            txtInput.SelectedText = "";
            txtInput.SelectionLength = 0;
            txtInput.SelectionStart = 0;
            txtInput.ShortcutsEnabled = true;
            txtInput.Size = new Size(231, 23);
            txtInput.Style = MetroColorStyle.Yellow;
            txtInput.TabIndex = 1;
            txtInput.TabStop = true;
            txtInput.Theme = MetroThemeStyle.Dark;
            txtInput.UseSelectable = true;
            txtInput.WaterMarkColor = Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            txtInput.WaterMarkFont = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Pixel);
            txtInput.KeyPress += txtInput_KeyPress;
            txtInput.TextChanged += (sender2, e2)=> {
                inputText = txtInput.Text != string.Empty ? txtInput.Text : inputText;
            };

            pnlStatus.BackColor = Color.DarkRed;
            pnlStatus.Location = new Point(413, 98);
            pnlStatus.Size = new Size(23, 23);
            pnlStatus.Visible = false;

            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(480, 144);
            this.Controls.Add(pnlStatus);
            this.Controls.Add(txtInput);
            this.Controls.Add(lblInputName);
            this.Controls.Add(lblDesc);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Resizable = false;
            this.Icon = new Icon("images/logo.ico");
            this.ShowIcon = false;
            this.ShowInTaskbar = true;
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.Style = MetroColorStyle.Yellow;
            this.Text = Text;
            this.Theme = MetroThemeStyle.Dark;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void txtInput_KeyPress(object sender, KeyPressEventArgs e) {
            if ((Keys)e.KeyChar == Keys.Enter) {
                pnlStatus.Visible = false;
                if (string.IsNullOrWhiteSpace(MatchText))
                    this.Hide();
                else {
                    if (!MatchText.Equals(InputText)) 
                        pnlStatus.Visible = true;
                     else {
                        this.Hide();
                        pnlStatus.Visible = false;
                        inputText = txtInput.Text;
                        txtInput.Text = string.Empty;
                    }
                }
            }
        }

        ~InputDialog() { }
    }
}
