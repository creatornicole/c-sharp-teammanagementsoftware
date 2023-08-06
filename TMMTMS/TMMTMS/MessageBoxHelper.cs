using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TMMTMS
{
    internal static class MessageBoxHelper
    {
        public static void ShowSuccessPopUp(string message)
        {
            MessageBox.Show(message, "Erfolgreich", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void ShowFailurePopUp(string message)
        {
            MessageBox.Show(message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
        }


    }
}
