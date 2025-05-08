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
            ExportSpesToExcel();
        }

      private void ExportSpesToExcel()
        {
            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT * FROM spes_table";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No beneficiaries found.");
                        return;
                    }

                    string templatePath = @"C:\Users\samsu\Desktop\edp1\Carl-bagato\edp-frontend\templates\stats_spes_bene.xlsx";
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss"); // e.g. 20250508_174522
                    string fileName = $"Statistics_SPES_Beneficiaries_Report_{timestamp}.xlsx";
                    string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

                    Excel.Application excelApp = new Excel.Application();
                    Excel.Workbook workbook = excelApp.Workbooks.Open(templatePath);
                    Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                    // === STATISTICAL SUMMARY SECTION ===
                    int summaryRow = 1;

                    int total = dt.Rows.Count;
                    int male = dt.Select("sex = 'Male'").Length;
                    int female = dt.Select("sex = 'Female'").Length;
                    int is4ps = dt.Select("[4ps_mem] = 'Yes'").Length;
                    int not4ps = dt.Select("[4ps_mem] = 'No'").Length;
                    int hasSocmed = dt.Select("socmed_account IS NOT NULL AND socmed_account <> ''").Length;
                    int noSocmed = dt.Select("socmed_account IS NULL OR socmed_account = ''").Length;

                    var yearGroups = dt.AsEnumerable()
                        .GroupBy(r => r.Field<int>("number_of_years")) 
                        .ToDictionary(g => g.Key, g => g.Count());

                    worksheet.Cells[summaryRow++, 1] = "Total Beneficiaries:";
                    worksheet.Cells[summaryRow++, 2] = total;

                    worksheet.Cells[summaryRow++, 1] = "Male:";
                    worksheet.Cells[summaryRow++, 2] = male;

                    worksheet.Cells[summaryRow++, 1] = "Female:";
                    worksheet.Cells[summaryRow++, 2] = female;

                    worksheet.Cells[summaryRow++, 1] = "4Ps Members:";
                    worksheet.Cells[summaryRow++, 2] = is4ps;

                    worksheet.Cells[summaryRow++, 1] = "Non-4Ps Members:";
                    worksheet.Cells[summaryRow++, 2] = not4ps;

                    worksheet.Cells[summaryRow++, 1] = "With SocMed Account:";
                    worksheet.Cells[summaryRow++, 2] = hasSocmed;

                    worksheet.Cells[summaryRow++, 1] = "No SocMed Account:";
                    worksheet.Cells[summaryRow++, 2] = noSocmed;

                    foreach (var group in yearGroups)
                    {
                        worksheet.Cells[summaryRow++, 1] = $"Years in SPES: {group.Key}";
                        worksheet.Cells[summaryRow - 1, 2] = group.Value;
                    }

                    worksheet.Columns.AutoFit();

                    workbook.SaveAs(savePath);
                    workbook.Close(false);
                    excelApp.Quit();

                    // Cleanup
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                    MessageBox.Show("Excel file saved:\n" + savePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    spesDashboard dilpForm = new spesDashboard();
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



        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.FormClosed += (s, args) => this.Show();
            form1.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ExportSpesToExcelWithInterop();
        }
        private void ExportSpesToExcelWithInterop()
        {
            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT * FROM spes_table";
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
                    string templatePath = @"C:\Users\samsu\Desktop\edp1\Carl-bagato\edp-frontend\templates\spes_bene.xlsx"; // Change this
                    string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "All_SPES_Beneficiaries_Report.xlsx");
                    
                    Excel.Application excelApp = new Excel.Application();
                    Excel.Workbook workbook = excelApp.Workbooks.Open(templatePath);
                    Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                    // Start inserting data from row 2 (assuming row 1 is header)
                    int startRow = 4;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        worksheet.Cells[startRow + i, 1] = dt.Rows[i]["spes_id"].ToString();
                        worksheet.Cells[startRow + i, 2] = dt.Rows[i]["type"].ToString();
                        worksheet.Cells[startRow + i, 3] = dt.Rows[i]["lname"].ToString();      
                        worksheet.Cells[startRow + i, 4] = dt.Rows[i]["fname"].ToString();       
                        worksheet.Cells[startRow + i, 5] = dt.Rows[i]["sex"].ToString();      
                        worksheet.Cells[startRow + i, 6] = dt.Rows[i]["bday"].ToString(); 
                        worksheet.Cells[startRow + i, 7] = dt.Rows[i]["contact_num"].ToString();
                        worksheet.Cells[startRow + i, 8] = dt.Rows[i]["email"].ToString();
                        worksheet.Cells[startRow + i, 11] = dt.Rows[i]["4ps_mem"].ToString();
                        worksheet.Cells[startRow + i, 9] = dt.Rows[i]["socmed_account"].ToString();
                        worksheet.Cells[startRow + i, 10] = dt.Rows[i]["number_of_years"].ToString();
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
                    spesDashboard dilpForm = new spesDashboard();
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

        private void button8_Click(object sender, EventArgs e)
        {
            ExportOldSpesToExcelWithInterop();
        }
        private void ExportOldSpesToExcelWithInterop()
        {
            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT * FROM spes_table Where type = 'old'";
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
                    string templatePath = @"C:\Users\samsu\Desktop\edp1\Carl-bagato\edp-frontend\templates\spes_bene.xlsx"; // Change this
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss"); // e.g. 20250508_174522
                    string fileName = $"Old_SPES_Beneficiaries_Report_{timestamp}.xlsx";
                    string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

                   
                    Excel.Application excelApp = new Excel.Application();
                    Excel.Workbook workbook = excelApp.Workbooks.Open(templatePath);
                    Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                    // Start inserting data from row 2 (assuming row 1 is header)
                    int startRow = 4;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        worksheet.Cells[startRow + i, 1] = dt.Rows[i]["spes_id"].ToString();
                        worksheet.Cells[startRow + i, 2] = dt.Rows[i]["lname"].ToString();
                        worksheet.Cells[startRow + i, 3] = dt.Rows[i]["fname"].ToString();
                        worksheet.Cells[startRow + i, 4] = dt.Rows[i]["sex"].ToString();
                        worksheet.Cells[startRow + i, 5] = dt.Rows[i]["bday"].ToString();
                        worksheet.Cells[startRow + i, 6] = dt.Rows[i]["contact_num"].ToString();
                        worksheet.Cells[startRow + i, 7] = dt.Rows[i]["email"].ToString();
                        worksheet.Cells[startRow + i, 8] = dt.Rows[i]["4ps_mem"].ToString();
                        worksheet.Cells[startRow + i, 9] = dt.Rows[i]["socmed_account"].ToString();
                        worksheet.Cells[startRow + i, 10] = dt.Rows[i]["number_of_years"].ToString();
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
                    spesDashboard dilpForm = new spesDashboard();
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

        private void button7_Click(object sender, EventArgs e)
        {
            ExportNewSpesToExcelWithInterop();
        }
        private void ExportNewSpesToExcelWithInterop()
        {
            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT * FROM spes_table Where type = 'new'";
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
                    string templatePath = @"C:\Users\samsu\Desktop\edp1\Carl-bagato\edp-frontend\templates\spes_bene.xlsx"; // Change this
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss"); // e.g. 20250508_174522
                    string fileName = $"New_SPES_Beneficiaries_Report_{timestamp}.xlsx";
                    string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

                    Excel.Application excelApp = new Excel.Application();
                    Excel.Workbook workbook = excelApp.Workbooks.Open(templatePath);
                    Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                    // Start inserting data from row 2 (assuming row 1 is header)
                    int startRow = 4;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        worksheet.Cells[startRow + i, 1] = dt.Rows[i]["spes_id"].ToString();
                        worksheet.Cells[startRow + i, 2] = dt.Rows[i]["lname"].ToString();
                        worksheet.Cells[startRow + i, 3] = dt.Rows[i]["fname"].ToString();
                        worksheet.Cells[startRow + i, 4] = dt.Rows[i]["sex"].ToString();
                        worksheet.Cells[startRow + i, 5] = dt.Rows[i]["bday"].ToString();
                        worksheet.Cells[startRow + i, 6] = dt.Rows[i]["contact_num"].ToString();
                        worksheet.Cells[startRow + i, 7] = dt.Rows[i]["email"].ToString();
                        worksheet.Cells[startRow + i, 8] = dt.Rows[i]["4ps_mem"].ToString();
                        worksheet.Cells[startRow + i, 9] = dt.Rows[i]["socmed_account"].ToString();
                        worksheet.Cells[startRow + i, 10] = dt.Rows[i]["number_of_years"].ToString();
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
                    spesDashboard dilpForm = new spesDashboard();
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
