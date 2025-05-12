using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace WindowsFormsApp1
{
    public partial class admin_program_bene : Form
    {
        public admin_program_bene()
        {
            InitializeComponent();

            // Lock form to center of screen
            this.StartPosition = FormStartPosition.CenterScreen;

            // Optional: prevent resizing or moving
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void admin_program_bene_Load(object sender, EventArgs e)
        {
            LoadTableNames();
        }

        // Method to load the table names into the DataGridView
        private void LoadTableNames()
        {
            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                try
                {
                    con.Open();

                    string query = @"
                SELECT TABLE_NAME AS 'Table Name'
                FROM information_schema.tables 
                WHERE table_schema = 'peso_edp_final'
                  AND TABLE_NAME IN ('admin_table', 'spes_table', 'dilp_table', 'gip_table', 'activity_logs_table');
            ";

                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading table list: " + ex.Message);
                }
            }
        }

        // Handle the search functionality
        private void button1_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Please enter a search term.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";
            string query = @"
        SELECT TABLE_NAME AS 'Table Name'
        FROM information_schema.tables 
        WHERE table_schema = 'peso_edp_final'
          AND TABLE_NAME IN ('admin_table', 'spes_table', 'dilp_table', 'gip_table', 'activity_logs_table')
          AND TABLE_NAME LIKE @search
    ";

            using (MySqlConnection con = new MySqlConnection(conString))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@search", "%" + searchTerm + "%");

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // If no results are found, add a dummy row indicating no results
                    if (dt.Rows.Count == 0)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = "Search not found";  // Add custom message in the first column
                        dt.Rows.Add(row);
                    }

                    // Bind the results to the DataGridView
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error performing search: " + ex.Message);
                }
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // You can add your logic here if you want to do something when a cell is clicked
            // For example:
            MessageBox.Show("Cell clicked at row " + e.RowIndex + " and column " + e.ColumnIndex);
        }

        // Handle the export functionality
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Index < 0)
            {
                MessageBox.Show("Please select a table to export.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedTable = dataGridView1.CurrentRow.Cells[0].Value?.ToString();
            if (string.IsNullOrEmpty(selectedTable) || selectedTable == "Search not found")
            {
                MessageBox.Show("No valid table selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ExportTableData(selectedTable);
        }



        private void ExportToExcel(DataTable dt, string exportFileName)
        {
            string templatePath = @"C:\Users\samsu\Desktop\edp1\Carl-bagato\edp-frontend\templates\gip_bene.xlsx"; // Template Path
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string defaultFileName = $"{exportFileName}_{timestamp}.xlsx";

            // Show SaveFileDialog for the user to select where to save the file
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Save Report",
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = defaultFileName,
                DefaultExt = "xlsx",
                AddExtension = true
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Export cancelled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string savePath = saveFileDialog.FileName;

            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Open(templatePath);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                // Insert data starting from row 2 (assuming row 1 is the header)
                int startRow = 2;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        worksheet.Cells[startRow + i, j + 1] = dt.Rows[i][j].ToString();
                    }
                }

                worksheet.Columns.AutoFit();

                // Save the workbook to the selected location
                workbook.SaveAs(savePath);
                workbook.Close(false);
                excelApp.Quit();

                // Cleanup
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                MessageBox.Show("Excel file saved:\n" + savePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting data to Excel: " + ex.Message);
            }
        }

        private void ExportTableData(string tableName)
        {
            string conString = "server=localhost;uid=root;pwd=1802;database=peso_edp_final";
            string query = $"SELECT * FROM {tableName}";

            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(conString))
            {
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving table data: " + ex.Message);
                    return;
                }
            }

            string templatePath = "";
            string exportName = "";

            switch (tableName)
            {
                case "gip_table":
                    templatePath = @"C:\Users\samsu\Desktop\edp1\Carl-bagato\edp-frontend\templates\gip_bene.xlsx";
                    exportName = "GIP_Beneficiaries_List";
                    break;
                case "spes_table":
                    templatePath = @"C:\Users\samsu\Desktop\edp1\Carl-bagato\edp-frontend\templates\spes_bene.xlsx";
                    exportName = "SPES_Beneficiaries_List";
                    break;
                case "dilp_table":
                    templatePath = @"C:\Users\samsu\Desktop\edp1\Carl-bagato\edp-frontend\templates\dilp_bene.xlsx";
                    exportName = "DILP_Beneficiaries_List";
                    break;
                case "admin_table":
                    templatePath = @"C:\Users\samsu\Desktop\edp1\Carl-bagato\edp-frontend\templates\admin_list.xlsx";
                    exportName = "Admin_List";
                    break;
                case "activity_logs_table":
                    templatePath = @"C:\Users\samsu\Desktop\edp1\Carl-bagato\edp-frontend\templates\activity_logs.xlsx";
                    exportName = "Activity_Logs";
                    break;
                default:
                    MessageBox.Show("Template for the selected table is not defined.");
                    return;
            }

            ExportToExcelWithTemplate(dt, exportName, templatePath, tableName);
        }


        private void ExportToExcelWithTemplate(DataTable dt, string exportFileName, string templatePath, string tableName)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string defaultFileName = $"{exportFileName}_{timestamp}.xlsx";

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = $"Save {exportFileName}",
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = defaultFileName,
                DefaultExt = "xlsx",
                AddExtension = true
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Export cancelled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string savePath = saveFileDialog.FileName;

            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Open(templatePath);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                int headerRow = 4;
                int startRow = 5;

                // Write column headers from DataTable
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[headerRow, i + 1] = dt.Columns[i].ColumnName;
                }

                // Write data
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        worksheet.Cells[startRow + i, j + 1] = dt.Rows[i][j].ToString();
                    }
                }

                // Now write data rows
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch (tableName)
                    {
                        case "gip_table":
                            worksheet.Cells[startRow + i, 1] = dt.Rows[i]["gip_id"].ToString();
                            goto case "shared_person_fields";

                        case "spes_table":
                            worksheet.Cells[startRow + i, 1] = dt.Rows[i]["spes_id"].ToString();
                            goto case "shared_person_fields";

                        case "dilp_table":
                            worksheet.Cells[startRow + i, 1] = dt.Rows[i]["dilp_id"].ToString();
                            goto case "shared_person_fields";

                        case "shared_person_fields":
                            worksheet.Cells[startRow + i, 2] = dt.Rows[i]["lname"].ToString();
                            worksheet.Cells[startRow + i, 3] = dt.Rows[i]["fname"].ToString();
                            worksheet.Cells[startRow + i, 4] = dt.Rows[i]["sex"].ToString();
                            worksheet.Cells[startRow + i, 5] = dt.Rows[i]["bday"].ToString();
                            worksheet.Cells[startRow + i, 6] = dt.Rows[i]["contact_num"].ToString();
                            worksheet.Cells[startRow + i, 7] = dt.Rows[i]["email"].ToString();
                            worksheet.Cells[startRow + i, 8] = dt.Rows[i]["socmed_account"].ToString();
                            break;

                        case "admin_table":
                            worksheet.Cells[startRow + i, 1] = dt.Rows[i]["admin_id"].ToString();
                            worksheet.Cells[startRow + i, 2] = dt.Rows[i]["username"].ToString();
                            worksheet.Cells[startRow + i, 3] = dt.Rows[i]["email"].ToString();
                            worksheet.Cells[startRow + i, 4] = dt.Rows[i]["role"].ToString();
                            break;

                        case "activity_logs_table":
                            worksheet.Cells[startRow + i, 1] = dt.Rows[i]["log_id"].ToString();
                            worksheet.Cells[startRow + i, 2] = dt.Rows[i]["username"].ToString();
                            worksheet.Cells[startRow + i, 3] = dt.Rows[i]["action"].ToString();
                            worksheet.Cells[startRow + i, 4] = dt.Rows[i]["timestamp"].ToString();
                            break;

                        default:
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                worksheet.Cells[startRow + i, j + 1] = dt.Rows[i][j].ToString();
                            }
                            break;
                    }
                }

                worksheet.Columns.AutoFit();
                workbook.SaveAs(savePath);
                workbook.Close(false);
                excelApp.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                MessageBox.Show("Excel file saved:\n" + savePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting data to Excel: " + ex.Message);
            }
        }



        private DataTable GetDataTableFromDataGridView()
        {
            DataTable dt = new DataTable();

            // Add columns to the DataTable
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                dt.Columns.Add(column.HeaderText);
            }

            // Add rows to the DataTable
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    DataRow dataRow = dt.NewRow();
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataRow[i] = row.Cells[i].Value?.ToString() ?? string.Empty; // Handle null values
                    }
                    dt.Rows.Add(dataRow);
                }
            }

            return dt;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            adminSettings spesForm = new adminSettings();
            spesForm.FormClosed += (s, args) => this.Show();
            spesForm.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            adminActivityLogs spesForm = new adminActivityLogs();
            spesForm.FormClosed += (s, args) => this.Show();
            spesForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            addAdminAccount spesForm = new addAdminAccount();
            spesForm.FormClosed += (s, args) => this.Show();
            spesForm.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            adminUpdateAccount spesForm = new adminUpdateAccount();
            spesForm.FormClosed += (s, args) => this.Show();
            spesForm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 spesForm = new Form1();
            spesForm.FormClosed += (s, args) => this.Show();
            spesForm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            admin_program_bene spesForm = new admin_program_bene();
            spesForm.FormClosed += (s, args) => this.Show();
            spesForm.Show();
            this.Hide();
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            // Create a new login form
            loginForm newLogin = new loginForm();

            // Hide this form first to avoid UI flickering
            this.Hide();

            // Show login form
            newLogin.Show();

            // Close this form (after login form is fully shown)
            this.BeginInvoke((Action)(() => this.Close()));
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
    }
}
