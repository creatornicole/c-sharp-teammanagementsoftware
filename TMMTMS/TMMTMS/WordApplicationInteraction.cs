using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace TMMTMS
{
    internal class WordApplicationInteraction
    {
        public static void GenerateProtocolAsWordDocument(Protocol protocol, Meeting meeting, ProtocolTopic topic)
        {
            Application wordApplication = OpenWordApplication();
            Document wordDocument = CreateEmptyWordDocument(wordApplication);
            
        }

        private static Application OpenWordApplication()
        {
            Application wordApplication = null;
            try
            {
                wordApplication = new Application();
                wordApplication.Visible = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error trying open to Word Application: " + ex.Message);
            }

            return wordApplication;
        }

        private static Document CreateEmptyWordDocument(Application wordApplication)
        {
            Document wordDocument = wordApplication.Documents.Add();
            return wordDocument;
        }

        
    }
}
