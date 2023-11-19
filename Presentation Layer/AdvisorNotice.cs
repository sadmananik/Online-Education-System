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
    public partial class AdvisorNotice : Form
    {
        Advisor a = new Advisor();
        string adminstatus, id;
        public AdvisorNotice(string id)
        {
            InitializeComponent();
            this.id = id;
            label1.Text = "Logged in Advisor ID : " + id;
        }

        private void AdvisorNotice_Load(object sender, EventArgs e)
        {
            DataTable t = a.GetAdvisorNotice(id);
            dataGridView1.DataSource = t;    
        }

        private void label1_Click(object sender, EventArgs e)
        {
            ChangePIN cp = new ChangePIN(id, "Advisor");
            this.Hide();
            cp.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChangePIN cp = new ChangePIN(id, "Admin");
            this.Hide();
            cp.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdvisorEditProfile adep = new AdvisorEditProfile(id);
            this.Hide();
            adep.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginPage lg = new LoginPage();
            this.Hide();
            lg.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdvisorHome adh = new AdvisorHome(id);
            this.Hide();
            adh.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AdvisorHome adh = new AdvisorHome(id);
            this.Hide();
            adh.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AdvisorNotice An = new AdvisorNotice(id);
            this.Hide();
            An.Show();
        }

        private void AdvisorNotice_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            AdvisorHome ah = new AdvisorHome(id);
            this.Hide();
            ah.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!adminstatus.Equals(adminstatus))
            {
                MessageBox.Show(a.DeleteNotice(int.Parse(textBox1.Text)));
                //delete notice
                DataTable t = a.GetAdvisorNotice(id);
                dataGridView1.DataSource = t;
            }
            else
            {
                MessageBox.Show("Can't Delete Admin Notice");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string lastID = a.GetLastNoticeID().ToString();
            string date = DateTime.Today.ToString("dd-MM-yyyy");
            string time = DateTime.Now.ToString("h:mm:ss tt");
            string result = a.UploadNotice(lastID, textBox2.Text, date, "Advisor", id, time);
            MessageBox.Show(result);

            DataTable t = a.GetAdvisorNotice(id);
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
                textBox1.Text = row.Cells["NOTICEID"].Value.ToString();
                textBox2.Text = row.Cells["Notice"].Value.ToString();
                adminstatus = row.Cells["Status"].Value.ToString();
            }
        }
    }
}
