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
    public partial class ExamineeNote : Form
    {
        Examinee eee = new Examinee();
        string id;


        public ExamineeNote(string id)
        {
            InitializeComponent();
            this.id = id;
            label1.Text = "Logged in Examinee ID : " + id;
        }

 

        private void button3_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            LoginPage LG = new LoginPage();
            LG.Show();
            this.Hide();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //code if presss
        }

        private void ExamineeNote_Load(object sender, EventArgs e)
        {             
            
            List<string> list = new List<string>();

            list = eee.GetExamineeCourseName(int.Parse(id));
            
            foreach(string a in list)
            {
                listBox1.Items.Add(a);
            }


            
         
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
                       
            listBox2.Items.Clear();
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Select Course Name Properly.");
            }
            else
            {

                string selectitem = listBox1.Items[listBox1.SelectedIndex].ToString();
                //MessageBox.Show(selectitem);

                List<string> list = new List<string>();
                list = eee.GetExamineeTopicName(selectitem);
                foreach (string a in list)
                {
                    listBox2.Items.Add(a);
                }
            }

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex < 0)
            {
                MessageBox.Show("Select Topic Name Properly.");
            }
            else
            {
                string selectitem = listBox2.Items[listBox2.SelectedIndex].ToString();
                string Textt = eee.GetExamineeNotes(selectitem);
                pictureBox3.ImageLocation = Textt;
                string reference =eee.GetExamineeReference(selectitem);
                textBox1.Text = reference;
                //MessageBox.Show(selectitem);
            }
        }

        private void ExamineeNote_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ExamineeHome eh = new ExamineeHome(id);
            this.Hide();
            eh.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ExamineeNotice En = new ExamineeNotice(id);
            this.Hide();
            En.Show();
        }
    }
}
