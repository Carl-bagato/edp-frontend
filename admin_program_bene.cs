using System;
using System.Data;
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
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No data available to export.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create a DataTable from DataGridView data
            DataTable dt = GetDataTableFromDataGridView();

            // Export the DataTable to Excel
            ExportToExcel(dt, "Search_Result");
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
    }
}
