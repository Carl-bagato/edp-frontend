using System;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class gipDashboard : Form
    {
        public gipDashboard()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.dilpDashboard_Load);

            // Lock form to center of screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Optional: prevent resizing or moving
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void gipDashboard_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            gipDashboard spesForm = new gipDashboard();
            spesForm.FormClosed += (s, args) => this.Show();
            spesForm.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            gipBeneficiary spesForm = new gipBeneficiary();
            spesForm.FormClosed += (s, args) => this.Show();
            spesForm.Show();
            this.Hide();
        }

        private void addspesbtn_Click(object sender, EventArgs e)
        {
            addGipBeneficiary spesForm = new addGipBeneficiary();
            spesForm.FormClosed += (s, args) => this.Show();
            spesForm.Show();
 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            updateGipBeneficiary spesForm = new updateGipBeneficiary();
            spesForm.FormClosed += (s, args) => this.Show();
            spesForm.Show();
            this.Hide();
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            deleteGipBeneficiary spesForm = new deleteGipBeneficiary();
            spesForm.FormClosed += (s, args) => this.Show();
            spesForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 spesForm = new Form1();
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

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void dilpDashboard_Load(object sender, EventArgs e)
        {
            LoadChartData();
            LoadTotalBeneficiaries();

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
                FROM gip_table
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
                    string query = "SELECT COUNT(*) FROM gip_table";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    int total = Convert.ToInt32(cmd.ExecuteScalar());

                    label4.Text = total.ToString();
                }
                catch (Exception ex)
                {
                    label1.Text = "Error loading total.";
                    MessageBox.Show("Error loading total beneficiaries: " + ex.Message);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
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
