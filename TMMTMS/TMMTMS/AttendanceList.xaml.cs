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
            AddMembersToDataGrid();
            AddDynamicColumnHeaders();
        }

        private void AddMembersToDataGrid()
        {
            List<string> teammemberNames = Datenbank.GetTeammemberNames();
            datagrid_attendance.ItemsSource = teammemberNames;
        }

        private void AddDynamicColumnHeaders()
        {
            List<string> columnHeaderNames = Datenbank.GetMeetingNames();
            foreach (string columnName in columnHeaderNames)
            {
                DataGridTextColumn column = new DataGridTextColumn();
                column.Header = columnName;
                column.Binding = new Binding(columnName);
                datagrid_attendance.Columns.Add(column);
            }
        }
    }
}
