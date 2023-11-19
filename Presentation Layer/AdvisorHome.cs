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
    public partial class AdvisorHome : Form
    {
        Advisor ad = new Advisor();
        string id, adminPicPath;

        public AdvisorHome(string id)
        {
            InitializeComponent();
            this.id = id;
            label1.Text = "Logged in Advisor ID :" + id;
        }

        private void AdvisorHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoginPage LG  = new LoginPage();
            LG.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdvisorEditQuestionBank aeqb = new AdvisorEditQuestionBank(id);
            aeqb.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ChangePIN cp = new ChangePIN(id,"Advisor");
            this.Hide();
            cp.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            LoginPage LG = new LoginPage();
            LG.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            AdvisorEditProfile adep = new AdvisorEditProfile(id);
            
            this.Hide();
            adep.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //get note of his current course
            AdvisorEditNote aen = new AdvisorEditNote(id);
            this.Hide();
            aen.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdvisorSetQuestion asq = new AdvisorSetQuestion(id);
            this.Hide();
            asq.Show();
        }

        private void AdvisorHome_Load(object sender, EventArgs e)
        {

            //getprofile
            List<string> list = new List<string>();
            list = ad.GetAdvisorProfile(Convert.ToInt32(id));

            foreach (string item in list)
            {
                //list[0]; //id
                //name
                label8.Text = list[1];
                //username
                label11.Text = list[2];

                if (list[3].Equals("Female"))
                {
                    label16.Text = "Fenmale";
                }
                else if (list[3].Equals("Male"))
                {
                    label16.Text = "Male";
                }

                //DOB
                label12.Text = list[4];
                //maritial status
                label13.Text = list[5];
                //email
                label19.Text = list[6];
                //BloodGroup
                label15.Text = list[7];
                adminPicPath = list[8];
                //phone
                label18.Text = list[9];
                pictureBox2.ImageLocation = adminPicPath; //photo
                textBox7.Text = list[10]; //address
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            AdvisorFinancialsDetails abd = new AdvisorFinancialsDetails(id);
            this.Hide();
            abd.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AdvisorCourseDetails acd = new AdvisorCourseDetails(id);
            this.Hide();
            acd.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AdvisorCheckResult eee = new AdvisorCheckResult(id);
            this.Hide();
            eee.Show();
        }


        private void button13_Click(object sender, EventArgs e)
        {
            AdvisorNotice An = new AdvisorNotice(id);
            this.Hide();
            An.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AdvisorChangeCourse aaa = new AdvisorChangeCourse(id);
            this.Hide();
            aaa.Show();
        }



    }
}
