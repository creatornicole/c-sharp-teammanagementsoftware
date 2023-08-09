using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMMTMS
{
    static class SwitchWindowHelper
    {
        public static void SwitchToTeammemberListPage()
        {
            MainWindow teamemberListWindow = new MainWindow();
            teamemberListWindow.Show();
        }

        public static void SwitchToAttendanceListPage()
        {
            AttendanceList attendanceListWindow = new AttendanceList();
            attendanceListWindow.Show();
        }

        public static void SwitchToProtocolPage()
        {
            ProtocolWindow protocolWindow = new ProtocolWindow();
            protocolWindow.Show();
        }

        public static void SwitchToAddPage()
        {
            Window1 win = new Window1();
            win.Show();
        }

        public static void SwitchToSendMailPage()
        {
            SendMailPage sendMailPage = new SendMailPage();
            sendMailPage.Show();
        }
    }
}
