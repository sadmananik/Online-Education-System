using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using Business_Logic_Layer;

namespace Presentation_Layer
{
    public partial class ForgotPIN : Form
    {
        Visitor lg = new Visitor();
        string email = "", secretAns = "", status ="";

        public ForgotPIN()
        {
            InitializeComponent();
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoginPage lp = new LoginPage();
            lp.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Equals(textBox4.Text))
            {
                DialogResult result = MessageBox.Show(lg.ForgetPassChange(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox4.Text)), "Password Changed", MessageBoxButtons.OK, MessageBoxIcon.None);
                if (result == DialogResult.OK)
                {
                    LoginPage lp = new LoginPage();
                    lp.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Confirm Password Not Matched");
            }
             
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            if (lg.CheckSecreAnswer(textBox1.Text,textBox2.Text).Equals("Matched"))
            {
                panel1.Visible = false;
                panel2.Visible = false;
                panel3.Visible = true;
            }
            else if (lg.CheckSecreAnswer(textBox1.Text, textBox2.Text).Equals("Incorrect ANS"))
            {
                MessageBox.Show("Answer not Matched! Try Again.", "Incorrect Answer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else 
            {
                MessageBox.Show(lg.CheckSecreAnswer(textBox1.Text, textBox2.Text));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoginPage lp = new LoginPage();
            lp.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (lg.CheckID(textBox1.Text).Equals("Found"))
            {
                panel1.Visible = false;
                panel2.Visible = true;
                panel3.Visible = false;
            }
            else
            {
                MessageBox.Show("This ID is not regested yet!","Invalid ID", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Forgot_Password_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button6_Click(sender, e);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }


        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            
                if (e.KeyChar == (char)Keys.Enter)
            {
                button4_Click(sender, e);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            status = lg.GetAccStatus(textBox1.Text);
            
            if(status.Equals("Admin"))
            {
                list = lg.GetEmailAndSecretAns(textBox1.Text, "ADMINACCOUNTS");
                email = list[0];
                secretAns = list[1];
                MessageBox.Show("Check Your Email For Secrect Ans");
                LoginPage h = new LoginPage();
                this.Hide();
                h.Show();

                SendSecretAnsViaEmailSMTP( email,  secretAns);
            }
            else if (status.Equals("Advisor"))
            {
                list = lg.GetEmailAndSecretAns(textBox1.Text, "ADVISORACCOUNTS");
                email = list[0];
                secretAns = list[1];
                MessageBox.Show("Check Your Email For Secrect Ans");
                LoginPage h = new LoginPage();
                this.Hide();
                h.Show();

                SendSecretAnsViaEmailSMTP( email,  secretAns);
            }
            else if (status.Equals("Examinee"))
            {
                list = lg.GetEmailAndSecretAns(textBox1.Text, "EXAMINEEACCOUNTS");
                email = list[0];
                secretAns = list[1];
                MessageBox.Show("Check Your Email For Secrect Ans");
                LoginPage h = new LoginPage();
                this.Hide();
                h.Show();

                SendSecretAnsViaEmailSMTP(email, secretAns);

            }

            list.Clear();
            list.Remove(email);
            list.Remove(secretAns);

        }


        private void SendSecretAnsViaEmailSMTP(string email, string ans)
        {
            
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("onlineedusystem@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Secret Ans of Online Education System";
                mail.Body = "Your ID :"+textBox1.Text+" Your Secret Ans :" +secretAns;
                //mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("onlineedusystem@gmail.com", "10aiubcsfest2k17");
                    smtp.EnableSsl = true;
                    try
                    {
                        smtp.Send(mail);
                        MessageBox.Show("Secret Ans Mail SuccessFully Send to Your Mail");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }





    }
}
