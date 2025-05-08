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
    public partial class deleteGipBeneficiary : Form
    {
        public deleteGipBeneficiary()
        {
            InitializeComponent();

            // Lock form to center of screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Optional: prevent resizing or moving
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                con.Open();
                MySqlDataAdapter sqldata = new MySqlDataAdapter("Select * From gip_table", con);
                DataTable dtb1 = new DataTable();
                sqldata.Fill(dtb1);

                dataGridView1.DataSource = dtb1;

            }
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            // Close all open forms except the one calling this method (usually the dashboard)
            foreach (Form frm in Application.OpenForms.Cast<Form>().ToList())
            {
                if (frm != this)
                {
                    frm.Close();
                }
            }

            // Show the login form after closing others
            loginForm login = new loginForm();
            login.Show();

            // Then close the current form (logout)
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            gipDashboard dilpForm = new gipDashboard();
            dilpForm.FormClosed += (s, args) => this.Show();
            dilpForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gipBeneficiary dilpForm = new gipBeneficiary();
            dilpForm.FormClosed += (s, args) => this.Show();
            dilpForm.Show();
            this.Hide();
        }

        private void addspesbtn_Click(object sender, EventArgs e)
        {
            addGipBeneficiary dilpForm = new addGipBeneficiary();
            dilpForm.FormClosed += (s, args) => this.Show();
            dilpForm.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            updateGipBeneficiary dilpForm = new updateGipBeneficiary();
            dilpForm.FormClosed += (s, args) => this.Show();
            dilpForm.Show();
            this.Hide();
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            deleteGipBeneficiary dilpForm = new deleteGipBeneficiary();
            dilpForm.FormClosed += (s, args) => this.Show();
            dilpForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Please enter a valid ID.");
                return;
            }

            DialogResult confirmResult = MessageBox.Show(
                "Are you sure you want to delete this record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmResult == DialogResult.No)
                return;

            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                try
                {
                    con.Open();

                    MySqlCommand cmd = new MySqlCommand("DELETE FROM gip_table WHERE gip_id = @id", con);
                    cmd.Parameters.AddWithValue("@id", int.Parse(textBox4.Text));

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Successfully Deleted!");
                        LoadSpesTable();
                    }
                    else
                        MessageBox.Show("No record found with the specified ID.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void LoadSpesTable()
        {
            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                try
                {
                    con.Open();
                    MySqlDataAdapter sqldata = new MySqlDataAdapter("SELECT * FROM gip_table", con);
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

        private void deleteGipBeneficiary_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
