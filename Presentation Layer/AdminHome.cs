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
    public partial class AdminHome : Form
    {
        Admin a = new Admin();
        string id, adminPicPath; 

        public AdminHome(string id)
        {
            InitializeComponent();
            this.id = id;
            label1.Text = "Logged in Admin ID :" + id;
        }


        private void AdminHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginPage LG = new LoginPage();
            LG.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChangePIN cp = new ChangePIN(id, "Admin");
            this.Hide();
            cp.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AdminAccessAccounts aaa = new AdminAccessAccounts(id);
            this.Hide();
            aaa.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminEditProfile aep = new AdminEditProfile(id);
            this.Hide();
            aep.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AdminAddCourse op = new AdminAddCourse(id);
            this.Hide();
            op.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AdminAddBatch aab = new AdminAddBatch(id);
            this.Hide();
            aab.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ///insert in to admin
            AdminAddAdmin aad = new AdminAddAdmin(id);
            this.Hide();
            aad.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //confirm advisor invalid reg
            AdminConfirmAdvisor acd = new AdminConfirmAdvisor(id);
            this.Hide();
            acd.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //confirm examinee invalid reg
            AdminConfirmExaminee ace = new AdminConfirmExaminee(id);
            this.Hide();
            ace.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //update advisor course
        }

        private void AdminHome_Load(object sender, EventArgs e)
        {
            //getprofile
            List<string> list = new List<string>();
            list = a.GetAdminProfile(Convert.ToInt32(id));

            foreach (string item in list)
            {
                //list[0]; //id
                //textBox3.Text = list[1]; //name
                label8.Text = list[1];
                //textBox1.Text = list[2]; //username
                label11.Text = list[2];

                if (list[3].Equals("Female"))
                {
                    // radioButton2.Select();
                    label16.Text = "Female";
                }
                else if (list[3].Equals("Male"))
                {
                    //radioButton1.Select();
                    label16.Text = "Male";
                }

                //dateTimePicker1.Text = list[4]; //DOB
                label12.Text = list[4];
                //comboBox3.Text = list[5]; //maritial status
                label13.Text = list[5];
                //textBox2.Text = list[6]; //email
                label19.Text = list[6];
                //comboBox1.Text = list[7]; //BloodGroup
                label15.Text = list[7];
                adminPicPath = list[8];
                //textBox6.Text = list[9]; //phone
                label18.Text = list[9];
                pictureBox2.ImageLocation = adminPicPath; //photo
                textBox7.Text = list[10]; //address
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            AdminBillManagement abm = new AdminBillManagement(id);
            this.Hide();
            abm.Show();
        }


        private void button12_Click_1(object sender, EventArgs e)
        {
            AdminNotice an = new AdminNotice(id);
            this.Hide();
            an.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            AdminAccessLoginRecords aalr = new AdminAccessLoginRecords(id);
            this.Hide();
            aalr.Show();

        }

    
    }
}
