using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class adminAccounts : Form
    {
        public adminAccounts()
        {
            InitializeComponent();

            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                con.Open();
                MySqlDataAdapter sqldata = new MySqlDataAdapter("Select * From admin_table", con);
                DataTable dtb1 = new DataTable();
                sqldata.Fill(dtb1);

                dataGridView1.DataSource = dtb1;

            }

            // Lock form to center of screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Optional: prevent resizing or moving
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            adminAccounts form1 = new adminAccounts();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            adminActivityLogs form1 = new adminActivityLogs();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            addAdminAccount form1 = new addAdminAccount();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            adminSettings form1 = new adminSettings();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }
    }
}
