using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TMMTMS
{

    internal class Meeting
    {
        private string name;
        private DateTime date;
        private TimeOnly time;
        private string location;

        public Meeting(string name, DateTime date, TimeOnly time, string location)
        {
            Name = name;
            Date = date;
            Time = time;
            Location = location;
        }

        public string Name
        {
            get { return name; }
            set
            { 
                /* <= 50 because of database column bezeichnung(varchar(50)) */
                if (!string.IsNullOrEmpty(value) || value.Length <= 50)
                {
                    name = value;
                }
                else
                {
                    throw new ArgumentException(
                        "Column (name) cannot be null, empty or longer than 50 characters");
                }
            }
        }
        public DateTime Date
        {
            get { return date; }
            set
            {
                if (value <= DateTime.Now)
                {
                    date = value;
                }
                else
                {
                    throw new ArgumentException(
                        "Column (datum) is Date. Date cannot be in the future.");
                }
            }
        }
        public TimeOnly Time
        {
            get { return time; }
            set { time = value; }
        }
        public string Location
        {
            get { return location; }
            set
            {
                /* <= 30 because of database column ort(varchar(30)) */
                if (!string.IsNullOrEmpty(value) || value.Length <= 30)
                {
                    location = value;
                }
                else
                {
                    throw new ArgumentException(
                        "Column (ort) cannot be null, empty or longer than 30 characters");
                }
            }
        }



    }
}
