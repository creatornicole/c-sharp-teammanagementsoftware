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
            AddProtocolContentToWordDocument(wordDocument, protocol, meeting, topic);
        }

        private static string GenerateProtocolData(Protocol protocol, Meeting meeting, ProtocolTopic topic)
        {
            string headline = meeting.Name;
            string meetingData = meeting.Date.ToString("dd.MM.yyyy") + "\t" + meeting.Time.ToString() + "\t" + meeting.Location;

            List<string> presentMemberNames = Datenbank.GetTeammembernamesFromHsKuerzelList(meeting.PresentMembers);
            List<string> absentMemberNames = Datenbank.GetTeammembernamesFromHsKuerzelList(meeting.AbsentMembers);

            string presentMembers = "Anwesende Teammitglieder: " + OperationHelper.GetListStringAsOneStringSeparatedByComma(presentMemberNames);
            string absentMembers = "Abwesende Teammitglieder: " + OperationHelper.GetListStringAsOneStringSeparatedByComma(absentMemberNames);
            string topicHeading = topic.Headline;
            string topicContent = OperationHelper.GetListStringAsOneStringSeparatedByNewLine(topic.Content);

            string protocolData = headline + "\n" + meetingData + "\n" + presentMembers + "\n" + absentMembers + "\n" + topicHeading + "\n" + topicContent;

            return protocolData;
        }

        private static void AddProtocolContentToWordDocument(Document wordDocument, Protocol protocol, Meeting meeting, ProtocolTopic topic)
        {
            //mehrdeutiger Verweis Range
            Microsoft.Office.Interop.Word.Range range = wordDocument.Content;
            range.Text = GenerateProtocolData(protocol, meeting, topic);
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
