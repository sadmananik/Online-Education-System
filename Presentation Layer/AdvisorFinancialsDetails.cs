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
    public partial class AdvisorFinancialsDetails : Form
    {
        Advisor adh = new Advisor();
        string id;
        public AdvisorFinancialsDetails(string id)
        {
            InitializeComponent();
            this.id = id;
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdvisorHome adh = new AdvisorHome(id);
            this.Hide();
            adh.Show();
        }

        private void AdvisorBillingDetails_Load(object sender, EventArgs e)
        {
            DataTable t = adh.GetAdvisorBillingDetails(id);
            dataGridView1.DataSource = t;    
        }
    }
}
