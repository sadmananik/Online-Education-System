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
    public partial class AdminNotice : Form
    {
        Admin a = new Admin();

        string id;
        public AdminNotice(string id)
        {
            InitializeComponent();
            this.id = id;
            label1.Text = "Logged in Admin ID : " + id;
        }



        private void AdminNotice_Load(object sender, EventArgs e)
        {
            DataTable t = a.GetAllNotice();
            dataGridView1.DataSource = t;    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminEditProfile aep = new AdminEditProfile(id);
            this.Hide();
            aep.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginPage lg = new LoginPage();
            this.Hide();
            lg.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdminHome ah = new AdminHome(id);
            this.Hide();
            ah.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChangePIN cp = new ChangePIN(id, "Admin");
            this.Hide();
            cp.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AdminHome ah = new AdminHome(id);
            this.Hide();
            ah.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AdminNotice an = new AdminNotice(id);
            this.Hide();
            an.Show();
        }

        private void AdminNotice_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string lastID = a.GetLastNoticeID().ToString();
            string date = DateTime.Today.ToString("dd-MM-yyyy");
            string time = DateTime.Now.ToString("h:mm:ss tt");
            string result=a.UploadNotice(lastID, textBox2.Text, date, "Admin",  id, time);
            MessageBox.Show(result);

            DataTable t = a.GetAllNotice();
            dataGridView1.DataSource = t;    
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show(a.DeleteNotice(int.Parse(textBox1.Text)));
            //delete notice
            DataTable t = a.GetAllNotice();
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
                textBox1.Text = row.Cells["NoticeID"].Value.ToString();
                textBox2.Text = row.Cells["Notice"].Value.ToString();  
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}
