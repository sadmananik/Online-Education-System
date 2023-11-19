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
    public partial class AdminAddCourse : Form
    {
        Admin a = new Admin();
        string id;
        public AdminAddCourse(string id)
        {
            InitializeComponent();
            this.id = id;
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
                textBox1.Text = row.Cells["COURSEID"].Value.ToString();
                textBox3.Text = row.Cells["C_PROGRAM"].Value.ToString();
                textBox2.Text = row.Cells["COURSENAME"].Value.ToString();               
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = a.GetLastCourseID().ToString();
             textBox3.Text = "";
             textBox2.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdminHome ah = new AdminHome(id);
            this.Hide();
            ah.Show();

        }

        private void OpenCourse_Load(object sender, EventArgs e)
        {
            DataTable t = a.GetCourse();
            dataGridView1.DataSource = t;
        }

        private void OpenCourse_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            //delete
            int courseID = Convert.ToInt32(textBox1.Text);
            MessageBox.Show(a.DeleteCourse(courseID));
            DataTable t = a.GetCourse();
            dataGridView1.DataSource = t;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //insert
            string courseID = a.GetLastCourseID().ToString();
            string courseName = textBox2.Text;
            string courseProg = textBox3.Text;
            string result = a.InsertCourse(courseID, courseName, courseProg);
            MessageBox.Show(result);

            DataTable t = a.GetCourse();
            dataGridView1.DataSource = t;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //update
            string courseID = textBox1.Text;
            string courseName = textBox2.Text;
            string courseProg = textBox3.Text;
            string result = a.UpdateCourse(courseID, courseName, courseProg);
            MessageBox.Show(result);

            DataTable t = a.GetCourse();
            dataGridView1.DataSource = t;
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
