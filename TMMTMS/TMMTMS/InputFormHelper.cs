using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TMMTMS
{
    internal class InputFormHelper
    {
        /// <summary>
        /// 
        /// vorgefertigte Methoden um Value aus ComboBox zu erhalten haben nicht funktioniert - not defined
        /// 
        /// </summary>
        public static string GetValueFromComboBoxItem(Object selectedItem)
        {
            string comboBoxValue = "";

            if (selectedItem != null)
            {
                string selectedItemAsString = Convert.ToString(selectedItem);
                int trimIndex = selectedItemAsString.IndexOf(' ');

                comboBoxValue = selectedItemAsString.Substring(trimIndex + 1);
            }

            return comboBoxValue;
        }

        public static List<string> GetSelectedListBoxItemsAsStrings(ListBox listBox)
        {
            List<string> selectedListBoxItems = new List<string>();
            foreach(Object selectedItem in listBox.SelectedItems)
            {
                if(selectedItem is ListBoxItem listBoxItem)
                {
                    selectedListBoxItems.Add(listBoxItem.Content.ToString());
                }
            }
            return selectedListBoxItems;
        }

        public static TimeOnly GetTimeOnlyFromString(string timeString)
        {
            if (IsValidTimeFormat(timeString))
            {
                return TimeOnly.ParseExact(timeString, "HH:mm", null);
            }
            return new TimeOnly(00, 00); //if timeString invalid -> return 00:00
        }

        public static bool IsValidTimeFormat(string timeString)
        {
            //checks if string fits the pattern "HH:mm" representing hours and minutes
            //in 24-hour format
            string timePattern = @"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$";
            return Regex.IsMatch(timeString, timePattern);
        }
    }
}
