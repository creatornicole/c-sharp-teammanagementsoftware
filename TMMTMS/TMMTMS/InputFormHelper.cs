using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        /// Pre-defined Methods to Get Value from ComboBox did not work (Error: not defined methods)
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
            List<string> selectedListBoxItemsAsString = new List<string>();

            foreach(var selectedItem in listBox.SelectedItems)
            {
                string selectedItemAsString = selectedItem.ToString();
                selectedListBoxItemsAsString.Add(selectedItemAsString);
            }
            return selectedListBoxItemsAsString;
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
