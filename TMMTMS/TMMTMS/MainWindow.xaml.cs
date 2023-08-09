using System;
using System.Collections.ObjectModel;
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
        public MainWindow()
        {
            InitializeComponent();
            datagrid_teammembers.ItemsSource = Datenbank.GetDataViewWithData("teammitglied");
            ShowNumberOfTeammembers();
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

        private void ShowNumberOfTeammembers()
        {
            int numberOfTeammembers = Datenbank.CountTeammembers();
            txtblock_teamember_counter.Text = numberOfTeammembers.ToString() + " Teammitglieder";
        }
    }
}
