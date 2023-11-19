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
    public partial class AdminConfirmExaminee : Form
    {
        Admin a = new Admin();
        string id;
        public AdminConfirmExaminee(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminHome ah = new AdminHome(id);
            this.Hide();
            ah.Show();
        }

        private void AdminConfirmExaminee_Load(object sender, EventArgs e)
        {
            DataTable t = a.GetReg();
            dataGridView1.DataSource = t;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(a.ConfirmExamineeByAdminID(textBox1.Text,id));
            DataTable t = a.GetReg();
            dataGridView1.DataSource = t;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
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
                textBox2.Text = row.Cells["NAME"].Value.ToString();
            }
        }

       
    }
}
