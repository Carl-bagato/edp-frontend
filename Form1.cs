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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void gipbtn_Click(object sender, EventArgs e)
        {
            gipDashboard gipForm = new gipDashboard();
            gipForm.FormClosed += (s, args) => this.Show(); 
            gipForm.Show();
            this.Hide();
        }

        private void spesbtn_Click(object sender, EventArgs e)
        {
            spesDashboard spesForm = new spesDashboard();
            spesForm.FormClosed += (s, args) => this.Show(); 
            spesForm.Show();
            this.Hide();
        }

        private void dilpbtn_Click(object sender, EventArgs e)
        {
            dilpDashboard dilpForm = new dilpDashboard();
            dilpForm.FormClosed += (s, args) => this.Show(); 
            dilpForm.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Logout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
