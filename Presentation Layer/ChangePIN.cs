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
    public partial class ChangePIN : Form
    {
        string id,status;

        public ChangePIN(string id, String status)
        {
            InitializeComponent();
            this.id = id;
            this.status=status;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //back
            if(status.Equals("Admin"))
            {
                AdminHome ah = new AdminHome(id);
                this.Hide();
                ah.Show();
            }
            else if(status.Equals("Advisor"))
            {
                AdvisorHome ah = new AdvisorHome(id);
                this.Hide();
                ah.Show();
            }
            else if (status.Equals("Examinee"))
            {
                ExamineeHome ah = new ExamineeHome(id);
                this.Hide();
                ah.Show();
            }

        }




        private void button1_Click(object sender, EventArgs e)
        {
            string oldPIN = "";

            if (textBox3.Text.Equals(textBox2.Text))
            {
                
                if (status.Equals("Admin"))
                {
                    Admin a = new Admin();
                    oldPIN = a.GetOldPIN(id).ToString();
                    if (oldPIN.Equals(textBox1.Text))
                    {
                        MessageBox.Show(a.ChangePIN(int.Parse(id), int.Parse(textBox3.Text)));
                        button2_Click(sender,e);
                    }
                    else
                    {
                        MessageBox.Show("Old PIN doesn't Match! Try Again");
                    }

                }
                else if (status.Equals("Advisor"))
                {
                    Advisor ad = new Advisor();
                    oldPIN = ad.GetOldPIN(id).ToString();
                    if (oldPIN.Equals(textBox1.Text))
                    {
                        MessageBox.Show(ad.ChangePIN(int.Parse(id), int.Parse(textBox3.Text)));
                        button2_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Old PIN doesn't Match! Try Again");
                    }

                }
                else if (status.Equals("Examinee"))
                {
                    Examinee eee = new Examinee();
                    oldPIN = eee.GetOldPIN(id).ToString();
                    if (oldPIN.Equals(textBox1.Text))
                    {
                        MessageBox.Show(eee.ChangePIN(int.Parse(id), int.Parse(textBox3.Text)));
                        button2_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Old PIN doesn't Match! Try Again");
                    }

                }

            }
            else
            {
                MessageBox.Show("Confirm PIN doesn't Match!");
            }
                         
        }

        private void ChangePassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }



    }
}
