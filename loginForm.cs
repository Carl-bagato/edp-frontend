using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();

            password.UseSystemPasswordChar = true;

            // Lock form to center of screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Optional: prevent resizing or moving
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (username.Text == "" || password.Text == "")
            {
                MessageBox.Show("Please fill all blank fields!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                bool loginSuccess = getData(username.Text.Trim(), password.Text.Trim());

                if (loginSuccess)
                {
                    Form1 spesForm = new Form1();
                    spesForm.FormClosed += (s, args) => this.Show();
                    spesForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Account not found.a Please check your username and password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public bool getData(string inputUser, string inputPass)
        {
            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            try
            {
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();

                    string query = @"
                SELECT * FROM admin_table 
                WHERE BINARY TRIM(admin_username) = BINARY @username 
                AND BINARY TRIM(admin_password) = BINARY @password";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@username", inputUser.Trim());
                    cmd.Parameters.AddWithValue("@password", inputPass.Trim());

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            password.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void password_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
