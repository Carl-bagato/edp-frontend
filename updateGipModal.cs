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
    public partial class updateGipModal : Form
    {
        public updateGipModal()
        {
            InitializeComponent();
        }

        private DataRow userData;

        public updateGipModal(DataRow data)
        {
            InitializeComponent();
            userData = data;
            FillFields();
        }

        private void FillFields()
        {
            // Make sure these match the exact column names in your `spes_table`
            textBox1.Text = userData["lname"].ToString();             // Last Name
            textBox3.Text = userData["mname"].ToString();             // Middle Name
            textBox2.Text = userData["fname"].ToString();             // First Name
            listBox1.Text = userData["sex"].ToString();               // Gender/Sex
            dateTimePicker1.Value = Convert.ToDateTime(userData["bday"]); // Birthday
            phone.Text = userData["contact_num"].ToString();          // Contact Number
            textBox7.Text = userData["email"].ToString();             // Email Address
            textBox8.Text = userData["socmed_account"].ToString();    // Social Media Account

            // Optional: Add handling if some fields may be null
        }
        private void submitbtn_Click(object sender, EventArgs e)
        {
            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                try
                {
                    con.Open();

                    string query = @"UPDATE gip_table 
                             SET lname = @lname, 
                                 mname = @mname, 
                                 fname = @fname, 
                                 sex = @sex, 
                                 bday = @bday, 
                                 contact_num = @contact, 
                                 email = @email, 
                                 socmed_account = @socmed
                             WHERE gip_id = @id";

                    MySqlCommand cmd = new MySqlCommand(query, con);

                    // Match this exactly with your insert structure and textbox variables
                    cmd.Parameters.AddWithValue("@lname", textBox1.Text);
                    cmd.Parameters.AddWithValue("@mname", textBox3.Text);
                    cmd.Parameters.AddWithValue("@fname", textBox2.Text);
                    cmd.Parameters.AddWithValue("@sex", listBox1.Text);
                    cmd.Parameters.AddWithValue("@bday", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@contact", phone.Text);
                    cmd.Parameters.AddWithValue("@email", textBox7.Text);
                    cmd.Parameters.AddWithValue("@socmed", textBox8.Text);

                    cmd.Parameters.AddWithValue("@id", userData["gip_id"]);

                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Record updated successfully!");
                    this.Close(); // Close modal and return to previous form
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
