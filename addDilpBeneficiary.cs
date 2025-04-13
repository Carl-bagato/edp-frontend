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
    public partial class addDilpBeneficiary : Form
    {
        public addDilpBeneficiary()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dilpDashboard dilpForm = new dilpDashboard();
            dilpForm.FormClosed += (s, args) => this.Show();
            dilpForm.Show();
            this.Hide();
        }

        private void submitbtn_Click(object sender, EventArgs e)
        {
            dilpDashboard dilpForm = new dilpDashboard();
            dilpForm.FormClosed += (s, args) => this.Show();
            dilpForm.Show();
            this.Hide();
        }
    }
}
