using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using MySql.Data.MySqlClient;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace WindowsFormsApp1
{
    public partial class dilpDashboard : Form
    {
        public dilpDashboard()
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

        private void adddilpbtn_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dilpDashboard dilpForm = new dilpDashboard();
            dilpForm.FormClosed += (s, args) => this.Show();
            dilpForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dilpBeneficiary dilpForm = new dilpBeneficiary();
            dilpForm.FormClosed += (s, args) => this.Show();
            dilpForm.Show();
            this.Hide();
        }

        private void addspesbtn_Click(object sender, EventArgs e)
        {
            addDilpBeneficiary dilpForm = new addDilpBeneficiary();
            dilpForm.FormClosed += (s, args) => this.Show();
            dilpForm.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            updateDilpBeneficiary dilpForm = new updateDilpBeneficiary();
            dilpForm.FormClosed += (s, args) => this.Show();
            dilpForm.Show();
            this.Hide();
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            deleteDilpBeneficiary dilpForm = new deleteDilpBeneficiary();
            dilpForm.FormClosed += (s, args) => this.Show();
            dilpForm.Show();
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

        private void label1_Click(object sender, EventArgs e)
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
                FROM dilp_table
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
                    string query = "SELECT COUNT(*) FROM dilp_table";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    int total = Convert.ToInt32(cmd.ExecuteScalar());

                    label1.Text = total.ToString();
                }
                catch (Exception ex)
                {
                    label1.Text = "Error loading total.";
                    MessageBox.Show("Error loading total beneficiaries: " + ex.Message);
                }
            }
        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ExporDilpBeneToExcel();
        }
        private void ExporDilpBeneToExcel()
        {
            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT * FROM dilp_table";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No 'old' beneficiaries found.");
                        return;
                    }

                    // ✅ Load your Excel template
                    string templatePath = @"C:\Users\samsu\Desktop\edp1\Carl-bagato\edp-frontend\templates\atats_dilp_bene.xlsx"; // Change this
                    string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Statistics_Dilp_Beneficiaries_Report.xlsx");

                    Excel.Application excelApp = new Excel.Application();
                    Excel.Workbook workbook = excelApp.Workbooks.Open(templatePath);
                    Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                    // Start inserting data from row 2 (assuming row 1 is header)
                    int startRow = 4;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        worksheet.Cells[startRow + i, 1] = dt.Rows[i]["dilp_id"].ToString();
                        worksheet.Cells[startRow + i, 2] = dt.Rows[i]["lname"].ToString();
                        worksheet.Cells[startRow + i, 3] = dt.Rows[i]["fname"].ToString();
                        worksheet.Cells[startRow + i, 4] = dt.Rows[i]["sex"].ToString();
                        worksheet.Cells[startRow + i, 5] = dt.Rows[i]["bday"].ToString();
                        worksheet.Cells[startRow + i, 6] = dt.Rows[i]["contact_num"].ToString();
                        worksheet.Cells[startRow + i, 7] = dt.Rows[i]["email"].ToString();
                        worksheet.Cells[startRow + i, 8] = dt.Rows[i]["socmed_account"].ToString();
                    }

                    worksheet.Columns.AutoFit();

                    // Save As new file
                    workbook.SaveAs(savePath);
                    workbook.Close(false);
                    excelApp.Quit();


                    // Cleanup
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                    MessageBox.Show("Excel file saved:\n" + savePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dilpDashboard dilpForm = new dilpDashboard();
                    dilpForm.FormClosed += (s, args) => this.Show();
                    dilpForm.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }



        private void button6_Click(object sender, EventArgs e)
        {
            ExporDilpToExcel();
        }
        private void ExporDilpToExcel()
        {
            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT * FROM dilp_table";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No 'old' beneficiaries found.");
                        return;
                    }

                    // ✅ Load your Excel template
                    string templatePath = @"C:\Users\samsu\Desktop\edp1\Carl-bagato\edp-frontend\templates\dilp_bene.xlsx"; // Change this
                    string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "List_Dilp_Beneficiariest.xlsx");

                    Excel.Application excelApp = new Excel.Application();
                    Excel.Workbook workbook = excelApp.Workbooks.Open(templatePath);
                    Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                    // Start inserting data from row 2 (assuming row 1 is header)
                    int startRow = 4;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        worksheet.Cells[startRow + i, 1] = dt.Rows[i]["dilp_id"].ToString();
                        worksheet.Cells[startRow + i, 2] = dt.Rows[i]["lname"].ToString();
                        worksheet.Cells[startRow + i, 3] = dt.Rows[i]["fname"].ToString();
                        worksheet.Cells[startRow + i, 4] = dt.Rows[i]["sex"].ToString();
                        worksheet.Cells[startRow + i, 5] = dt.Rows[i]["bday"].ToString();
                        worksheet.Cells[startRow + i, 6] = dt.Rows[i]["contact_num"].ToString();
                        worksheet.Cells[startRow + i, 7] = dt.Rows[i]["email"].ToString();
                        worksheet.Cells[startRow + i, 8] = dt.Rows[i]["socmed_account"].ToString();
                    }

                    worksheet.Columns.AutoFit();

                    // Save As new file
                    workbook.SaveAs(savePath);
                    workbook.Close(false);
                    excelApp.Quit();


                    // Cleanup
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                    MessageBox.Show("Excel file saved:\n" + savePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dilpDashboard dilpForm = new dilpDashboard();
                    dilpForm.FormClosed += (s, args) => this.Show();
                    dilpForm.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        }
}
