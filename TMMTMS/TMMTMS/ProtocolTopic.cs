using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMMTMS
{
    internal class ProtocolTopic
    {
        private Protocol associatedProtocol;
        private string headline;
        private List<string> content;

        public ProtocolTopic(Protocol protocol, string headline, List<string> content)
        {
            AssociatedProtocol = protocol;
            Headline = headline;
            Content = content;
        }

        public string GetContentAsString(List<string> contentAsList)
        {
            return string.Join("; ", contentAsList);
        }

        public Protocol AssociatedProtocol
        {
            get { return associatedProtocol; }
            set { associatedProtocol = value; }
        }
        public string Headline
        {
            get { return headline; }
            set
            {
                /* <= 30 because of database column ueberschrift(varchar(30)) */
                if (!string.IsNullOrEmpty(value) || value.Length <= 30)
                {
                    headline = value;
                }
                else
                {
                    throw new ArgumentException(
                        "Column (ueberschrift) cannot be null, empty or longer than 30 characters");
                }
            }
        }
        public List<string> Content
        {
            get { return content; }
            set
            {
                string contentAsString = OperationHelper.GetListStringAsOneString(value);
                /* <= 1080 because of database column inhalt(varchar(1080)) */
                if (!string.IsNullOrEmpty(contentAsString) || contentAsString.Length <= 1080)
                {
                    content = new List<string>(value);
                }
                else
                {
                    throw new ArgumentException(
                        "Column (inhalt) cannot be null, empty or longer than 1080 characters");
                }
            }
        }

        
    }
}
