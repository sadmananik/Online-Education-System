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
    public partial class AdminBillManagement : Form
    {
        Admin a = new Admin();
        string id, tID;

        public AdminBillManagement(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdminHome ah = new AdminHome(id);
            this.Hide();
            ah.Show();
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            DataTable dt = a.GetAdvisorAccounts();

            comboBox1.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                object cellData = row["ACCID"];
                comboBox1.Items.Add(cellData.ToString());
            }
        }

        private void comboBox3_MouseClick(object sender, MouseEventArgs e)
        {
            DataTable dt = a.GetAdvisorBatch(comboBox1.Text);

            comboBox3.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                object cellData = row["BATCHID"];
                comboBox3.Items.Add(cellData.ToString());
            }
        }

        private void comboBox4_MouseClick(object sender, MouseEventArgs e)
        {
            DataTable dt = a.GetBatch();

            comboBox4.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                object cellData = row["BATCHID"];
                comboBox4.Items.Add(cellData.ToString());
            }
        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            DataTable dt = a.GetExamineeAccounts();

            comboBox2.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                object cellData = row["ACCID"];
                comboBox2.Items.Add(cellData.ToString());
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            double fee = a.GetBatchFee(comboBox3.Text); 
            textBox1.Text = fee.ToString();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            double fee = a.GetBatchFee(comboBox4.Text);
            textBox2.Text = fee.ToString();
        }

        private void AdminBillManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //pay

            int tid = a.GetLastTID();
            int accid = int.Parse(comboBox1.Text);
            float recFee = Convert.ToSingle(textBox1.Text);
            string status = "Advisor";
            string date = dateTimePicker1.Text;
            int adminID = int.Parse(id);
            int batchID = int.Parse(comboBox3.Text);


            MessageBox.Show(a.PayAdvisorBill(tid, accid, recFee, status, date, adminID, batchID));

            DataTable t = a.GetBill();
            dataGridView1.DataSource = t;
            
        }
       

        private void button6_Click(object sender, EventArgs e)
        {
            //recieve
            int tid = a.GetLastTID();
            int accid = int.Parse(comboBox2.Text); 
            double recFee=Convert.ToDouble(textBox2.Text);
            string status="Examinee";
            string  date=dateTimePicker2.Text; 
            int adminID=int.Parse(id);
            int batchID=int.Parse(comboBox4.Text); 


           MessageBox.Show(a.RecAdvisorBill(tid, accid, recFee, status, date, adminID, batchID));

           DataTable t = a.GetBill();
           dataGridView1.DataSource = t;
            
        }

        private void AdminBillManagement_Load(object sender, EventArgs e)
        {
            DataTable t = a.GetBill();
            dataGridView1.DataSource = t;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //delete
            MessageBox.Show(a.DeleteTransaction(tID));
            DataTable t = a.GetBill();
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
                tID = row.Cells["TRANSACTIONID"].Value.ToString();
                
            }
        }

        
    }
}
