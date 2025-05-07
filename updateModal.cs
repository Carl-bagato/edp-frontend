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
    public partial class updateModal : Form
    {
        public updateModal()
        {
            InitializeComponent();

            // Lock form to center of screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Optional: prevent resizing or moving
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

        }

        private void updateModal_Load(object sender, EventArgs e)
        {

        }

        private DataRow userData;

        public updateModal(DataRow data)
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
            textBox4.Text = userData["number_of_years"].ToString();   // Years as Beneficiary
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

                    string query = @"UPDATE spes_table 
                             SET lname = @lname, 
                                 mname = @mname, 
                                 fname = @fname, 
                                 sex = @sex, 
                                 bday = @bday, 
                                 contact_num = @contact, 
                                 out_of_school = @outOfSchool, 
                                 4ps_mem = @is4ps, 
                                 number_of_years = @years, 
                                 type = @type, 
                                 email = @email, 
                                 socmed_account = @socmed
                             WHERE spes_id = @id";

                    MySqlCommand cmd = new MySqlCommand(query, con);

                    // Match this exactly with your insert structure and textbox variables
                    cmd.Parameters.AddWithValue("@lname", textBox1.Text);
                    cmd.Parameters.AddWithValue("@mname", textBox3.Text);
                    cmd.Parameters.AddWithValue("@fname", textBox2.Text);
                    cmd.Parameters.AddWithValue("@sex", listBox1.Text);
                    cmd.Parameters.AddWithValue("@bday", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@contact", phone.Text);
                    cmd.Parameters.AddWithValue("@outOfSchool", "yes"); // or a control value if dynamic
                    cmd.Parameters.AddWithValue("@is4ps", "no");         // or control value
                    cmd.Parameters.AddWithValue("@years", int.TryParse(textBox4.Text, out int n) ? n : 0);
                    cmd.Parameters.AddWithValue("@type", "old");         // or drop-down selected item
                    cmd.Parameters.AddWithValue("@email", textBox7.Text);
                    cmd.Parameters.AddWithValue("@socmed", textBox8.Text);

                    cmd.Parameters.AddWithValue("@id", userData["spes_id"]);

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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
