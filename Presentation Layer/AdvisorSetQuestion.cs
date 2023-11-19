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
    public partial class AdvisorSetQuestion : Form
    {
        Advisor ad = new Advisor();
        string id, topicName="", dqid;
        List<string> qList = new List<string>();
        List<string> tList = new List<string>();

        public AdvisorSetQuestion(string id)
        {
            InitializeComponent();
            this.id=id;
        }

        private void AdvisorSetQuestion_Load(object sender, EventArgs e)
        {
            DataTable t = ad.GetAdvisorQueBank(int.Parse(id));
            dataGridView1.DataSource = t;


            DataTable t2 = ad.GetSelectedQuestion(int.Parse(id));
            dataGridView2.DataSource = t2;

        }

        private void AdvisorSetQuestion_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable t = ad.GetAdvisorQueBank(int.Parse(id));
            dataGridView1.DataSource = t;
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            DataTable dt = ad.GetAdvisorQueBank(int.Parse(id));

            comboBox1.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                object cellData = row["TOPICNAME"];
                comboBox1.Items.Add(cellData.ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string topicName = comboBox1.SelectedItem.ToString();          
            DataTable t = ad.GetTopicWiseQue(topicName, int.Parse(id));
            dataGridView1.DataSource = t;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //add insert

            try
            {
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    string checkBoxValue = Convert.ToBoolean(dr.Cells[0].Value).ToString();
                    if (checkBoxValue.Equals("True"))
                    {
                        qList.Add(dr.Cells["QID"].Value.ToString());
                        tList.Add(dr.Cells["TOPICNAME"].Value.ToString());
                    }
                }
            }
            catch (Exception ew)
            {

            }

            for (int i = 0; i < qList.Count(); i++)
            {
                string queID = qList[i];
                string tName = tList[i];

                List<string> list = new List<string>();

                list = ad.GetAdvisorSetQuestionInfo(int.Parse(id), tName);

                int batchID = int.Parse(list[0]);
                int topicID = int.Parse(list[1]);
                int courseID = int.Parse(list[2]);

                MessageBox.Show("QID "+queID+ ad.InsertSetQuestion(int.Parse(queID), batchID, topicID, courseID, id));
            }

            qList.Clear();
            tList.Clear();

            
            DataTable t2 = ad.GetSelectedQuestion(int.Parse(id));
            dataGridView2.DataSource = t2;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                if (chk.Value == chk.TrueValue)
                {
                    chk.Value = chk.FalseValue;
                }
                else
                {
                    chk.Value = chk.TrueValue;
                }
            }
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //remove
            try
            {
                foreach (DataGridViewRow dr in dataGridView2.Rows)
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
                ad.DeleteSelectedQues(int.Parse(value));
            }
            qList.Clear();
            MessageBox.Show("Question Deleted");


            DataTable t2 = ad.GetSelectedQuestion(int.Parse(id));
            dataGridView2.DataSource = t2;
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
                topicName = row.Cells["TOPICNAME"].Value.ToString(); 
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewCell cell = null;
            foreach (DataGridViewCell selectedCell in dataGridView2.SelectedCells)
            {
                cell = selectedCell;
                break;
            }
            if (cell != null)
            {
                DataGridViewRow row = cell.OwningRow;
                dqid = row.Cells["QID"].Value.ToString();
            }
                                
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdvisorHome ah = new AdvisorHome(id);
            this.Hide();
            ah.Show();
        }



       

    }
}
