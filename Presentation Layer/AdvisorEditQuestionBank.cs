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
    public partial class AdvisorEditQuestionBank : Form
    {
        Advisor ad = new Advisor();
        string id = "";
        List<string> qList = new List<string>();

        public AdvisorEditQuestionBank(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void AdvisorEditQuestionBank_Load(object sender, EventArgs e)
        {        
            DataTable t = ad.GetQuestionBank(id);
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
                textBox1.Text = row.Cells["QID"].Value.ToString();
                textBox6.Text = row.Cells["QUE"].Value.ToString();
                comboBox1.Text= row.Cells["TOPICID"].Value.ToString();
                comboBox2.Text= row.Cells["Q_TYPE"].Value.ToString();
                textBox2.Text = row.Cells["OPTIONA"].Value.ToString();
                textBox3.Text = row.Cells["OPTIONB"].Value.ToString();
                textBox4.Text = row.Cells["OPTIONC"].Value.ToString();
                textBox5.Text = row.Cells["OPTIOND"].Value.ToString();
                textBox7.Text = row.Cells["CORRECTOPTION"].Value.ToString();
                comboBox3.Text = row.Cells["TOPICNAME"].Value.ToString();
            }
        }



        private void AdvisorEditQuestionBank_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = ad.GetLastQueID().ToString();

            comboBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox3.Text = "";
            comboBox2.Text = "";

           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //delete

            try
            {
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    string checkBoxValue = Convert.ToBoolean(dr.Cells[0].Value).ToString();
                    if (checkBoxValue.Equals("True"))
                    {
                        qList.Add(dr.Cells["QID"].Value.ToString());
                    }
                }
            }
            catch (Exception ew)
            {

            }

            foreach (string value in qList)
            {
                ad.DeleteQueBank(Convert.ToInt32(value));
            }
            qList.Clear();
            MessageBox.Show("Question Deleted");

            DataTable t = ad.GetQuestionBank(id);
            dataGridView1.DataSource = t;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //update
            string que = textBox6.Text;
            int topicID = Convert.ToInt32(comboBox1.Text);
            string qType = comboBox2.Text;
            string optionA = textBox2.Text;
            string optionB = textBox3.Text;
            string optionC = textBox4.Text;
            string optionD = textBox5.Text;
            string correctoption = textBox7.Text;
            int updateQID = Convert.ToInt32(textBox1.Text);
            string result = ad.UpdateQueBank(que, topicID, qType, optionA, optionB, optionC, optionD, correctoption, updateQID);

            MessageBox.Show(result);

            DataTable t = ad.GetQuestionBank(id);
            dataGridView1.DataSource = t;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //insert
            int qID = Convert.ToInt32(ad.GetLastQueID().ToString());
            string que =textBox6.Text;
            int topicID = Convert.ToInt32(comboBox1.Text);
            string qType= comboBox2.Text;
            string optionA = textBox2.Text;
            string optionB = textBox3.Text;
            string optionC =textBox4.Text;
            string optionD =textBox5.Text;
            string correctoption = textBox7.Text;
            string topicName = comboBox3.Text;

            MessageBox.Show(ad.InsertQestionBank(qID, que, topicID, qType, optionA, optionB, optionC, optionD, correctoption));

            DataTable t = ad.GetQuestionBank(id);
            dataGridView1.DataSource = t; 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //back
            AdvisorHome ah = new AdvisorHome(id);
            this.Hide();
            ah.Show();
            

        }

        private void comboBox3_MouseClick(object sender, MouseEventArgs e)
        {
            //getcourseid
            DataTable dt = ad.GetAdvisorTopicName(id);

            comboBox3.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                object cellData = row["TOPICNAME"];
                comboBox3.Items.Add(cellData.ToString());
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Text = ad.GetSpecificTopicID(comboBox3.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataTable t = ad.GetQuestionBank(id);
            dataGridView1.DataSource = t;            
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

      

    }
}
