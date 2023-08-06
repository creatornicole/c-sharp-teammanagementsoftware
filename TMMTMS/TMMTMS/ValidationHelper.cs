using System;

namespace TMMTMS
{
    internal static class ValidationHelper
    {
        public static bool IsStringValid(string str, int maxLength)
        {
            if (!string.IsNullOrWhiteSpace(str) && str.Length <= maxLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsDateValid(DateTime date)
        {
            if (date <= DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsComboBoxSelectedItemValid(Object selectedItem)
        {
            string comboBoxValue = "";

            if (selectedItem != null)
            {
                string selectedItemAsString = Convert.ToString(selectedItem);
                int trimIndex = selectedItemAsString.IndexOf(':');

                comboBoxValue = selectedItemAsString.Substring(trimIndex + 1);
            }

            if (selectedItem == null || string.IsNullOrWhiteSpace(comboBoxValue))
            {
                return false;
            }

            return true;
        }
    }
}
