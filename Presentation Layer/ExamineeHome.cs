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
    public partial class ExamineeHome : Form
    {
        Examinee eee = new Examinee();
        string id, adminPicPath;
        public ExamineeHome(string id)
        {
            InitializeComponent();
            this.id = id;
            label1.Text = "Welcome Home Examinee ID :" + id;
        }

        private void ExaminesHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoginPage LG = new LoginPage();
            LG.Show();
            this.Hide();
        }


  
        private void button3_Click_2(object sender, EventArgs e)
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            LoginPage LG = new LoginPage();
            LG.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ExamineeNote a = new ExamineeNote(id);
            this.Hide();
            a.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (int.Parse(eee.GetRegisteredBatch(id)) > 0)
            {
                MessageBox.Show("You are Already in a Batch. Complete this First.", "Batch!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                ExamineeBatchRegistration ebr = new ExamineeBatchRegistration(id);
                this.Hide();
                ebr.Show();
            }
        }

        private void ExamineeHome_Load(object sender, EventArgs e)
        {
            //getprofile
            List<string> list = new List<string>();
            list = eee.GetExamineeProfile(Convert.ToInt32(id));

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

        private void button10_Click(object sender, EventArgs e)
        {
            ExamineeFinancialsDetails abd = new ExamineeFinancialsDetails(id);
            this.Hide();
            abd.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (eee.GetExamineeBatchStatus(id)>0)
          {
            ExamineeExam ee = new ExamineeExam(id);
            this.Hide();
            ee.Show();
          }
          else
          {
              MessageBox.Show("You are not registered to any Batch");
          }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ExamineeCheckResult eee = new ExamineeCheckResult(id);
            this.Hide();
            eee.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //batchrating
            string batchID=eee.GetExamineeBatchID(id);

            MessageBox.Show(eee.GetExamineeBatchRatingStatus(id, batchID).ToString());
            if (eee.GetExamineeBatchRatingStatus(id, batchID) == 1)
            {
                ExamineeRatingBatch ert = new ExamineeRatingBatch(id);
                this.Hide();
                ert.Show();
            }
            else
            {
                MessageBox.Show("You Already Rate This Batch");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            ExamineeNotice En = new ExamineeNotice(id);
            this.Hide();
            En.Show();
        }




    }
}
