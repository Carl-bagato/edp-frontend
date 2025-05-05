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
    public partial class addGipBeneficiary : Form
    {
        public addGipBeneficiary()
        {
            InitializeComponent();

            // Lock form to center of screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Optional: prevent resizing or moving
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            gipDashboard dilpForm = new gipDashboard();
            dilpForm.FormClosed += (s, args) => this.Show();
            dilpForm.Show();
            this.Hide();
        }

        private void submitbtn_Click(object sender, EventArgs e)
        {
            gipDashboard dilpForm = new gipDashboard();
            dilpForm.FormClosed += (s, args) => this.Show();
            dilpForm.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void submitbtn_Click_1(object sender, EventArgs e)
        {
            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                con.Open();

                MySqlCommand cmd;

                cmd = new MySqlCommand(@"INSERT INTO gip_table (lname, mname, fname, sex, bday,
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
                MessageBox.Show("Successfully added GIP Beneficiary!");

                gipBeneficiary spesDashboard = new gipBeneficiary();
                spesDashboard.ShowDialog();
                this.Close();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
