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
    public partial class updateSpesBeneficiary : Form
    {
        public updateSpesBeneficiary()
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
                MySqlDataAdapter sqldata = new MySqlDataAdapter("Select * From spes_table", con);
                DataTable dtb1 = new DataTable();
                sqldata.Fill(dtb1);

                dataGridView1.DataSource = dtb1;

            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            deleteSpesBeneficiary form1 = new deleteSpesBeneficiary();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }

        private void addspesbtn_Click(object sender, EventArgs e)
        {
            addSpesBeneficiary addspes = new addSpesBeneficiary();
            addspes.FormClosed += (s, Args) => this.Show();
            addspes.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            spesBeneficiary spesForm = new spesBeneficiary();
            spesForm.FormClosed += (s, args) => this.Show();
            spesForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            updateSpesBeneficiary spesForm = new updateSpesBeneficiary();
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

        private void button6_Click(object sender, EventArgs e)
        {
            spesDashboard spesForm = new spesDashboard();
            spesForm.FormClosed += (s, args) => this.Show();
            spesForm.Show();
            this.Hide();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text))
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
                textBox4.Clear();
                return;
            }

            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM spes_table WHERE spes_id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", int.Parse(textBox4.Text));

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No record found with the specified ID.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return;
                }
            }

            // Pass DataTable row to the modal form
            updateModal modal = new updateModal(dt.Rows[0]);
            modal.FormClosed += (s, Args) => this.Show();
            modal.Show();
            this.Hide();
        }




        private void searchbtn_Click(object sender, EventArgs e)
        {
            
        }

    }
}
