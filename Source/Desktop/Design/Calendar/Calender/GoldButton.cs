using System;
using System.Drawing;
using System.Windows.Forms;

namespace ForeRunners.GUI.GoldButton {
    public class GoldButton:Label {
        public GoldButton() {
            this.MouseEnter += GoldButton_MouseEnter;
            this.MouseLeave += GoldButton_MouseLeave;
            
            this.BackColor = Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            this.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.ForeColor = Color.Black;
            this.Cursor = Cursors.Hand;

            this.Size = new Size(140, 25);
        }

        private void GoldButton_MouseLeave(object sender, EventArgs e) {
            this.BackColor = Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
        }

        private void GoldButton_MouseEnter(object sender, EventArgs e) {
            this.BackColor = Color.White;
        }
    }
}