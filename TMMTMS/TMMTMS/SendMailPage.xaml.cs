using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    public partial class SendMailPage : Window
    {
        public SendMailPage()
        {
            InitializeComponent();
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

        private void Button_SendMail(object sender, EventArgs e)
        {
        }

        private void Button_SwitchToTeammemberListPage(object sender, EventArgs e)
        {
            SwitchWindowHelper.SwitchToTeammemberListPage();
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
    }
}
