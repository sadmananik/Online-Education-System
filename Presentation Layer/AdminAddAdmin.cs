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
    public partial class AdminAddAdmin : Form
    {
        Admin a = new Admin();
        Visitor v = new Visitor();

        string id, adminPicPath, secretQueAns, gender, name, DOB, maritialStatus, email, bloodGroup, phone, address, userName, photo, joinDate;
        bool checkGender, UniqueUserName, checkSecretAns, checkNumber, checkMaritialStatus, checkEmail, checkAddress, checkName, checkBloodGroup, checkUserName, checkPicPath;
        int newAdminId;


        private void GetNEWACCID()
        {
            newAdminId = v.GetNewAccountID();
        }

        public AdminAddAdmin(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void CheckPicPath()
        {
            if (String.IsNullOrWhiteSpace(adminPicPath))
            {
                checkPicPath = false;
                MessageBox.Show("Please Select Photo");
            }
            else
            {
                photo = adminPicPath;
                checkPicPath = true;
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


        private void checkUniqueUserName()
        {
            if (v.checkUniqueUserName(textBox1.Text) >= 1)
            {
                MessageBox.Show("Already Have " + v.checkUniqueUserName(textBox1.Text) + " Account With This User Name! \nTry Another Username Please.");
                UniqueUserName = false;
            }
            else
            {
                UniqueUserName = true;
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

        private void AdminAddAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
      


        private void button2_Click(object sender, EventArgs e)
        {
           //submit
            checkUniqueUserName();
            CheckName();
            CheckUserName();
            CheckMaritialStatus();
            CheckBloodGroup();
            CheckGender();
            CheckNumber();
            CheckEmail();
            CheckPicPath();
            CheckAddress();
            CheckSecretAns();
            GetNEWACCID();
            joinDate = DateTime.Today.ToString("dd-MM-yyyy");

            DOB = dateTimePicker1.Text;

            if (checkGender == true && checkMaritialStatus == true && UniqueUserName == true && checkSecretAns == true && checkAddress == true && checkEmail == true && checkBloodGroup == true && checkName == true && checkNumber == true && checkUserName == true && checkPicPath == true)
            {
                //submitchange
                string result = a.InsertAdminAccount(Convert.ToInt32(id), newAdminId, name, userName, gender, DOB, maritialStatus, email, bloodGroup, photo, phone, address, joinDate, "Valid", 1234, "Admin", secretQueAns);
                 MessageBox.Show("Profile " + result +"\n ACCID: "+newAdminId+" Defualt PIN: 1234");
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AdminHome ah = new AdminHome(id);
            this.Hide();
            ah.Show();
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

        private void AdminAddAdmin_Load(object sender, EventArgs e)
        {

        }




    }
}
