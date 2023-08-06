using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMMTMS
{

    internal class Meeting
    {
        private DateOnly date;
        private TimeOnly time;
        private string location;
        private Teammitglied lead;
        private Teammitglied[] present;
        private Teammitglied[] absent;
        private Teammitglied[] missing;

        public Meeting(DateOnly date, TimeOnly time, string location, Teammitglied lead, Teammitglied[] present, Teammitglied[] absent, Teammitglied[] missing)
        {
            this.date = date;
            this.time = time;
            this.location = location;
            this.lead = lead;
            this.present = present;
            this.absent = absent;
            this.missing = missing;
        }



    }
}
