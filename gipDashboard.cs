using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class gipDashboard : Form
    {
        public gipDashboard()
        {
            InitializeComponent();
        }

        private void gipDashboard_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            gipDashboard spesForm = new gipDashboard();
            spesForm.FormClosed += (s, args) => this.Show();
            spesForm.Show();
            this.Hide();
        }
    }
}
