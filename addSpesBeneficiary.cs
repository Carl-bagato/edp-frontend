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
    public partial class addSpesBeneficiary : Form
    {

        string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

        public addSpesBeneficiary()
        {
            InitializeComponent();

            // Lock form to center of screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Optional: prevent resizing or moving
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
   
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            spesDashboard spesDashboard = new spesDashboard();
            spesDashboard.ShowDialog();
            this.Close();
        }

        private void submitbtn_Click(object sender, EventArgs e)
        {

            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                con.Open();

                MySqlCommand cmd;

                // 1. Insert Address
                //cmd = new MySqlCommand(@"INSERT INTO address_table (street, brgy, city, province) 
                //                 VALUES (@zone, @brgy, @city, @prov); SELECT LAST_INSERT_ID();", con);
                //cmd.Parameters.AddWithValue("@zone", textBox9.Text);
                //cmd.Parameters.AddWithValue("@brgy", textBox10.Text);
                //cmd.Parameters.AddWithValue("@city", textBox11.Text);
                //cmd.Parameters.AddWithValue("@prov", textBox12.Text);
                //int address_id = Convert.ToInt32(cmd.ExecuteScalar());
                //if (address_id <= 0)
                //{
                //    MessageBox.Show("Failed to insert address.");
                //    return;
                //}

                // 2. Insert School Info
                //cmd = new MySqlCommand(@"INSERT INTO school_table (school_name, program/strand, year_level, school_year) 
                //                 VALUES (@school, @strand, @yearLvl, @sy); SELECT LAST_INSERT_ID();", con);
                //cmd.Parameters.AddWithValue("@school", textBox14.Text);
                //cmd.Parameters.AddWithValue("@strand", textBox21.Text);
                //cmd.Parameters.AddWithValue("@yearLvl", textBox25.Text);
                //cmd.Parameters.AddWithValue("@sy", textBox29.Text);
                //int school_id = Convert.ToInt32(cmd.ExecuteScalar());

                //// 3. Insert OSY info (Optional)
                //int osy_id = 0;
                //if (!string.IsNullOrWhiteSpace(textBox35.Text))
                //{
                //    cmd = new MySqlCommand(@"INSERT INTO osy_table (last_sy_attended, last_yl_attended, last_sn_attended)
                //                     VALUES (@sy, @yl, @sn); SELECT LAST_INSERT_ID();", con);
                //    cmd.Parameters.AddWithValue("@sy", textBox35.Text);
                //    cmd.Parameters.AddWithValue("@yl", textBox34.Text);
                //    cmd.Parameters.AddWithValue("@sn", textBox32.Text);
                //    osy_id = Convert.ToInt32(cmd.ExecuteScalar());
                //}

                // 4. Insert to spes_table
                cmd = new MySqlCommand(@"INSERT INTO spes_table (lname, mname, fname, sex, bday,
                                     contact_num, out_of_school, 4ps_mem, number_of_years, type, email, socmed_account)
                                 VALUES (@lname, @mname, @fname, @sex, @bday, 
                                         @contact, @outOfSchool, @is4ps, @years, @type, @email, @socmed)", con);

                cmd.Parameters.AddWithValue("@lname", textBox1.Text);
                cmd.Parameters.AddWithValue("@mname", textBox3.Text);
                cmd.Parameters.AddWithValue("@fname", textBox2.Text);
                cmd.Parameters.AddWithValue("@sex", listBox1.Text);
                cmd.Parameters.AddWithValue("@bday", dateTimePicker1.Value.Date);
                //cmd.Parameters.AddWithValue("@addrID", address_id);
                //cmd.Parameters.AddWithValue("@schoolID", school_id);
                cmd.Parameters.AddWithValue("@contact", phone.Text);
                cmd.Parameters.AddWithValue("@outOfSchool", "yes");
                //cmd.Parameters.AddWithValue("@osyID", osy_id != 0 ? osy_id : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@is4ps", "no"); // Update if you collect this field
                cmd.Parameters.AddWithValue("@years", int.TryParse(textBox4.Text, out int n) ? n : 0);
                cmd.Parameters.AddWithValue("@type", "old"); // You may also make this a drop-down
                cmd.Parameters.AddWithValue("@email", textBox7.Text);
                cmd.Parameters.AddWithValue("@socmed", textBox8.Text);

                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Successfully added SPES Beneficiary!");

                spesBeneficiary spesDashboard = new spesBeneficiary();
                spesDashboard.ShowDialog();
                this.Close();
            }

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void addSpesBeneficiary_Load(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void textBox33_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
