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
    public partial class FormRegistration : Form
    {
        Visitor v = new Visitor();

        string  secretQueAns,courseName, getBatchID, joinAs, picPath, gender, name, userName, DOB, maritialStatus, email, bloodGroup, photo, phone, address, joinDate, validity;
        int id, confirmPIN, lastRegID;
        bool checkGender, UniqueUserName, checkCourseName, checkSecretAns, checkBothPIN, checkNumber, checkMaritialStatus, checkEmail, checkAddress, checkUserName, checkName, checkBloodGroup, checkPIN, checkConfirmPIN, checkPicPath, checkJoinAs;

        public FormRegistration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginPage lg = new LoginPage();
            this.Hide();
            lg.Show();
        }

        private void FormRegistration_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)| *.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                picPath = dlg.FileName.ToString();
                pictureBox1.ImageLocation = picPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkUniqueUserName();
            CheckName();
            CheckUserName();
            CheckMaritialStatus();            
            CheckBloodGroup();
            CheckGender();
            CheckNumber();
            CheckEmail();
            CheckPIN();
            CheckConfirmPIN();           
            CheckJoinAs();
            CheckPicPath();
            CheckAddress();
            CheckSecretAns();
            CheckBothPIN();
            GetNEWACCID();
            CheckCourseName();

            DOB = dateTimePicker1.Text;
            joinDate =DateTime.Today.ToString("dd-MM-yyyy");
            lastRegID = v.GetLastRegID();
            id = v.GetNewAccountID();


            if (checkGender == true && UniqueUserName==true && checkCourseName == true && checkMaritialStatus == true && checkSecretAns == true && checkEmail == true && checkBloodGroup == true && checkJoinAs == true && checkName == true && checkPIN == true && checkConfirmPIN == true && checkUserName == true && checkPicPath == true && checkNumber == true && checkBothPIN == true)
            {
                //submit
                if (comboBox2.Text.Equals("Advisor"))
                {
                    validity = "Invalid";
                    string reg = v.AdvisorRegistration(id, name, userName, gender, DOB, maritialStatus, email, bloodGroup, photo, phone, address, joinDate, validity, confirmPIN, joinAs, secretQueAns, getBatchID);

                    DialogResult result = MessageBox.Show(reg + "\n  Your User ID : " + id + "\n  Thanks For Registration. \n Check Your Email For Further Validation", "Registration Complete", MessageBoxButtons.OK, MessageBoxIcon.None);
                    if (result == DialogResult.OK)
                    {                      
                        LoginPage lg = new LoginPage();
                        this.Hide();
                        lg.Show();
                        SendAdvisorEmailSMTP(email);
                    }
                }
                else if (comboBox2.Text.Equals("Examinee"))
                {
                    validity = "Valid";
                    string reg = v.InsertExamineeRegistration(id, name, userName, gender, DOB, maritialStatus, email, bloodGroup, photo, phone, address, joinDate, validity, confirmPIN, joinAs, secretQueAns, getBatchID,lastRegID);

                    DialogResult result = MessageBox.Show(reg + "\n Your User ID : " + id + " \n   Thanks For Registration. \n Check Your Email For Course Validation", "Registration Complete", MessageBoxButtons.OK, MessageBoxIcon.None);
                    if (result == DialogResult.OK)
                    {                      
                        LoginPage lg = new LoginPage();
                        this.Hide();
                        lg.Show();
                        SendExamineeEmailSMTP(email);
                    }
                }
            }

        }


        private void SendAdvisorEmailSMTP(string email)
        {
            this.email = email;
            double courseFee = v.GetCourseFee(getBatchID);
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("onlineedusystem@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Registration Complete";
                mail.Body = "Your ID :" + id + " ,\n Your PIN :" + confirmPIN + ".\n Send your Proper CV @onlineedusystem@gmail.com. \n We Will Confirm you via email soon.\n Thanks For Registration on Online Education system.    ";
                //mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("onlineedusystem@gmail.com", "10aiubcsfest2k17");
                    smtp.EnableSsl = true;
                    try
                    {
                        smtp.Send(mail);
                       // MessageBox.Show("Mail SuccessFully Send");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }


        private void SendExamineeEmailSMTP(string email)
        {
            this.email = email;
            double courseFee = v.GetCourseFee(getBatchID);
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("onlineedusystem@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Registration Complete";
                mail.Body = "Your ID :" + id + " ,\n Your PIN :" + confirmPIN + ".\n To valid Course pay Course Fee"+courseFee+" at our bKash (01700000000) with your ID as reference. \n Thanks For Registration on Online Education system.  ";
                //mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("onlineedusystem@gmail.com", "10aiubcsfest2k17");
                    smtp.EnableSsl = true;
                    try
                    {
                        smtp.Send(mail);
                       // MessageBox.Show("Mail SuccessFully Send");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void checkUniqueUserName()
        {
            if (v.checkUniqueUserName(textBox1.Text) >= 1)
            {
                MessageBox.Show("Already Have Account With This User Name! \nTry Another Username Please.");
                UniqueUserName = false;
            }
            else 
            {
                UniqueUserName = true;
            }
        }

        private void CheckName()
        {
            if (String.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Please Enter Name");
                checkName = false;
            }
            else
            {
                name = textBox3.Text;
                checkName = true;
            }

        }

        private void CheckUserName()
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please Enter UserName");
                checkUserName = false;
            }
            else
            {
                userName = textBox1.Text;
                checkUserName = true;
            }
        }

        private void GetNEWACCID()
        {          
           id= v.GetNewAccountID();
        }

        private void CheckEmail()
        {
            if (String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please Enter Email");
                checkEmail = false;
            }
            else
            {
                email = textBox2.Text;
                checkEmail = true;
            }
        }


        private void CheckAddress()
        {
            if (String.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Please Enter Address");
                checkAddress = false;
            }
            else
            {
                address = textBox7.Text;
                checkAddress = true;
            }
        }


        private void CheckSecretAns()
        {
            if (String.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Please Answer Secret Question");
                checkSecretAns = false;
            }
            else
            {
                secretQueAns = textBox4.Text;
                checkSecretAns = true;
            }
        }

        private void CheckPIN()
        {
            if (String.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Please Enter PIN");
                checkPIN = false;
            }
            else
            {
                checkPIN = true;
            }
        }
     

        private void CheckConfirmPIN()
        {
            if (String.IsNullOrWhiteSpace(textBox8.Text))
            {
                MessageBox.Show("Please Enter Confirm PIN");
                checkConfirmPIN = false;
            }
            else
            {
                checkConfirmPIN = true;
            }
        }

        private void CheckBothPIN()
        {
            if (textBox8.Text.Equals(textBox5.Text))
            {
                try
                {
                    confirmPIN = Convert.ToInt32(textBox8.Text);
                    checkBothPIN = true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("PIN Must Be Numeric\n"+ex.ToString());
                }
            }
            else if (!textBox8.Text.Equals(textBox6.Text))
            {
                MessageBox.Show("Confirm PIN Not Matched");
                checkBothPIN = false;
            }
        }

        private void CheckNumber()
        {
            if (String.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Please Enter Phone");
                checkNumber = false;
            }
            else
            {
                phone=textBox6.Text;
                checkNumber = true;
            }
        }

        private void CheckBloodGroup()
        {
            if (comboBox1.SelectedIndex > -1)
            {
                bloodGroup = comboBox1.SelectedItem.ToString();
                checkBloodGroup = true;
            }
            if (!checkBloodGroup)
            {
                MessageBox.Show("Please Select Blood Group");
            }
        }

        private void CheckCourseName()
        {
            if (comboBox4.SelectedIndex > -1)
            {
                courseName = comboBox4.SelectedItem.ToString();
                checkCourseName = true;
            }
            if (!checkCourseName)
            {
                MessageBox.Show("Please Select Course Name");
            }
        }

        private void CheckMaritialStatus()
        {
            if (comboBox3.SelectedIndex > -1)
            {
                maritialStatus = comboBox3.SelectedItem.ToString();
                checkMaritialStatus = true;
            }
            if (!checkMaritialStatus)
            {
                MessageBox.Show("Please Select Maritial Status");
            }
        }

        private void CheckJoinAs()
        {
            if (comboBox2.SelectedIndex > -1)
            {
                joinAs = comboBox2.SelectedItem.ToString();
                checkJoinAs = true;
            }
            if (!checkJoinAs)
            {
                MessageBox.Show("Please Select Join As");
            }
        }

        private void CheckPicPath()
        {
            if (String.IsNullOrWhiteSpace(picPath))
            {
                checkPicPath = false;
                MessageBox.Show("Please Select Photo");
            }
            else
            {
                photo = picPath;
                checkPicPath = true;
            }
        }

        private void CheckGender()
        {
            if (radioButton1.Checked)
            {
                gender = "Male";
                checkGender = true;
            }
            else if (radioButton2.Checked)
            {
                gender = "Female";
                checkGender = true;
            }
            if (!checkGender)
            {
                MessageBox.Show("Please Select Gender");
            }
        }

        private void comboBox4_MouseClick(object sender, MouseEventArgs e)
        {
            //mouse click
            //getadvisorid
            DataTable dt = v.GetOpenCourseName();

            comboBox4.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                object cellData = row["COURSENAME"];
                comboBox4.Items.Add(cellData.ToString());
            }
            
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            getBatchID = v.GetLastOpenCourseBatchID(comboBox4.Text).ToString();
        }

        

    }
}
