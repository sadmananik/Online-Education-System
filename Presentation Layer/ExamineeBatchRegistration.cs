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
    public partial class ExamineeBatchRegistration : Form
    {
        Examinee eee = new Examinee();
        string id;


        public ExamineeBatchRegistration(string id)
        {
            InitializeComponent();
            this.id = id;
        }

 

        private void button3_Click(object sender, EventArgs e)
        {
            ExamineeEditProfile eep = new ExamineeEditProfile(id);
            this.Hide();
            eep.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ChangePIN cp = new ChangePIN(id, "Examinee");
            this.Hide();
            cp.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginPage LG = new LoginPage();
            LG.Show();
            this.Hide();
        }

        private void ExamineeBatchRegistration_Load(object sender, EventArgs e)
        {
            DataTable t = eee.GetBatchForRegistration();
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
                textBox1.Text = row.Cells["BATCHID"].Value.ToString();
                textBox3.Text = row.Cells["COURSENAME"].Value.ToString();
                textBox2.Text = row.Cells["BATCHNAME"].Value.ToString();
                textBox5.Text = row.Cells["FEETK"].Value.ToString();
                textBox4.Text = row.Cells["NAME"].Value.ToString();
                textBox6.Text = row.Cells["P_SESSION"].Value.ToString();

            }
        }

        private void ExamineeBatchRegistration_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ExamineeHome eee = new ExamineeHome(id);
            this.Hide();
            eee.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {

            DialogResult Dresult = MessageBox.Show("Are you sure?", "Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (Dresult == DialogResult.Yes)
            {         
                //insert in to reg
                int lastRegID = eee.GetLastRegID();
                string examineeID = id;
                string batchID = textBox1.Text;
                string validity = "Invalid";

                string result = eee.InsertCourseRegistration(lastRegID, examineeID, batchID, validity);
                MessageBox.Show(result+" \n To valid Course pay Course Fee at our bKash (01700000000) with your ID as reference \n");
            }           
        }


        private void button5_Click(object sender, EventArgs e)
        {
            ExamineeNotice En = new ExamineeNotice(id);
            this.Hide();
            En.Show();
        }

      
    }
}
