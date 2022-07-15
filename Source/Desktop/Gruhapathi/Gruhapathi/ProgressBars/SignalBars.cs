using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Gruhapathi.ProgressBars {
    public class SignalBars : Panel {
        private const short signalWidth = 6;

        private int signalBarCount = 1;
        private int signalBarHeight = 6;
        private int maxX = 12;
        private int maxWidth = signalWidth;
        private int percentage = 0;

        [Category("Appearance")]
        public int SignalBarCount {
            get {
                return signalBarCount;
            }
            set {
                signalBarCount = value;
                initControls();
            }
        }

        private void initControls() {
            signalBarHeight = 6*SignalBarCount;

            this.Height = signalBarHeight;

            this.Controls.Clear();

            maxX = ((SignalBarCount - 1) * 12) + 1;
            maxWidth = (maxX + signalWidth) + 1;

            this.Width = maxWidth;
            this.MaximumSize = new Size(maxWidth, signalBarHeight);

            this.AutoSize = true;
            int x = 0, y = 0;

            for (int i = 0; i < SignalBarCount; i++) {
                Panel bar = new Panel();

                bar.Paint += (sender, e) => {
                    Rectangle rect = e.ClipRectangle;
                    rect.Width--;
                    rect.Height--;
                    e.Graphics.DrawRectangle(new Pen(ColorTranslator.FromHtml("#f2b600")), rect);
                };

                bar.Location = new Point(x, y);
                bar.Width = signalWidth;
                bar.Height = signalBarHeight;

                this.Controls.Add(bar);

                x += 12;
                y += 6;
                signalBarHeight -= 5;
            }
        }

        public SignalBars() : base() {
            this.Name = "SignalBar";
            initControls();

        }

        [Category("Appearance")]
        public int Percentage {
            get {
                double colorBars = (percentage * SignalBarCount) / 100;
                colorBars = Math.Round(Math.Ceiling(colorBars), 0);

                foreach (Control ctrl in this.Controls)
                    ctrl.BackColor = Color.Transparent;

                int containCount = this.Controls.Count - 1;

                for (int j = containCount; (containCount - j) < colorBars; j--)
                    this.Controls[j].BackColor = ColorTranslator.FromHtml("#f2b600");
                return percentage;
            }
            set {
                percentage = value;
                double colorBars = (percentage * SignalBarCount) / 100;
                colorBars = Math.Round(Math.Ceiling(colorBars), 0);

                foreach (Control ctrl in this.Controls)
                    ctrl.BackColor = Color.Transparent;

                int containCount = this.Controls.Count - 1;

                for (int j = containCount; (containCount - j) < colorBars; j--)
                    this.Controls[j].BackColor = ColorTranslator.FromHtml("#f2b600");
            }
        }

        ~SignalBars() { }
    }
}