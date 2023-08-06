using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// <param name="selectedItem"></param>
        /// <returns></returns>
        public static string GetValueFromComboBox(Object selectedItem)
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
    }
}
