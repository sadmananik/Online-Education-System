using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Logic_Layer;

namespace Presentation_Layer
{
    public partial class AdvisorCourseDetails : Form
    {
        Advisor a = new Advisor();
        string id;
        public AdvisorCourseDetails(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void AdvisorCourseDetails_Load(object sender, EventArgs e)
        {

            DataTable t = a.GetAdvisorCourseDetails(id);
            dataGridView1.DataSource = t;    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdvisorHome ah = new AdvisorHome(id);
            this.Hide();
            ah.Show();
        }
    }
}
