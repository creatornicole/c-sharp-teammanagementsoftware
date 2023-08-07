using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace TMMTMS
{
    internal class Teammitglied
    {
        private string vorname;
        private string nachname;
        private string handynummer;
        private string position;
        private string abteilung;
        private string bereich;
        private string seminargruppe;
        private string hskuerzel;
        private DateTime geburtstag;
        private DateTime eintrittsdatum;

        public Teammitglied(string vorname, string nachname, string handynummer, string position, string abteilung, 
            string bereich, string seminargruppe, string hskuerzel, DateTime geburtstag, DateTime eintrittsdatum) 
        { 
            Vorname = vorname;
            Nachname = nachname;
            Handynummer = handynummer;
            Position = position;
            Abteilung = abteilung;
            Bereich = bereich;
            Seminargruppe  = seminargruppe;
            Hskuerzel  = hskuerzel;
            Geburtstag = geburtstag;
            Eintrittsdatum = eintrittsdatum;
        }

        public bool StoreMember(Teammitglied teammitglied) 
        {
            return Datenbank.InsertTeammemberData(teammitglied);
        }

        public string Vorname
        {
            get { return vorname; }
            set
            {
                /* <= 25 because of database column vorname(varchar(25), null) */
                if(!string.IsNullOrEmpty(value) || value.Length <= 25 )
                {
                    vorname = value;
                }
                else
                {
                    throw new ArgumentException(
                        "Column (vorname) cannot be null, empty or longer than 25 characters");
                }
            }
        }
        public string Nachname
        {
            get { return nachname; }
            set
            {
                /* <= 25 because of database column nachname(varchar(25), null) */
                if (!string.IsNullOrEmpty(value) && value.Length <= 25)
                {
                    nachname = value;
                }
                else
                {
                    throw new ArgumentException(
                        "Column (nachname) cannot be null, empty or longer than 25 characters");
                }
            }
        }
        public string Handynummer
        {
            get { return handynummer; }
            set
            {
                /* <= 25 because of database column handynummer(varchar(25), null) */
                if (!string.IsNullOrEmpty(value) || value.Length <= 25)
                {
                    handynummer = value;
                }
                else
                {
                    throw new ArgumentException(
                        "Column (handynummer) cannot be null, empty or longer than 25 characters");
                }
            }
        }
        public string Position
        {
            get { return position; }
            set
            {
                /* <= 50 because of database column position(varchar(50), null) */
                if (!string.IsNullOrEmpty(value) || value.Length <= 50)
                {
                    position = value;
                }
                else
                {
                    throw new ArgumentException(
                        "Column (position) cannot be null, empty or longer than 50 characters");
                }
            }
        }
        public string Abteilung
        {
            get { return abteilung; }
            set
            {
                /* <= 50 because of database column abteilung(varchar(50), null) */
                if (!string.IsNullOrEmpty(value) || value.Length <= 50)
                {
                    abteilung = value;
                }
                else
                {
                    throw new ArgumentException(
                        "Column (abteilung) cannot be null, empty or longer than 50 characters");
                }
            }
        }
        public string Bereich
        {
            get { return bereich; }
            set
            {
                /* <= 50 because of database column bereich(varchar(50), null) */
                if (!string.IsNullOrEmpty(value) || value.Length <= 50)
                {
                    bereich = value;
                }
                else
                {
                    throw new ArgumentException(
                        "Column (bereich) cannot be null, empty or longer than 50 characters");
                }
            }
        }
        public string Seminargruppe
        {
            get { return seminargruppe; }
            set
            {
                /* <= 9 because of database column seminargruppe(varchar(9), null) */
                /* usual e.g. pattern for seminargruppe at University of Applied Sciences Mittweida: IF21wS1-B */
                if (!string.IsNullOrEmpty(value) || value.Length <= 9)
                {
                    seminargruppe = value;
                }
                else
                {
                    throw new ArgumentException(
                        "Column (bereich) cannot be null, empty or longer than 9 characters");
                }
            }
        }
        public string Hskuerzel
        {
            get { return hskuerzel; }
            set
            {
                /* <= 8 because of database column hskuerzel(varchar(8), not null) */
                /* usual e.g. pattern for hskuerzel at University of Applied Sciences Mittweida: vsurname */
                if (!string.IsNullOrEmpty(value) || value.Length <= 8)
                {
                    hskuerzel = value;
                }
                else
                {
                    throw new ArgumentException(
                        "Column (hskuerzel) cannot be null, empty or longer than 8 characters");
                }
            }
        }
        public DateTime Geburtstag
        {
            get { return geburtstag; }
            set
            {
                if (value <= DateTime.Now)
                {
                    geburtstag = value;
                }
                else
                {
                    throw new ArgumentException(
                        "Column (geburtstag) is Date. Date cannot be in the future.");
                }
            }
        }
        public DateTime Eintrittsdatum
        {
            get { return eintrittsdatum; }
            set
            {
                if (value <= DateTime.Now)
                {
                    eintrittsdatum = value;
                }
                else
                {
                    throw new ArgumentException(
                        "Column (eintrittsdatum) is Date. Date cannot be in the future.");
                }
            }
        }

    }
}
