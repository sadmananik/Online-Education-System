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
    public partial class AdvisorEditNote : Form
    {
        Advisor a = new Advisor();
        string id, picPath;
        public AdvisorEditNote(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void AdvisorEditNote_Load(object sender, EventArgs e)
        {
            DataTable t = a.GetAdvisorNotes(id);
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
                textBox1.Text = row.Cells["TOPICID"].Value.ToString();
                textBox2.Text = row.Cells["TOPICNAME"].Value.ToString();
                textBox3.Text = row.Cells["REFERENCE"].Value.ToString();
                picPath = row.Cells["NOTES"].Value.ToString();
                textBox4.Text = row.Cells["TYPE"].Value.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)| *.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                picPath = dlg.FileName.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //update
            int topicID=int.Parse(textBox1.Text);
            string topicName = textBox2.Text;
            string type = textBox4.Text;
            int courseID = a.GetAdvisorCourseID(id);  
            string  notes= picPath;  
            string reference= textBox3.Text;

            MessageBox.Show(a.UpdateNotes( topicID,  topicName,  type,  courseID,  notes,  reference));

            DataTable t = a.GetAdvisorNotes(id);
            dataGridView1.DataSource = t;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //insert
            int topicID = int.Parse(a.GetLastTopicID().ToString()) ;
            string topicName = textBox2.Text;
            string type = textBox4.Text;
            int courseID = a.GetAdvisorCourseID(id);
            string notes = picPath;
            string reference = textBox3.Text;

            MessageBox.Show(a.InsertNotes(topicID, topicName, type, courseID, notes, reference));

            DataTable t = a.GetAdvisorNotes(id);
            dataGridView1.DataSource = t;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //delete
            MessageBox.Show(a.DeleteNotes(int.Parse(textBox1.Text)));

            DataTable t = a.GetAdvisorNotes(id);
            dataGridView1.DataSource = t;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //clear
            textBox1.Text = a.GetLastTopicID().ToString();
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //back
            AdvisorHome ah = new AdvisorHome(id);
            this.Hide();
            ah.Show();
        }

        private void AdvisorEditNote_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
