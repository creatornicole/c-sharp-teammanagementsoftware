using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMMTMS
{
    internal static class OperationHelper
    {
        public static string GetListStringAsOneString(List<string> contentAsList)
        {
            string contentAsString = string.Join(" ", contentAsList);
            return contentAsString;
        }

        public static string GetListStringAsOneStringSeparatedByComma(List<string> contentAsList)
        {
            string contentAsString = string.Join(", ", contentAsList);
            return contentAsString;
        }

        public static string GetListStringAsOneStringSeparatedByNewLine(List<string> contentAsList)
        {
            string contentAsString = string.Join("\n", contentAsList);
            return contentAsString;
        }
    }
}
