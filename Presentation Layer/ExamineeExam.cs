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
    public partial class ExamineeExam : Form
    {
        Examinee eee = new Examinee();
        string id;
        List<string> list=new List<string>();
        static string value = "";
        int quizTimeSec = 59;
        int quizTimeMin = 24;
        DateTime thisDay = DateTime.Today;
           
           

        public ExamineeExam(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void button2_Click(object sender, EventArgs e)

        {
           

            panel1.Visible = false;
            panel2.Visible = true;
        }

        private void ExamineeNote_Load(object sender, EventArgs e)
        {
           
            int count=0;
            panel2.Visible =false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            label12.Text = eee.GetExamineeExamCourseBatch(id);

            DataTable dt=eee.GetQuestion(int.Parse(id));
            

            foreach (DataRow row in dt.Rows)
            {
                if (count == 0)
                {
                    groupBox1.Text = "1."+row["QUE"].ToString();
                    radioButton1.Text = row["OPTIONA"].ToString();
                    radioButton2.Text = row["OPTIONB"].ToString();
                    radioButton3.Text = row["OPTIONC"].ToString();
                    radioButton4.Text = row["OPTIOND"].ToString();
                    count++;
                }
                else if (count == 1)
                {
                    groupBox2.Text = "2." + row["QUE"].ToString();
                    radioButton5.Text = row["OPTIONA"].ToString();
                    radioButton6.Text = row["OPTIONB"].ToString();
                    radioButton7.Text = row["OPTIONC"].ToString();
                    radioButton8.Text = row["OPTIOND"].ToString();

                    count++;
                }

                else if (count == 2)
                {
                    groupBox3.Text = "3."  + row["QUE"].ToString();
                    radioButton9.Text  = row["OPTIONA"].ToString();
                    radioButton10.Text = row["OPTIONB"].ToString();
                    radioButton11.Text = row["OPTIONC"].ToString();
                    radioButton12.Text = row["OPTIOND"].ToString();

                    count++;
                }
                else if (count == 3)
                {
                    groupBox4.Text = "4." + row["QUE"].ToString();
                    radioButton13.Text = row["OPTIONA"].ToString();
                    radioButton14.Text = row["OPTIONB"].ToString();
                    radioButton15.Text = row["OPTIONC"].ToString();
                    radioButton16.Text = row["OPTIOND"].ToString();

                    count++;
                }
                else if (count == 4)
                {
                    groupBox5.Text = "5."+row["QUE"].ToString();
                    radioButton17.Text = row["OPTIONA"].ToString();
                    radioButton18.Text = row["OPTIONB"].ToString();
                    radioButton19.Text = row["OPTIONC"].ToString();
                    radioButton20.Text = row["OPTIOND"].ToString();

                    count++;
                }
                else if (count == 5)
                {
                    groupBox6.Text = "6."+row["QUE"].ToString();
                    radioButton21.Text = row["OPTIONA"].ToString();
                    radioButton22.Text = row["OPTIONB"].ToString();
                    radioButton23.Text = row["OPTIONC"].ToString();
                    radioButton24.Text = row["OPTIOND"].ToString();

                    count++;
                }
                else if (count == 6)
                {
                    groupBox7.Text ="7."+ row["QUE"].ToString();
                    radioButton25.Text = row["OPTIONA"].ToString();
                    radioButton26.Text = row["OPTIONB"].ToString();
                    radioButton27.Text = row["OPTIONC"].ToString();
                    radioButton28.Text = row["OPTIOND"].ToString();

                    count++;
                }
                else if (count == 7)
                {
                    groupBox8.Text = "8."+row["QUE"].ToString();
                    radioButton29.Text = row["OPTIONA"].ToString();
                    radioButton30.Text = row["OPTIONB"].ToString();
                    radioButton31.Text = row["OPTIONC"].ToString();
                    radioButton32.Text = row["OPTIOND"].ToString();

                    count++;
                }
                else if (count == 8)
                {
                    groupBox9.Text = "9."+row["QUE"].ToString();
                    radioButton33.Text = row["OPTIONA"].ToString();
                    radioButton34.Text = row["OPTIONB"].ToString();
                    radioButton35.Text = row["OPTIONC"].ToString();
                    radioButton36.Text = row["OPTIOND"].ToString();

                    count++;
                }
                else if (count == 9)
                {
                    groupBox10.Text = "10."+row["QUE"].ToString();
                    radioButton37.Text = row["OPTIONA"].ToString();
                    radioButton38.Text = row["OPTIONB"].ToString();
                    radioButton39.Text = row["OPTIONC"].ToString();
                    radioButton40.Text = row["OPTIOND"].ToString();

                    count++;
                }
                else if (count == 10)
                {
                    groupBox20.Text ="20."+ row["QUE"].ToString();
                    radioButton41.Text = row["OPTIONA"].ToString();
                    radioButton42.Text = row["OPTIONB"].ToString();
                    radioButton43.Text = row["OPTIONC"].ToString();
                    radioButton44.Text = row["OPTIOND"].ToString();

                    count++;
                }
                else if (count == 11)
                {
                    groupBox15.Text ="15."+ row["QUE"].ToString();
                    radioButton45.Text = row["OPTIONA"].ToString();
                    radioButton46.Text = row["OPTIONB"].ToString();
                    radioButton47.Text = row["OPTIONC"].ToString();
                    radioButton48.Text = row["OPTIOND"].ToString();

                    count++;
                }
                else if (count == 12)
                {
                    groupBox19.Text ="19."+ row["QUE"].ToString();
                    radioButton49.Text = row["OPTIONA"].ToString();
                    radioButton50.Text = row["OPTIONB"].ToString();
                    radioButton51.Text = row["OPTIONC"].ToString();
                    radioButton52.Text = row["OPTIOND"].ToString();

                    count++;
                }
                else if (count == 13)
                {
                    groupBox14.Text ="14."+ row["QUE"].ToString();
                    radioButton53.Text = row["OPTIONA"].ToString();
                    radioButton54.Text = row["OPTIONB"].ToString();
                    radioButton55.Text = row["OPTIONC"].ToString();
                    radioButton56.Text = row["OPTIOND"].ToString();

                    count++;
                }
                else if (count == 14)
                {
                    groupBox13.Text ="13."+row["QUE"].ToString();
                    radioButton57.Text = row["OPTIONA"].ToString();
                    radioButton58.Text = row["OPTIONB"].ToString();
                    radioButton59.Text = row["OPTIONC"].ToString();
                    radioButton60.Text = row["OPTIOND"].ToString();

                    count++;
                }
                else if (count == 15)
                {
                    groupBox18.Text = "18."+row["QUE"].ToString();
                    radioButton61.Text = row["OPTIONA"].ToString();
                    radioButton62.Text = row["OPTIONB"].ToString();
                    radioButton63.Text = row["OPTIONC"].ToString();
                    radioButton64.Text = row["OPTIOND"].ToString();

                    count++;
                }
                else if (count == 16)
                {
                    groupBox17.Text = "17."+row["QUE"].ToString();
                    radioButton65.Text = row["OPTIONA"].ToString();
                    radioButton66.Text = row["OPTIONB"].ToString();
                    radioButton67.Text = row["OPTIONC"].ToString();
                    radioButton68.Text = row["OPTIOND"].ToString();

                    count++;
                }
                else if (count == 17)
                {
                    groupBox12.Text = "12."+row["QUE"].ToString();
                    radioButton69.Text = row["OPTIONA"].ToString();
                    radioButton70.Text = row["OPTIONB"].ToString();
                    radioButton71.Text = row["OPTIONC"].ToString();
                    radioButton72.Text = row["OPTIOND"].ToString();

                    count++;
                }
                else if (count == 18)
                {
                    groupBox16.Text = "16."+row["QUE"].ToString();
                    radioButton73.Text = row["OPTIONA"].ToString();
                    radioButton74.Text = row["OPTIONB"].ToString();
                    radioButton75.Text = row["OPTIONC"].ToString();
                    radioButton76.Text = row["OPTIOND"].ToString();

                    count++;
                }
                else if (count == 19)
                {
                    groupBox11.Text = "11."+row["QUE"].ToString();
                    radioButton77.Text = row["OPTIONA"].ToString();
                    radioButton78.Text = row["OPTIONB"].ToString();
                    radioButton79.Text = row["OPTIONC"].ToString();
                    radioButton80.Text = row["OPTIOND"].ToString();

                    count++;
                }
                
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;                  
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            string bid="";
            int count = 0;
            
          DataTable dt=eee.GetQuestion(int.Parse(id));


            foreach (DataRow row in dt.Rows)
            {
                foreach(var item in list )
                if (item == row["CORRECTOPTION"].ToString())
                {
                    count++;
                }
               
                object cellData = row["BATCHID"];
                bid=cellData.ToString();
            }


            DialogResult result = MessageBox.Show("Exam Completed", "Time Up", MessageBoxButtons.OK);
            if (result == DialogResult.OK)
            {
               
                int LastQid = eee.GetLastRecID();
                string grade="";
                if (count>15)
                {
                    grade = "A+";
                }
                else 
                {
                    grade = "A-";
                }

                MessageBox.Show(eee.InsertExamineeResult(LastQid, int.Parse(id), grade, int.Parse(bid), thisDay.ToString("d"), Convert.ToDouble(count)));

                ExamineeHome eh = new ExamineeHome(id);
                this.Hide();
                eh.Show();
            }
        }

        private void checkBox1_Enter(object sender, EventArgs e)
        {

           
            RadioButton radio = (RadioButton)sender;

            if (radio.Checked)
            {
                 value=radio.Text;
                 list.Add(value);
            }
            else
            {
                list.Remove(value);
            }
          
           
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label10.Text = quizTimeMin.ToString();
            label9.Text = quizTimeSec.ToString();

            if (quizTimeMin==0 && quizTimeSec==0)
            {
                timer1.Stop();
                DialogResult result = MessageBox.Show("Your time is up", "Time Up", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                if (result == DialogResult.OK)
                {
                    button3.PerformClick();
                }

            }

            
            quizTimeSec--;
            if (quizTimeSec < 0 && quizTimeMin>0)
            {            
                quizTimeMin--;
                quizTimeSec = 59;
            }
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            timer1.Start();
            radioButton37.Checked = false;
            panel3.Visible = false;
            panel2.Visible = false;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
           

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ExamineeHome eh = new ExamineeHome(id);
            this.Hide();
            eh.Show();
        }

      

      


       
    }
}
