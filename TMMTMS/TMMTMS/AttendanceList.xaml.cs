using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TMMTMS
{
    public partial class AttendanceList : Window
    {
        public AttendanceList()
        {
            InitializeComponent();
            LoadDataGrid();
        }

        /// <summary>
        /// 
        /// Enables Drag Move with Left Mouse Down
        /// 
        /// </summary>
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_SwitchToTeammemberListPage(object sender, RoutedEventArgs e)
        {
            SwitchWindowHelper.SwitchToTeammemberListPage();
            this.Close();
        }

        private void Button_SwitchToProtocolPage(object sender, EventArgs e)
        {
            SwitchWindowHelper.SwitchToProtocolPage();
            this.Close();
        }

        private void LoadDataGrid()
        {
            //retrieve column names from database
            List<string> columnNames = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Datenbank.GetConnectionString()))
                {
                    Datenbank.OpenConnection(connection);

                    string query = "SELECT bezeichnung, datum FROM meeting";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader["bezeichnung"].ToString();
                                string date = reader["datum"].ToString();
                                string columnName = name + "\n " + date;

                                columnNames.Add(columnName);
                            }
                        }

                    }
                }
                //connection is automatically closed at the end of this block
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:  " + ex.Message);
            }

            //dynamically create DataGrid columns
            foreach (string columnName in columnNames)
            {
                DataGridTextColumn column = new DataGridTextColumn();
                column.Header = columnName;
                column.Binding = new Binding(columnName);
                datagrid_attendance.Columns.Add(column);
            }
            
        }

    }
}
