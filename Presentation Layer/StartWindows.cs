using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace Presentation_Layer
{
    public partial class StartWindows : Form
    {

        public StartWindows()
        {
            InitializeComponent();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            
            rectangleShape1.Width += 10;
            if (rectangleShape1.Width == 645)
            {
                timer1.Stop();
            }

        }


    }
}
