using System;
using System.Windows;
using System.Windows.Input;

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
            //TODO for further Development
            MessageBoxHelper.ShowFailurePopUp("Mailversand noch in Bearbeitung.");
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
