using Gruhapathi.Dialogs;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gruhapathi.ControlPanel {
    public partial class Settings : MetroForm {

        GUICore gui;
        InputDialog inputBox;

        protected string password;
        protected string email;

        public Settings() {
            InitializeComponent();

            gui = new GUICore(this);
            gui.createCoreGUI(3);

            inputBox = new InputDialog();

            password = "Universe.13";
            txtPassword.Text = password;

            email = "amuthupuwath@gmail.com";
            txtEmail.Text = email;
        }

        private void txtPassword_Leave(object sender, EventArgs e) {
            inputBox = new InputDialog();
            inputBox.Text = "Password Confirmation";
            inputBox.Description = "Please Enter the Password to Confirm.";
            inputBox.InputLabel = "Password:";
            inputBox.InputFieldMask = 'X';

            if (!password.Equals(txtPassword.Text)) {
                inputBox.MatchText = txtPassword.Text;
                inputBox.ShowDialog(this);
                if (!string.IsNullOrWhiteSpace(inputBox.InputText)) {
                    if (txtPassword.Text.Equals(inputBox.InputText)) {
                        password = inputBox.InputText;
                    }
                }              
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e) {
            inputBox = new InputDialog();
            inputBox.Text = "Email Confirmation";
            inputBox.Description = "Please Enter the Email to Confirm.";
            inputBox.InputLabel = "Email:";

            if (!email.Equals(txtEmail.Text)) {
                inputBox.MatchText = txtEmail.Text;
                inputBox.ShowDialog(this);
                if (!string.IsNullOrWhiteSpace(inputBox.InputText)) {
                    if (txtEmail.Text.Equals(inputBox.InputText)) {
                        email = inputBox.InputText;
                    }
                }
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e) {
            if ((Keys)e.KeyChar == Keys.Enter) 
                txtPassword_Leave(sender, e);            
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e) {
            if ((Keys)e.KeyChar == Keys.Enter)
                txtEmail_Leave(sender, e);
        }
    }
}
