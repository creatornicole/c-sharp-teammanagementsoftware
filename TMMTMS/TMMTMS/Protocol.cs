using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMMTMS
{
    internal class Protocol
    {
        private Meeting meeting;
        private DateTime meetingDate;
        private DateTime createDate;
        public Protocol(Meeting meeting, DateTime meetingDate) 
        {
            Meeting = meeting;
            MeetingDate = meetingDate;
            CreateDate = DateTime.Now;
        }

        public Meeting Meeting 
        { 
            get { return meeting; } 
            set { meeting = value; }
        }
        public DateTime MeetingDate
        {
            get { return meetingDate; }
            set
            {
                if (value <= DateTime.Now)
                {
                    meetingDate = value;
                }
                else
                {
                    throw new ArgumentException(
                        "Column (meetingdatum) is Date. Date cannot be in the future.");
                }
            }
        }
        public DateTime CreateDate
        {
            get { return createDate; }
            set
            {
                if (value <= DateTime.Now)
                {
                    createDate = value;
                }
                else
                {
                    throw new ArgumentException(
                        "Column (verfassungsdatum) is Date. Date cannot be in the future.");
                }
            }
        }
    }
}
