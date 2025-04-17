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
                MySqlCommand cmd = new MySqlCommand("insert into spes_table values (@lname,@mname,@fname,@sex,@bday,@address_id,@InSchool_id,@contact_num,@out_of_school,@osy_id,@4ps_mem,@number_of_years,@type,@email,@socmed_account)", con);
                cmd.Parameters.AddWithValue("@lname",textBox1.Text);
                cmd.Parameters.AddWithValue("@mname", textBox3.Text);
                cmd.Parameters.AddWithValue("@fname", textBox2.Text);
                cmd.Parameters.AddWithValue("@sex", listBox1.Text);
                cmd.Parameters.AddWithValue("@bday", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@address_id", "1");
                cmd.Parameters.AddWithValue("@InSchool_id", "1");
                cmd.Parameters.AddWithValue("@contact_num", double.Parse(phone.Text));
                cmd.Parameters.AddWithValue("@out_of_school", "no");
                cmd.Parameters.AddWithValue("@osy_id", textBox1.Text);
                cmd.Parameters.AddWithValue("@4ps_mem", textBox1.Text);
                cmd.Parameters.AddWithValue("@number_of_years", int.Parse(textBox4.Text));
                cmd.Parameters.AddWithValue("@type", "old");
                cmd.Parameters.AddWithValue("@email", textBox1.Text);
                cmd.Parameters.AddWithValue("@socmed_account", textBox8.Text);
                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("sakses!");


                //spesDashboard spesDashboard = new spesDashboard();
                //spesDashboard.ShowDialog();
                //this.Close();
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
