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
    public partial class AdvisorCheckResult : Form
    {
        string id;
        Advisor ad = new Advisor();

        public AdvisorCheckResult(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdvisorHome ah = new AdvisorHome(id);
            this.Hide();
            ah.Show();
        }

        private void AdvisorCheckResult_Load(object sender, EventArgs e)
        {
            DataTable t = ad.AdvisorGetExamineeResult(id);
            dataGridView1.DataSource = t;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //update
            MessageBox.Show(ad.AdvisorComment(int.Parse(textBox3.Text), textBox2.Text));
            DataTable t = ad.AdvisorGetExamineeResult(id);
            dataGridView1.DataSource = t;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            AdvisorHome ah = new AdvisorHome(id);
            this.Hide();
            ah.Show();
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            DataGridViewCell cell = null;
            foreach (DataGridViewCell selectedCell in dataGridView1.SelectedCells)
            {
                cell = selectedCell;
                break;
            }
            if (cell != null)
            {
                DataGridViewRow row = cell.OwningRow;
                textBox1.Text = row.Cells["ACCID"].Value.ToString();
                textBox3.Text = row.Cells["RECID"].Value.ToString();
            }
        }
    }
}
