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
    public partial class addDilpBeneficiary : Form
    {
        public addDilpBeneficiary()
        {
            InitializeComponent();

            // Lock form to center of screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Optional: prevent resizing or moving
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void addDilpBeneficiary_Load(object sender, EventArgs e)
        {

        }

        private void submitbtn_Click_1(object sender, EventArgs e)
        {
            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                con.Open();

                MySqlCommand cmd;

                cmd = new MySqlCommand(@"INSERT INTO dilp_table (lname, mname, fname, sex, bday,
                                     contact_num, email, socmed_account)
                                 VALUES (@lname, @mname, @fname, @sex, @bday, 
                                         @contact, @email, @socmed)", con);

                cmd.Parameters.AddWithValue("@lname", textBox1.Text);
                cmd.Parameters.AddWithValue("@mname", textBox3.Text);
                cmd.Parameters.AddWithValue("@fname", textBox2.Text);
                cmd.Parameters.AddWithValue("@sex", listBox1.Text);
                cmd.Parameters.AddWithValue("@bday", dateTimePicker1.Value.Date);
                //cmd.Parameters.AddWithValue("@addrID", address_id);
                //cmd.Parameters.AddWithValue("@schoolID", school_id);
                cmd.Parameters.AddWithValue("@contact", phone.Text);
                //cmd.Parameters.AddWithValue("@osyID", osy_id != 0 ? osy_id : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@email", textBox7.Text);
                cmd.Parameters.AddWithValue("@socmed", textBox8.Text);

                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Successfully added DILP Beneficiary!");

                dilpBeneficiary spesDashboard = new dilpBeneficiary();
                spesDashboard.ShowDialog();
                this.Close();
            }
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void phone_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
