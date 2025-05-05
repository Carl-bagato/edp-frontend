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
    public partial class spesDashboard : Form
    {
        public spesDashboard()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.spesDashboard_Load);

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

        private void addspesbtn_Click(object sender, EventArgs e)
        {
            addSpesBeneficiary addspes = new addSpesBeneficiary();
            addspes.FormClosed += (s, Args) => this.Show();
            addspes.Show();
   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            updateSpesBeneficiary form1 = new updateSpesBeneficiary();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            deleteSpesBeneficiary form1 = new deleteSpesBeneficiary();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            spesBeneficiary spesForm = new spesBeneficiary();
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

        private void button4_Click(object sender, EventArgs e)
        {
            spesDashboard spesForm = new spesDashboard();
            spesForm.FormClosed += (s, args) => this.Show();
            spesForm.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {
           
        }

        private void chart2_Click_1(object sender, EventArgs e)
        {
          
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void spesDashboard_Load(object sender, EventArgs e)
        {
            LoadChartData();
            LoadTotalBeneficiaries(); 
            LoadTotalNewBeneficiaries(); 
            LoadTotalOldBeneficiaries(); 
        }
        private void LoadChartData()
        {
            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                try
                {
                    con.Open();

                    string query = @"
                SELECT sex, COUNT(*) AS total
                FROM spes_table
                GROUP BY sex;
            ";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    // Clear existing chart data
                    chart2.Series.Clear();
                    chart2.Series.Add("Gender");
                    chart2.Series["Gender"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

                    while (reader.Read())
                    {
                        string gender = reader["sex"].ToString();
                        int count = Convert.ToInt32(reader["total"]);

                        chart2.Series["Gender"].Points.AddXY(gender, count);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading chart data: " + ex.Message);
                }
            }
        }

        private void LoadTotalBeneficiaries()
        {
            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM spes_table";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    int total = Convert.ToInt32(cmd.ExecuteScalar());

                    label7.Text = total.ToString();
                }
                catch (Exception ex)
                {
                    label7.Text = "Error loading total.";
                    MessageBox.Show("Error loading total beneficiaries: " + ex.Message);
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void LoadTotalNewBeneficiaries()
        {
            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM spes_table WHERE type = 'new'";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    int total = Convert.ToInt32(cmd.ExecuteScalar());

                    label8.Text = total.ToString();
                }
                catch (Exception ex)
                {
                    label8.Text = "Error loading total.";
                    MessageBox.Show("Error loading total beneficiaries: " + ex.Message);
                }
            }
        }
        private void LoadTotalOldBeneficiaries()
        {
            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM spes_table WHERE type = 'old'";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    int total = Convert.ToInt32(cmd.ExecuteScalar());

                    label9.Text = total.ToString();
                }
                catch (Exception ex)
                {
                    label9.Text = "Error loading total.";
                    MessageBox.Show("Error loading total beneficiaries: " + ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }
    }
}
