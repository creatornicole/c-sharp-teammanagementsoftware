using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
