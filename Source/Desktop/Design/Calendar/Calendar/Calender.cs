using System;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace ForeRunners.GUI.Calender {
    /*        
        Author: Basura Ratnayake
        Copyright (c) 2017 Basura Ratnayake. All Rights Reserved.
    */

    public class Calender {
        private string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        private string[] months_full = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        private int[] years;

        private string selectedMonth;
        private int selectedMonthIndex;

        private int selectedYear;
        private int selectedYearIndex;

        private int selectedDay;

        private int daysInMonth;

        private string date;
        public string CurrentDate {
            get {
                return date;
            }
        }

        private void pnlI1_Paint(object sender, PaintEventArgs e) {
            Rectangle rect = e.ClipRectangle;
            rect.Width--;
            rect.Height--;
            e.Graphics.DrawRectangle(new Pen(ColorTranslator.FromHtml("#f2b600")), rect);
        }

        private Panel calenderHolder;

        public Calender(MetroForm frmOwner, Point location, int startYear = 2016) {
            calenderHolder = new Panel();
            calenderHolder.Location = location;
            calenderHolder.Name = "calenderHolder";
            calenderHolder.Size = new Size(182, 134);

            DateTime dt = DateTime.Now;

            selectedMonthIndex = dt.Month - 1;
            selectedMonth = months_full[selectedMonthIndex];

            selectedYear = dt.Year;

            selectedDay = dt.Day;

            daysInMonth = DateTime.DaysInMonth(selectedYear, dt.Month);

            int remaining = dt.Year - startYear + 1;
            years = new int[remaining];

            for (int i = 0; i < remaining; i++)
                years[i] = selectedYear - i;

            selectedYearIndex = 0;

            initCalender();
            buildDays();

            date = selectedDay + "/" + selectedMonthIndex + 1 + "/" + selectedYear;

            frmOwner.Controls.Add(calenderHolder);
        }

        private void initCalender() {
            Label lblPrevious = new Label();
            lblPrevious.Font = new Font("Arial", 11.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblPrevious.ForeColor = Color.White;
            lblPrevious.Location = new Point(10, 3);
            lblPrevious.Size = new Size(22, 20);
            lblPrevious.Text = "<";
            lblPrevious.TextAlign = ContentAlignment.MiddleCenter;
            lblPrevious.MouseLeave += Day_MouseLeave;
            lblPrevious.MouseEnter += Day_MouseEnter;
            lblPrevious.Click += LblPrevious_Click;
            calenderHolder.Controls.Add(lblPrevious);

            Label lblNext = new Label();
            lblNext.Font = new Font("Arial", 11.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblNext.ForeColor = Color.White;
            lblNext.Location = new Point(147, 3);
            lblNext.Size = new Size(22, 20);
            lblNext.Text = ">";
            lblNext.TextAlign = ContentAlignment.MiddleCenter;
            lblNext.MouseLeave += Day_MouseLeave;
            lblNext.MouseEnter += Day_MouseEnter;
            lblNext.Click += LblNext_Click;
            calenderHolder.Controls.Add(lblNext);

            Label lblMonth = new Label();
            lblMonth.BackColor = Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            lblMonth.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblMonth.ForeColor = Color.Black;
            lblMonth.Location = new Point(33, 3);
            lblMonth.Name = "lblMonth";
            lblMonth.Size = new Size(75, 20);
            lblMonth.Text = selectedMonth;
            lblMonth.TextAlign = ContentAlignment.MiddleCenter;
            lblMonth.Click += LblMonth_Click;
            calenderHolder.Controls.Add(lblMonth);

            Label lblYear = new Label();
            lblYear.BackColor = Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(182)))), ((int)(((byte)(0)))));
            lblYear.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblYear.ForeColor = Color.Black;
            lblYear.Location = new Point(108, 3);
            lblYear.Name = "lblYear";
            lblYear.Size = new Size(38, 20);
            lblYear.TabIndex = 44;
            lblYear.Text = selectedYear.ToString();
            lblYear.TextAlign = ContentAlignment.MiddleCenter;
            lblYear.Click += LblYear_Click;
            calenderHolder.Controls.Add(lblYear);
        }

        private void LblNext_Click(object sender, EventArgs e) {
            selectedMonthIndex++;
            if (selectedMonthIndex == -1) {
                selectedMonthIndex = 11;
                selectedYearIndex--;
            } else if (selectedMonthIndex == 12) {
                selectedMonthIndex = 0;
                selectedYearIndex--;
            }

            if (selectedYearIndex == -1)
                selectedYearIndex = years.Length - 1;
            else if (selectedYearIndex == years.Length)
                selectedYearIndex = 0;

            selectedYear = years[selectedYearIndex];
            selectedMonth = months_full[selectedMonthIndex];

            date = selectedDay + "/" + getMonthInNumber() + "/" + selectedYear;

            calenderHolder.Controls.Clear();

            initCalender();
            daysInMonth = DateTime.DaysInMonth(selectedYear, selectedMonthIndex + 1);
            buildDays();
        }

        private void LblPrevious_Click(object sender, EventArgs e) {
            selectedMonthIndex--;
            if (selectedMonthIndex == -1) {
                selectedMonthIndex = 11;
                selectedYearIndex++;
            } else if (selectedMonthIndex == 12) {
                selectedMonthIndex = 0;
                selectedYearIndex++;
            }

            if (selectedYearIndex == -1)
                selectedYearIndex = years.Length - 1;
            else if (selectedYearIndex == years.Length)
                selectedYearIndex = 0;

            selectedYear = years[selectedYearIndex];
            selectedMonth = months_full[selectedMonthIndex];

            date = selectedDay + "/" + getMonthInNumber() + "/" + selectedYear;

            calenderHolder.Controls.Clear();

            initCalender();
            daysInMonth = DateTime.DaysInMonth(selectedYear, selectedMonthIndex + 1);
            buildDays();
        }
        #region Year
        private void LblYear_Click(object sender, EventArgs e) {
            buildYears();
        }
        private void buildYears() {
            calenderHolder.Controls.Clear();

            int x = 20, y = 32;
            for (int i = 0; i < years.Length; i++) {
                calenderHolder.Controls.Add(getLabelYear(years[i].ToString(), new Point(x, y)));
                x += 43;
                if (x > 125) {
                    x = 20;
                    y += 24;
                }
            }
        }
        private Label getLabelYear(string text, Point location) {
            Label year = new Label();
            year.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            year.ForeColor = Color.White;
            year.Location = location;
            year.Size = new Size(42, 23);
            year.Cursor = Cursors.Hand;
            year.Text = text;
            year.TextAlign = ContentAlignment.MiddleCenter;
            year.Click += Year_Click;
            year.MouseEnter += Day_MouseEnter;
            year.Paint += pnlI1_Paint;
            year.MouseLeave += Day_MouseLeave;
            return year;
        }
        private void Year_Click(object sender, EventArgs e) {
            for (int i = 0; i < years.Length; i++)
                if (years[i].ToString() == ((Label)sender).Text) {
                    selectedYearIndex = i;
                    break;
                }

            selectedYear = years[selectedYearIndex];
            calenderHolder.Controls.Clear();

            daysInMonth = DateTime.DaysInMonth(selectedYear, selectedMonthIndex + 1);

            date = selectedDay + "/" + getMonthInNumber() + "/" + selectedYear;

            initCalender();
            buildDays();
        }
        #endregion
        #region Month
        private void LblMonth_Click(object sender, EventArgs e) {
            buildMonths();
        }
        private void buildMonths() {
            calenderHolder.Controls.Clear();

            int x = 26, y = 32;
            for (int i = 0; i < months.Length; i++) {
                calenderHolder.Controls.Add(getLabelMonth(months[i], new Point(x, y)));
                x += 33;
                if (x > 125) {
                    x = 26;
                    y += 24;
                }
            }
        }
        private Label getLabelMonth(string text, Point location) {
            Label month = new Label();
            month.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            month.ForeColor = Color.White;
            month.Location = location;
            month.Size = new Size(32, 23);
            month.Cursor = Cursors.Hand;
            month.Text = text;
            month.TextAlign = ContentAlignment.MiddleCenter;
            month.Click += Month_Click;
            month.MouseEnter += Day_MouseEnter;
            month.Paint += pnlI1_Paint;
            month.MouseLeave += Day_MouseLeave;
            return month;
        }
        private void Month_Click(object sender, EventArgs e) {
            for (int i = 0; i < months.Length; i++)
                if (months[i] == ((Label)sender).Text) {
                    selectedMonthIndex = i;
                    break;
                }

            selectedMonth = months_full[selectedMonthIndex];
            calenderHolder.Controls.Clear();

            daysInMonth = DateTime.DaysInMonth(selectedYear, selectedMonthIndex + 1);

            date = selectedDay + "/" + getMonthInNumber() + "/" + selectedYear;

            initCalender();
            buildDays();
        }

        private int getMonthInNumber() {
            switch (selectedMonth) {
                case "January":
                    return 1;
                case "February":
                    return 2;
                case "March":
                    return 3;
                case "April":
                    return 4;
                case "May":
                    return 5;
                case "June":
                    return 6;
                case "July":
                    return 7;
                case "August":
                    return 8;
                case "September":
                    return 9;
                case "October":
                    return 10;
                case "November":
                    return 11;
                case "December":
                    return 12;
            }
            return 0;
        }
        #endregion
        #region Day
        private void buildDays() {
            int x = 6, y = 26;
            for (int i = 0; i < daysInMonth; i++) {
                if (i + 1 == selectedDay)
                    calenderHolder.Controls.Add(getLabelDay((i + 1).ToString(), new Point(x, y), true));
                else
                    calenderHolder.Controls.Add(getLabelDay((i + 1).ToString(), new Point(x, y)));

                x += 24;
                if (x > 150) {
                    x = 6;
                    y += 22;
                }
            }
        }
        private Label getLabelDay(string text, Point location, bool selected = false) {
            Label day = new Label();
            if (selected) {
                day.BackColor = ColorTranslator.FromHtml("#f2b600");
                day.ForeColor = Color.Black;
            } else {
                day.BackColor = Color.Transparent;
                day.ForeColor = Color.White;
                day.Click += Day_Click;
                day.Cursor = Cursors.Hand;
            }
            day.Font = new Font("Arial", 8, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            day.Location = location;
            day.Size = new Size(22, 20);
            day.Text = text;
            day.TextAlign = ContentAlignment.MiddleCenter;
            day.Name = "lblDay";
            day.Paint += pnlI1_Paint;
            day.MouseEnter += Day_MouseEnter;
            day.MouseLeave += Day_MouseLeave;
            return day;
        }
        private void Day_Click(object sender, EventArgs e) {
            Control con = ((Control)sender).Parent;
            Label label = ((Label)sender);
            date = label.Text + "/" + (selectedMonthIndex+1) + "/" + con.Controls.Find("lblYear", true)[0].Text;

            foreach (Label lbl in con.Controls)
                if (lbl.Name == "lblDay" && lbl.BackColor != Color.Transparent) {
                    lbl.BackColor = Color.Transparent;
                    lbl.ForeColor = Color.White;
                    lbl.Click += Day_Click;
                    lbl.Cursor = Cursors.Hand;
                }

            label.BackColor = ColorTranslator.FromHtml("#f2b600");
            label.ForeColor = Color.Black;
            selectedDay = Convert.ToInt16(label.Text);
        }
        private void Day_MouseLeave(object sender, EventArgs e) {
            Label label = (Label)sender;
            if (label.Text == selectedDay.ToString()) {
                label.BackColor = ColorTranslator.FromHtml("#f2b600");
                label.ForeColor = Color.Black;
            } else {
                label.BackColor = Color.Transparent;
                label.ForeColor = Color.White;
            }
        }
        private void Day_MouseEnter(object sender, EventArgs e) {
            Label label = (Label)sender;
            if (label.Text != selectedDay.ToString()) {
                label.BackColor = ColorTranslator.FromHtml("#f2b600");
                label.ForeColor = Color.Black;
            }
        }
        #endregion
    }
}