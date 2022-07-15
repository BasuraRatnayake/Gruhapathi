using MetroFramework.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace ForeRunners.GUI.ComboBoxWithBorder {
    public class ComboBoxWithBorder : ComboBox {
        private Color _borderColor = Color.Black;
        private ButtonBorderStyle _borderStyle = ButtonBorderStyle.Solid;
        private static int WM_PAINT = 0x000F;

        private Color _borderBrushColor = Color.Black;
        private Color _dropButtonBrush = Color.Black;
        private Color _arrowBrush = Color.Black;       

        protected override void WndProc(ref Message m) {
            base.WndProc(ref m);

            if (m.Msg == WM_PAINT) {
                Graphics g = Graphics.FromHwnd(Handle);
                Rectangle bounds = new Rectangle(0, 0, Width, Height);
                ControlPaint.DrawBorder(g, bounds, _borderColor, _borderStyle);

                g = this.CreateGraphics();
                Pen p = new Pen(Color.Black);
                g.FillRectangle(new SolidBrush(_borderBrushColor), this.ClientRectangle);

                Rectangle rect = new Rectangle(this.Width - 17, 0, 17, this.Height);
                g.FillRectangle(new SolidBrush(_dropButtonBrush), rect);

                GraphicsPath pth = new GraphicsPath();
                PointF TopLeft = new PointF(this.Width - 13, (this.Height - 5) / 2);
                PointF TopRight = new PointF(this.Width - 6, (this.Height - 5) / 2);
                PointF Bottom = new PointF(this.Width - 9, (this.Height + 2) / 2);
                pth.AddLine(TopLeft, TopRight);
                pth.AddLine(TopRight, Bottom);

                g.SmoothingMode = SmoothingMode.HighQuality;

                if (this.DroppedDown)
                    _arrowBrush = Color.Black;
                else
                    _arrowBrush = Color.Black;

                g.FillPath(new SolidBrush(_arrowBrush), pth);
            }
        }

        [Category("Appearance")]
        public Color ArrowColor {
            get { return _arrowBrush; }
            set {
                _arrowBrush = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public Color DropbuttonBrush {
            get { return _dropButtonBrush; }
            set {
                _dropButtonBrush = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public Color BorderBrush {
            get { return _borderBrushColor; }
            set {
                _borderBrushColor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public Color BorderColor {
            get { return _borderColor; }
            set {
                _borderColor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public ButtonBorderStyle BorderStyle {
            get { return _borderStyle; }
            set {
                _borderStyle = value;
                Invalidate();
            }
        }
    }
}
