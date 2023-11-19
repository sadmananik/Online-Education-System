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
    public partial class AdminAccessLoginRecords : Form
    {
        Admin a = new Admin(); 

        string id;
        public AdminAccessLoginRecords(string id)
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

        private void AdminAccessLoginRecords_Load(object sender, EventArgs e)
        {
            DataTable t = a.GetLoginRec();
            dataGridView1.DataSource = t;  
        }

        private void label2_Click(object sender, EventArgs e)
        {
            DataTable t = a.GetLoginRec(textBox1.Text);
            dataGridView1.DataSource = t;  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable t = a.GetLoginRec();
            dataGridView1.DataSource = t;  
        }

        private void AdminAccessLoginRecords_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminEditProfile aep = new AdminEditProfile(id);
            this.Hide();
            aep.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChangePIN cp = new ChangePIN(id, "Admin");
            this.Hide();
            cp.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoginPage lg = new LoginPage();
            this.Hide();
            lg.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminHome ah = new AdminHome(id);
            this.Hide();
            ah.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AdminNotice An = new AdminNotice(id);
            this.Hide();
            An.Show();
        }



    }
}
