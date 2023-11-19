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
    public partial class AdminAccessAccounts : Form
    {
        Admin a = new Admin();
        string id, tableName; 

        public AdminAccessAccounts(string id)
        {
            InitializeComponent();
            this.id = id;
            label1.Text = label1.Text + id;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChangePIN cp = new ChangePIN(id,"Admin");
            this.Hide();
            cp.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminEditProfile aep = new AdminEditProfile(id);
            this.Hide();
            aep.Show();
        }




        private void button10_Click(object sender, EventArgs e)
        {

            string status = a.GetAccStatus(textBox1.Text);

            if (status.Equals("Admin"))
            {
                DataTable t = a.GetThisAccount(Convert.ToInt32(textBox1.Text), "ADMINACCOUNTS");
                dataGridView1.DataSource = t;  
            }
            else if (status.Equals("Advisor"))
            {
                DataTable t = a.GetThisAccount(Convert.ToInt32(textBox1.Text), "ADVISORACCOUNTS");
                dataGridView1.DataSource = t;      
            }
            else if (status.Equals("Examinee"))
            {
                DataTable t = a.GetThisAccount(Convert.ToInt32(textBox1.Text), "EXAMINEEACCOUNTS");
                dataGridView1.DataSource = t;  
            }
                  
        }

        private void AdminAccessAccounts_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button10_Click(sender, e);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdminHome ah = new AdminHome(id);
            this.Hide();
            ah.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginPage LG = new LoginPage();
            LG.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tableName = comboBox1.SelectedItem.ToString();
            if(tableName.Equals("Admin"))
            {
                DataTable t = a.GetAdminAccounts();
                dataGridView1.DataSource = t;
            }
            else if(tableName.Equals("Advisor"))
            {
                DataTable t = a.GetAdvisorAccounts();
                dataGridView1.DataSource = t;
            }
            else if (tableName.Equals("Examinee"))
            {
                DataTable t = a.GetExamineeAccounts();
                dataGridView1.DataSource = t;
            }
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
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //valid
            string status = a.GetAccStatus(textBox1.Text);
            string tableName;
            if (status.Equals("Advisor"))
            {
                tableName = "ADVISORACCOUNTS";
                MessageBox.Show(a.ValidACC(textBox1.Text,tableName));
              
                DataTable t = a.GetAdvisorAccounts();
                dataGridView1.DataSource = t;


            }
            else if (status.Equals("Examinee"))
            {
                tableName = "EXAMINEEACCOUNTS";
                MessageBox.Show(a.ValidACC(textBox1.Text, tableName));

                DataTable t = a.GetExamineeAccounts();
                dataGridView1.DataSource = t;
            }
            else if (status.Equals("Admin"))
            {
                tableName = "ADMINACCOUNTS";
                MessageBox.Show(a.ValidACC(textBox1.Text, tableName));

                DataTable t = a.GetAdminAccounts();
                dataGridView1.DataSource = t;
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            //invalid
            string status = a.GetAccStatus(textBox1.Text);
            string tableName;
            if (status.Equals("Advisor"))
            {
                tableName = "ADVISORACCOUNTS";
                MessageBox.Show(a.InvalidACC(textBox1.Text, tableName));
            }
            else if (status.Equals("Examinee"))
            {
                tableName = "EXAMINEEACCOUNTS";
                MessageBox.Show(a.InvalidACC(textBox1.Text, tableName));
            }
            else if (status.Equals("Admin"))
            {
                tableName = "ADMINACCOUNTS";
                MessageBox.Show("Sorry! Can't Invalid Admin Account");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            LoginPage lg = new LoginPage();
            this.Hide();
            lg.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            AdminNotice An = new AdminNotice(id);
            this.Hide();
            An.Show();
        }


    }
}
