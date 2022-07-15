using ForeRunners.GUI;
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

namespace Calendar {
    public partial class Form1 : MetroForm {

        
        public Form1() {
            InitializeComponent();

            Calender cal = new Calender(this, new Point(324, 218), 2013);

            Console.WriteLine(cal.CurrentDate);
        }
        
    }
}