using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Logic_Layer;

namespace Presentation_Layer
{
    public partial class AdvisorChangeCourse : Form
    {
        string id, getBatchID;
        Advisor ad = new Advisor();
        public AdvisorChangeCourse(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdvisorHome ah = new AdvisorHome(id);
            this.Hide();
            ah.Show();
        }

        private void comboBox4_MouseClick(object sender, MouseEventArgs e)
        {
            DataTable dt = ad.GetOpenCourseName();
            comboBox4.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                object cellData = row["COURSENAME"];
                comboBox4.Items.Add(cellData.ToString());
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            getBatchID = ad.GetLastOpenCourseBatchID(comboBox4.Text).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show("Your Request has been sent to the admin. Send Your CV. We will notify you soon vai Email");

            AdvisorHome ah = new AdvisorHome(id);
            this.Hide();
            ah.Show();
            //mail
            SendEmailSMTP();
        }



        private void SendEmailSMTP()
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("onlineedusystem@gmail.com");
                mail.To.Add("sadmananik1@gmail.com");
                mail.Subject = "Advisor "+id +" Request to Change Course";
                mail.Body = "Resuested Course : "+comboBox4.Text + "Batch ID: "+getBatchID;
                //mail.Body = "<h1>bodytext</h1>";
                //mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("onlineedusystem@gmail.com", "10aiubcsfest2k17");
                    smtp.EnableSsl = true;
                    try
                    {
                        smtp.Send(mail);
                        MessageBox.Show("Mail SuccessFully Send");
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdvisorNotice An = new AdvisorNotice(id);
            this.Hide();
            An.Show();
        }


    }
}
