using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TMMTMS
{
    public partial class MainWindow : Window
    {
        private BackgroundWorker backgroundWorkerToCountMembers;
        private BackgroundWorker backgroundWorkerToLoadDataGrid;
        private int numberOfTeammembers = -1;
        private DataView DataViewWithTeammemberdata;

        public MainWindow()
        {
            InitializeComponent();
            
            InitializeBackgroundWorkerCounterToCountMembers();
            //check for BackgroundWorkers existence
            if(backgroundWorkerToCountMembers != null)
            {
                backgroundWorkerToCountMembers.RunWorkerAsync();
            }

            InitializeBackgroundWorkerToLoadDataGrid();
            //check for BackgroundWorkers existence
            if (backgroundWorkerToLoadDataGrid != null)
            {
                backgroundWorkerToLoadDataGrid.RunWorkerAsync();
            }
        }

        private void InitializeBackgroundWorkerCounterToCountMembers()
        {
            backgroundWorkerToCountMembers = new BackgroundWorker();
            backgroundWorkerToCountMembers.DoWork += Backgroundworker_CountMembers;
            backgroundWorkerToCountMembers.RunWorkerCompleted += Backgroundworker_ShowNumberOfTeammembers;
            backgroundWorkerToCountMembers.WorkerSupportsCancellation = false;
            backgroundWorkerToCountMembers.WorkerReportsProgress = false;
        }

        private void InitializeBackgroundWorkerToLoadDataGrid()
        {
            backgroundWorkerToLoadDataGrid = new BackgroundWorker();
            backgroundWorkerToLoadDataGrid.DoWork += Backgroundworker_GetDataView;
            backgroundWorkerToLoadDataGrid.RunWorkerCompleted += Backgroundworker_ShowDataView;
            backgroundWorkerToLoadDataGrid.WorkerSupportsCancellation = false;
            backgroundWorkerToLoadDataGrid.WorkerReportsProgress = false;
        }

        /// <summary>
        /// 
        /// Enables Drag Move with Left Mouse Down
        /// 
        /// </summary>
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }

        }

        private void Button_SwitchToAddPage(object sender, EventArgs e)
        {
            SwitchWindowHelper.SwitchToAddPage();
            this.Close();
        }

        private void Button_SwitchToAttendanceListPage(object sender, EventArgs e)
        {
            SwitchWindowHelper.SwitchToAttendanceListPage();
            this.Close();
        }

        private void Button_SwitchToProtocolPage(object sender, EventArgs e)
        {
            SwitchWindowHelper.SwitchToProtocolPage();
            this.Close();
        }

        private void Button_SwitchToSendMailPage(object sender, EventArgs e)
        {
            SwitchWindowHelper.SwitchToSendMailPage();
            this.Close();
        }

        private void Backgroundworker_CountMembers(object sender, DoWorkEventArgs e)
        {
            numberOfTeammembers = Datenbank.CountTeammembers();
        }

        private void Backgroundworker_ShowNumberOfTeammembers(object sender, RunWorkerCompletedEventArgs e)
        {
            txtblock_teamember_counter.Text = numberOfTeammembers.ToString() + " Teammitglieder";
        }

        private void Backgroundworker_GetDataView(object sender, DoWorkEventArgs e)
        {
            this.DataViewWithTeammemberdata = Datenbank.GetDataViewWithData("teammitglied");
        }

        private void Backgroundworker_ShowDataView(object sender, RunWorkerCompletedEventArgs e)
        {
            datagrid_teammembers.ItemsSource = this.DataViewWithTeammemberdata;
        }
    }
}
