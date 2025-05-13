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
    public partial class adminActivityLogs : Form
    {
        public adminActivityLogs()
        {
            InitializeComponent();

            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                con.Open();
                MySqlDataAdapter sqldata = new MySqlDataAdapter("Select * From activity_logs_table", con);
                DataTable dtb1 = new DataTable();
                sqldata.Fill(dtb1);

                dataGridView1.DataSource = dtb1;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }

            // Lock form to center of screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Optional: prevent resizing or moving
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            adminActivityLogs form1 = new adminActivityLogs();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            LoadSpesTable();
        }

        private void LoadSpesTable()
        {
            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                try
                {
                    con.Open();
                    MySqlDataAdapter sqldata = new MySqlDataAdapter("SELECT * FROM admin_table", con);
                    DataTable dtb1 = new DataTable();
                    sqldata.Fill(dtb1);
                    dataGridView1.DataSource = dtb1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load data: " + ex.Message);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            adminSettings form1 = new adminSettings();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            addAdminAccount form1 = new addAdminAccount();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            adminAccounts form1 = new adminAccounts();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            // Create a new login form
            loginForm newLogin = new loginForm();

            // Hide this form first to avoid UI flickering
            this.Hide();

            // Show login form
            newLogin.Show();

            // Close this form (after login form is fully shown)
            this.BeginInvoke((Action)(() => this.Close()));
        }

        private void button10_Click(object sender, EventArgs e)
        {
            admin_program_bene form1 = new admin_program_bene();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            adminActivityLogs form1 = new adminActivityLogs();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }
    }
}
