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
    public partial class AdminEditProfile : Form
    {
        Admin a = new Admin();

         string id, adminPicPath, secretQueAns, gender, name, DOB, maritialStatus, email, bloodGroup, phone, address;
         bool checkGender, checkSecretAns, checkNumber, checkMaritialStatus,  checkEmail, checkAddress , checkName, checkBloodGroup ;

        public AdminEditProfile(string id)
        {
            InitializeComponent();
            this.id = id;
            label10.Text = label10.Text+id;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ChangePIN cp = new ChangePIN(id, "Admin");
            this.Hide();
            cp.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            LoginPage LG = new LoginPage();
            LG.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AdminAccessAccounts aaa = new AdminAccessAccounts(id);
            this.Hide();
            aaa.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdminHome aep = new AdminHome(id);
            this.Hide();
            aep.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CheckName();
            CheckMaritialStatus();            
            CheckBloodGroup();
            CheckGender();
            CheckNumber();
            CheckEmail();
            CheckAddress();
            CheckSecretAns();

            DOB = dateTimePicker1.Text;

            if (checkGender == true && checkMaritialStatus == true && checkSecretAns == true && checkAddress==true && checkEmail == true && checkBloodGroup == true && checkName == true && checkNumber == true)
            {
                //submitchange
                string result = a.UpdateAdminAccount(Convert.ToInt32(id), name, gender, DOB, maritialStatus, email, bloodGroup, adminPicPath, phone, address, secretQueAns);
               // string quesUpdateResult = a.UpdateSecretQuesAns(id, secretQueAns); 
                MessageBox.Show("Profile "+result + "\nSecret Question");
            }

            AdminEditProfile_Load(sender,e);
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)| *.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                adminPicPath = dlg.FileName.ToString();
                pictureBox1.ImageLocation = adminPicPath;
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

        private void CheckNumber()
        {
            if (String.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Please Enter Phone");
                checkNumber = false;
            }
            else
            {
                phone = textBox6.Text;
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

        private void AdminEditProfile_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void AdminEditProfile_Load(object sender, EventArgs e)
        {
            //getprofile
           List<string> list = new List<string>();
           list=a.GetAdminProfile(Convert.ToInt32(id));
           secretQueAns = a.GetSecretQuesAns(id);

           foreach (string item in list)
           {
                //list[0]; //id
                textBox3.Text = list[1]; //name
                textBox1.Text = list[2]; //username

                if (list[3].Equals("Female"))
                    {
                        radioButton2.Select();
                    }
                else if (list[3].Equals("Male"))
                    {
                        radioButton1.Select();
                    }

                dateTimePicker1.Text = list[4]; //DOB
                comboBox3.Text = list[5]; //maritial status
                textBox2.Text = list[6]; //email
                comboBox1.Text = list[7]; //BloodGroup
                adminPicPath = list[8];
                textBox6.Text = list[9];
                pictureBox1.ImageLocation = adminPicPath; //photo
                textBox7.Text = list[10]; //address
                textBox4.Text = secretQueAns;
           }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminHome adh = new AdminHome(id);
            this.Hide();
            adh.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            AdminNotice an = new AdminNotice(id);
            this.Hide();
            an.Show();
        }
    }
}
