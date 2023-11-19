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
    public partial class AdminConfirmAdvisor : Form
    {
        Admin a = new Admin();
        string id, email="";
        public AdminConfirmAdvisor(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminHome ah = new AdminHome(id);
            this.Hide();
            ah.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //update advisor
            List<string> list = new List<string>();

            MessageBox.Show(a.ConfirmAdvisorID(textBox1.Text, textBox4.Text,id));
           
            list = a.GetEmailAndSecretAns(textBox1.Text, "ADVISORACCOUNTS");
            email = list[0];
            SendEmailSMTP();

            list.RemoveAt(0);
            //list.RemoveAt(1);

            DataTable t1 = a.GetInvalidAdvisorAccounts();
            dataGridView1.DataSource = t1;

        }


        private void SendEmailSMTP()
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("onlineedusystem@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Your Course Request Accepted. Thanks for being with us.";
                mail.Body = "Your Batch Confirmed to :"+textBox4.Text +" Best of luck ahead. :)";
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
                        Console.WriteLine("Mail SuccessFully Send");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
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
                textBox2.Text = row.Cells["NAME"].Value.ToString();
                textBox4.Text = row.Cells["CURRENTBATCHID"].Value.ToString();               
            }
        }

        private void AdminConfirmAdvisor_Load(object sender, EventArgs e)
        {
            DataTable t = a.GetInvalidAdvisorAccounts();
            dataGridView1.DataSource = t;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable t = a.GetThisAccount(Convert.ToInt32(textBox3.Text), "ADVISORACCOUNTS");
                dataGridView1.DataSource = t;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error Search!!!"+ex.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataTable t = a.GetInvalidAdvisorAccounts();
            dataGridView1.DataSource = t;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }




    }
}
