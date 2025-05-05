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
    public partial class adminUpdateAccount : Form
    {
        public adminUpdateAccount()
        {
            InitializeComponent();

            // Lock form to center of screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Optional: prevent resizing or moving
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void adminUpdateAccount_Load(object sender, EventArgs e)
        {

        }

        private DataRow userData;

        public adminUpdateAccount(DataRow data)
        {
            InitializeComponent();
            userData = data;
            FillFields();
        }

        private void FillFields()
        {
            
            textBox1.Text = userData["admin_name"].ToString();             
            textBox3.Text = userData["admin_position"].ToString();             
            textBox2.Text = userData["admin_username"].ToString();
            phone.Text = userData["admin_password"].ToString();         
        }

        private void submitbtn_Click(object sender, EventArgs e)
        {
            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                try
                {
                    con.Open();

                    string query = @"UPDATE admin_table 
                             SET admin_username = @user, 
                                 admin_password = @pass, 
                                 admin_position = @pos, 
                                 admin_name = @name, 
                             WHERE admin_id = @id";

                    MySqlCommand cmd = new MySqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@user", textBox3.Text);
                    cmd.Parameters.AddWithValue("@pass", phone.Text);
                    cmd.Parameters.AddWithValue("@pos", textBox2.Text);
                    cmd.Parameters.AddWithValue("@name", textBox1.Text);

                    cmd.Parameters.AddWithValue("@id", userData["admin_id"]);

                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Record updated successfully!");
                    this.Close(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
