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
using System.Threading;

namespace Presentation_Layer
{
    public partial class LoginPage : Form
    {
        Visitor li = new Visitor();
        string loginResult, loginStatus, loginID;


        public LoginPage()
        {
            Thread t = new Thread(new ThreadStart(StartWindows));
            t.Start();
            Thread.Sleep(2100);

            InitializeComponent();

            t.Abort();
            timer1.Start();
        }

        public void StartWindows()
        {
            Application.Run(new StartWindows()) ;
        }

        private void LoginPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }        


        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ForgotPIN fp = new ForgotPIN();
            this.Hide();
            fp.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //reg
            TermsAndConditions tc = new TermsAndConditions();
            this.Hide();
            tc.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (button3.ForeColor == Color.Black)
                button3.ForeColor = Color.White;
            else
                button3.ForeColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string pin = textBox2.Text;

            List<string> list = new List<string>();

            list = li.LoginD(id, pin);
            loginResult = list[0];
            loginStatus = list[1];
            loginID =list[2];

            
            if (loginResult.Equals("Found"))
            {
                if (loginStatus.Equals("Admin"))
                {
                    int lastLogRecID = li.GetLastLogRecID();
                    string date = DateTime.Today.ToString("dd-MM-yyyy");
                    string time = DateTime.Now.ToString("h:mm:ss tt");
                    li.InsertLoginRecord(lastLogRecID, loginID, date, time, "0", "0", loginStatus);

                    AdminHome ah = new AdminHome(loginID);
                    this.Hide();
                    ah.Show();
                }
                else if (loginStatus.Equals("Advisor"))
                {
                    if (li.CheckAdvisorValidity(id).Equals("Valid"))
                    {
                        List<string> loginfo = new List<string>();
                        loginfo = li.GetBatchIDAndCourseID(loginID, "ADVISORACCOUNTS");
                        string batchID = loginfo[0];
                        string courseID = loginfo[1];
                        int lastLogRecID = li.GetLastLogRecID();
                        string date = DateTime.Today.ToString("dd-MM-yyyy");
                        string time = DateTime.Now.ToString("h:mm:ss tt");
                        li.InsertLoginRecord(lastLogRecID, loginID, date, time, courseID, batchID, loginStatus);
                        loginfo.Remove(batchID);
                        loginfo.Remove(courseID);

                        AdvisorHome adh = new AdvisorHome(loginID);
                        this.Hide();
                        adh.Show();
                    }
                    else
                    {   
                        li.CheckAdvisorValidity(id);
                        MessageBox.Show("Your Account Not Confirmed Yet. Please, Check Your Email.", "Invalid Account ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);                      
                    }

                }
                else if (loginStatus.Equals("Examinee"))
                {

                    List<string> loginfo = new List<string>();
                    loginfo = li.GetBatchIDAndCourseID(loginID, "EXAMINEEACCOUNTS");
                    string batchID = loginfo[0];
                    string courseID = loginfo[1];
                    int lastLogRecID = li.GetLastLogRecID();
                    string date = DateTime.Today.ToString("dd-MM-yyyy");
                    string time = DateTime.Now.ToString("h:mm:ss tt");
                    li.InsertLoginRecord(lastLogRecID, loginID, date, time, courseID, batchID, loginStatus);
                    loginfo.Remove(batchID);
                    loginfo.Remove(courseID);

                    ExamineeHome eh = new ExamineeHome(loginID);
                    this.Hide();
                    eh.Show();
                }

            }

            else
            {
                MessageBox.Show(loginResult ,"Wrong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            textBox2.Text = "";
            list.Remove(loginResult);
            list.Remove(loginStatus);
            list.Remove(loginID);
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


      
       
    }
}
