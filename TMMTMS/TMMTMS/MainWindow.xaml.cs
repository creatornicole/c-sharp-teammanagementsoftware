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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }

        }

        private void Button_SwitchToAddPage(object sender, EventArgs e)
        {
            Window1 win = new Window1();
            win.Show();
            this.Close();
        }

        private void Button_SwitchToProtocolPage(object sender, EventArgs e)
        {
            ProtocolWindow protocolWindow = new ProtocolWindow();
            protocolWindow.Show();
            this.Close();
        }

        private void ShowNumberOfTeammembers()
        {
            int numberOfTeammembers = Datenbank.CountTeammembers();
            txtblock_teamember_counter.Text = numberOfTeammembers.ToString() + " Teammitglieder";
        }
    }
}
