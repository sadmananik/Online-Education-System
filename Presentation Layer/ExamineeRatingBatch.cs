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
    public partial class ExamineeRatingBatch : Form
    {
        Examinee eee = new Examinee();
        string id; 

        public ExamineeRatingBatch(string id)
        {
            InitializeComponent();
            this.id=id;
            label1.Text = "Logged in Examinee ID : " + id;
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

        private void ExamineeRatingCourse_Load(object sender, EventArgs e)
        {
            //gradeview
            DataTable t1 = eee.GetExamineeRating(id);
            dataGridView1.DataSource = t1;

        }


        private void button7_Click(object sender, EventArgs e)
        {
            ExamineeNotice En = new ExamineeNotice(id);
            this.Hide();
            this.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int counter = eee.GetBatchRatingCounter(textBox1.Text)+1;
            double rating = (eee.GetBatchRating(textBox1.Text)+int.Parse(comboBox1.Text))/counter;
            MessageBox.Show(eee.UpdateBatchRating(textBox1.Text,rating,counter, id, int.Parse(comboBox1.Text)));
            ExamineeHome eh = new ExamineeHome(id);
            eh.Show();
            this.Hide();
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

            }
        }



    }
}
