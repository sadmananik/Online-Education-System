using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation_Layer
{
    public partial class TermsAndConditions : Form
    {
        public TermsAndConditions()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoginPage lg = new LoginPage();
            this.Hide();
            lg.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                FormRegistration fr = new FormRegistration();
                this.Hide();
                fr.Show();
            }
            else if (!checkBox1.Checked)
            {
                DialogResult result = MessageBox.Show("Agree our terms and conditions first.", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel)
                {
                    LoginPage lp = new LoginPage();
                    lp.Show();
                    this.Hide();
                }
            }
        }

        private void TermsAndConditions_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
