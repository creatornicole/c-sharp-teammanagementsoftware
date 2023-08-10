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
using System.ComponentModel;

namespace TMMTMS
{
    public partial class AttendanceList : Window
    {
        private BackgroundWorker backgroundWorkerToLoadDynamicColumns;
        private BackgroundWorker backgroundWorkerToLoadTeammemberColumn;
        List<string> columnHeaderNames;
        List<string> teammemberNames;

        public AttendanceList()
        {
            InitializeComponent();

            InitializeBackgroundWorkerToLoadDataGrid();
            InitializeBackgroundWorkerToLoadTeammemberColumn();

            //check for existence of both BackgroundWorkers before running them
            if (backgroundWorkerToLoadDynamicColumns != null && backgroundWorkerToLoadTeammemberColumn != null)
            {
                LoadDataGrid();
            }
        }

        private void InitializeBackgroundWorkerToLoadDataGrid()
        {
            backgroundWorkerToLoadDynamicColumns = new BackgroundWorker();
            backgroundWorkerToLoadDynamicColumns.DoWork += Backgroundworker_GetMeetingNames;
            backgroundWorkerToLoadDynamicColumns.RunWorkerCompleted += Backgroundworker_LoadColumns;
            backgroundWorkerToLoadDynamicColumns.WorkerSupportsCancellation = false;
            backgroundWorkerToLoadDynamicColumns.WorkerReportsProgress = false;
        }

        private void InitializeBackgroundWorkerToLoadTeammemberColumn()
        {
            backgroundWorkerToLoadTeammemberColumn = new BackgroundWorker();
            backgroundWorkerToLoadTeammemberColumn.DoWork += Backgroundworker_GetTeammemberNames;
            backgroundWorkerToLoadTeammemberColumn.RunWorkerCompleted += Backgroundworker_LoadItemsSource;
            backgroundWorkerToLoadTeammemberColumn.WorkerSupportsCancellation = false;
            backgroundWorkerToLoadTeammemberColumn.WorkerReportsProgress = false;
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
            backgroundWorkerToLoadTeammemberColumn.RunWorkerAsync();
            backgroundWorkerToLoadDynamicColumns.RunWorkerAsync();
        }

        private void Backgroundworker_GetMeetingNames(object sender, DoWorkEventArgs e)
        {
            this.columnHeaderNames = Datenbank.GetMeetingNames();
        }

        private void Backgroundworker_LoadColumns(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Error == null) 
            {
                foreach (string columnName in this.columnHeaderNames)
                {
                    DataGridTextColumn column = new DataGridTextColumn();
                    column.Header = columnName;
                    column.Binding = new Binding(columnName);
                    datagrid_attendance.Columns.Add(column);
                }
            }
            else
            {
                MessageBoxHelper.ShowFailurePopUp("Spalten konnten nicht geladen werden.");
            }
        }

        private void Backgroundworker_GetTeammemberNames(object sender, DoWorkEventArgs e)
        {
            this.teammemberNames = Datenbank.GetTeammemberNames();
        }

        private void Backgroundworker_LoadItemsSource(object sender, RunWorkerCompletedEventArgs e)
        {
            datagrid_attendance.ItemsSource = this.teammemberNames;
        }
    }
}
