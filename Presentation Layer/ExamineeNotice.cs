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
    public partial class ExamineeNotice : Form
    {
        Examinee ee = new Examinee();
        string id;
        public ExamineeNotice(string id)
        {
            InitializeComponent();
            this.id = id;
            label1.Text = "Logged in Examinee ID : " + id;
        }

        private void ExamineeNotice_Load(object sender, EventArgs e)
        {
            DataTable t = ee.GetExamineeNotice(id);
            dataGridView1.DataSource = t;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ExamineeEditProfile eep = new ExamineeEditProfile(id);
            this.Hide();
            eep.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChangePIN cp = new ChangePIN(id, "Examinee");
            this.Hide();
            cp.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginPage lg = new LoginPage();
            this.Hide();
            lg.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ExamineeHome eh = new ExamineeHome(id);
            this.Hide();
            eh.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ExamineeHome eh = new ExamineeHome(id);
            this.Hide();
            eh.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ExamineeNotice En = new ExamineeNotice(id);
            this.Hide();
            En.Show();
        }

        private void ExamineeNotice_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
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
                textBox1.Text = row.Cells["Notice"].Value.ToString();
            }
        }
    }
}
