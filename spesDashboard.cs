using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class spesDashboard : Form
    {
        public spesDashboard()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void addspesbtn_Click(object sender, EventArgs e)
        {
            addSpesBeneficiary addspes = new addSpesBeneficiary();
            addspes.FormClosed += (s, Args) => this.Show();
            addspes.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            updateSpesBeneficiary form1 = new updateSpesBeneficiary();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            deleteSpesBeneficiary form1 = new deleteSpesBeneficiary();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            spesBeneficiary spesForm = new spesBeneficiary();
            spesForm.FormClosed += (s, args) => this.Show();
            spesForm.Show();
            this.Hide();
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {

            loginForm login = Application.OpenForms.OfType<loginForm>().FirstOrDefault();

            // If it's not open, create a new one *before* closing other forms
            if (login == null)
            {
                login = new loginForm();
            }

            // Close all other forms except the login form
            foreach (Form frm in Application.OpenForms.Cast<Form>().ToList())
            {
                if (frm != login)
                {
                    frm.Close();
                }
            }

            // Show the login form
            login.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            spesDashboard spesForm = new spesDashboard();
            spesForm.FormClosed += (s, args) => this.Show();
            spesForm.Show();
            this.Hide();
        }
    }
}
