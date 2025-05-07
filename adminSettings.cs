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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class adminSettings : Form
    {
        public adminSettings()
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

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void addspesbtn_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            addAdminAccount addadmin = new addAdminAccount();
            addadmin.FormClosed += (s, args) => this.Show();
            addadmin.Show();
            this.Hide();
            LoadSpesTable();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter an ID to search.");
                return;
            }

            DialogResult result = MessageBox.Show("Do you want to update this profile?",
                                                  "Confirm Update",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                textBox1.Clear();
                return;
            }

            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM admin_table WHERE admin_id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No record found with the specified ID.");
                        return;
                    }

                    adminUpdateAccount updateForm = new adminUpdateAccount(dt.Rows[0]);
                    updateForm.FormClosed += (s, args) => {
                        this.Show();
                        LoadSpesTable(); // Refresh data
                    };
                    updateForm.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return;
                }
            }
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

        private void button4_Click_1(object sender, EventArgs e)
        {
            LoadSpesTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
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

                    MySqlCommand cmd = new MySqlCommand("DELETE FROM admin_table WHERE admin_id = @id", con);
                    cmd.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));

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
    }
}
