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
    public partial class AdminAddBatch : Form
    {
        Admin a = new Admin();
        string id;

        public AdminAddBatch(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //update UpdateBatch
            string batchID = textBox1.Text;
            string batchName = textBox2.Text;
            string prog = comboBox1.Text;
            string courseID = comboBox2.Text;
            string advisorID = comboBox3.Text;
            string fee = textBox3.Text;
            string status = comboBox4.Text;
            string result = a.UpdateBatch(batchID, batchName, courseID, advisorID, fee, prog, status);
            MessageBox.Show(result);

            DataTable t = a.GetBatch();
            dataGridView1.DataSource = t;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //insert
            string batchID = a.GetLastBatchID().ToString();
            string batchName = textBox2.Text;
            string prog = comboBox1.Text;
            string courseID = comboBox2.Text;
            string advisorID = comboBox3.Text;
            string fee = textBox3.Text;
            string status = comboBox4.Text;
            string result = a.InsertBatch(batchID, batchName, courseID, advisorID, fee, prog, status);
            MessageBox.Show(result);

            DataTable t = a.GetBatch();
            dataGridView1.DataSource = t;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //delete
            int batchID= Convert.ToInt32(textBox1.Text);
            MessageBox.Show(a.DeleteCourse(batchID));
            DataTable t = a.GetBatch();
            dataGridView1.DataSource = t;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //clear
            textBox1.Text = a.GetLastBatchID().ToString();
            textBox3.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //back
            AdminHome ah = new AdminHome(id);
            this.Hide();
            ah.Show();
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
                textBox2.Text = row.Cells["BATCHNAME"].Value.ToString();
                textBox3.Text = row.Cells["FEETK"].Value.ToString();//
                comboBox1.Text = row.Cells["P_SESSION"].Value.ToString();
                comboBox2.Text = row.Cells["COURSEID"].Value.ToString();
                comboBox3.Text = row.Cells["ADVISORID"].Value.ToString();
                comboBox4.Text = row.Cells["STATUS"].Value.ToString();
            }
        }

        private void AdminAddBatch_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            //getcourseid
            DataTable dt = a.GetCourse();

            comboBox2.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                object cellData = row["COURSEID"];
                comboBox2.Items.Add(cellData.ToString());
            }
        }

        private void comboBox3_MouseClick(object sender, MouseEventArgs e)
        {
            //getadvisorid
            DataTable dt = a.GetAdvisorID();

            comboBox3.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                object cellData = row["ACCID"];
                comboBox3.Items.Add(cellData.ToString());
            }
        }

        private void AdminAddBatch_Load(object sender, EventArgs e)
        {
            DataTable t = a.GetBatch();
            dataGridView1.DataSource = t;
        }

    }
}
