using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TMMTMS
{
    class Datenbank
    {

        public static void OpenConnection(SqlConnection connection)
        {
            try
            {
                connection.Open();
            }
            catch (Exception ex) 
            { 
                Console.WriteLine("Error connecting to database: " + ex.Message);
            }            
        }

        public static bool InsertTeammemberData(Teammitglied teammember)
        {
            string vorname = teammember.Vorname;
            string nachname = teammember.Nachname;
            string handynummer = teammember.Handynummer;
            string position = teammember.Position;
            string abteilung = teammember.Abteilung;
            string bereich = teammember.Bereich;
            string seminargruppe = teammember.Seminargruppe;
            string hskuerzel = teammember.Hskuerzel;
            DateTime geburtstag = teammember.Geburtstag;
            DateTime eintrittsdatum = teammember.Eintrittsdatum;

            string insertQuery = "INSERT INTO teammitglied (hs_kuerzel, vorname, nachname, seminargruppe, abteilung, bereich, position, beitrittsdatum, geburtsdatum, handynummer) "
                    + "VALUES (@hs_kuerzel, @vorname, @nachname, @seminargruppe, @abteilung, @bereich, @position, @beitrittsdatum, @geburtsdatum, @handynummer)";

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    OpenConnection(connection);

                    SqlCommand command = new SqlCommand(insertQuery, connection);

                    command.Parameters.AddWithValue("@hs_kuerzel", hskuerzel);
                    command.Parameters.AddWithValue("@vorname", vorname);
                    command.Parameters.AddWithValue("@nachname", nachname);
                    command.Parameters.AddWithValue("@seminargruppe", seminargruppe);
                    command.Parameters.AddWithValue("@abteilung", abteilung);
                    command.Parameters.AddWithValue("@bereich", bereich);
                    command.Parameters.AddWithValue("@position", position);
                    command.Parameters.AddWithValue("@beitrittsdatum", eintrittsdatum);
                    command.Parameters.AddWithValue("@geburtsdatum", geburtstag);
                    command.Parameters.AddWithValue("@handynummer", handynummer);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (!IsInsertSuccess(rowsAffected))
                    {
                        Console.WriteLine("Error inserting teammember data to database.");
                    }
                    return true;
                }
                //connection is automatically closed at the end of this block
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception:  " + ex.Message);
            }       
            
            return false;
        }

        public static bool StoreProtocol(Meeting meeting, Protocol protocol, ProtocolTopic topic)
        {
            List<string> presentMembers = meeting.PresentMembers;
            List<string> absentMembers = meeting.AbsentMembers;

            if(InsertMeetingData(meeting) && InsertProtocolData(protocol, GetLastInsertedMeetingId())
                && InsertProtocolTopicData(topic, GetLastInsertedProtocolId()) 
                && InsertAttendanceData(presentMembers, absentMembers))
            {
                return true;
            } 
            else
            {
                return false;
            }
        }
        
        public static List<string> GetTeammemberNames()
        {
            List<string> teammembernames = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    OpenConnection(connection);

                    string query = "SELECT nachname, vorname FROM teammitglied";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string firstName = reader["vorname"].ToString();
                                string lastName = reader["nachname"].ToString();
                                string completeName = lastName + ", " + firstName;

                                teammembernames.Add(completeName);
                            }
                        }
                    }
                }
                //connection is automatically closed at the end of this block
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:  " + ex.Message);
            }

            return teammembernames;
        }

        public static string GetHsKuerzelFromTeammemberName(string teammembername)
        {
            string hskuerzel = null;
            string firstName = null;
            string lastName = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    OpenConnection(connection);

                    string query = "SELECT hs_kuerzel FROM teammitglied WHERE vorname = @vorname AND nachname = @nachname";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        string[] parts = teammembername.Split(','); //name format: 'lastName, firstName'
                        if (parts.Length == 2)
                        {
                            firstName = parts[1].Trim(); //remove whitespaces
                            lastName = parts[0].Trim();
                        }
    
                        command.Parameters.AddWithValue("@vorname", firstName);
                        command.Parameters.AddWithValue("@nachname", lastName);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    hskuerzel = reader["hs_kuerzel"].ToString();
                                }
                            }
                            /* using statement ensures that SqlDataReader is properly disposed of when it goes out of scope
                                that includes closing the reader */
                    }
                }
                //connection is automatically closed at the end of this block
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:  " + ex.Message);
            }

            return hskuerzel;
        }

        private static string GetTeammembernameFromHsKuerzel(string hskuerzel)
        {
            string teammembername = null;
            string firstName = null;
            string lastName = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    OpenConnection(connection);

                    string query = "SELECT vorname, nachname FROM teammitglied WHERE hs_kuerzel = @hskuerzel";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@hskuerzel", hskuerzel);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                firstName = reader["vorname"].ToString();
                                lastName = reader["nachname"].ToString();
                                teammembername = firstName + " " + lastName;
                            }
                        }
                        /* using statement ensures that SqlDataReader is properly disposed of when it goes out of scope
                            that includes closing the reader */
                    }
                }
                //connection is automatically closed at the end of this block
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:  " + ex.Message);
            }

            return teammembername;
        }

        public static List<string> GetTeammembernamesFromHsKuerzelList(List<string> hskuerzelList)
        {
            List<string> teammembernames = new List<string>();

            foreach (string hskuerzel in hskuerzelList)
            {
                string teammembername = GetTeammembernameFromHsKuerzel(hskuerzel);
                teammembernames.Add(teammembername);
            }
            return teammembernames;
        }

        public static List<string> GetAttendanceDataOfTeammember(string hskuerzel)
        {
            List<string> attendanceStates = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    OpenConnection(connection);

                    string query = "SELECT anwesenheitsstatus FROM teammitglied "
                        + "CROSS JOIN meeting "
                        + "LEFT JOIN anwesenheit ON teammitglied.hs_kuerzel = anwesenheit.hs_kuerzel "
                        + "AND meeting.meeting_id = anwesenheit.meeting_id "
                        + "WHERE teammitglied.hs_kuerzel = @hskuerzel";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@hskuerzel", hskuerzel);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if(!reader.IsDBNull(reader.GetOrdinal("anwesenheitsstatus")))
                                {
                                    int attendanceState = reader.GetInt32(reader.GetOrdinal("anwesenheitsstatus"));
                                    string attendanceStateAsString = attendanceState.ToString();
                                    attendanceStates.Add(attendanceStateAsString);
                                } 
                                else
                                {
                                    attendanceStates.Add("NULL");
                                }                                
                            }
                        }
                    }
                }                
                //connection is automatically closed at the end of this block
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:  " + ex.Message);
            }

            return attendanceStates;
        }

        public static List<string> GetHsKuerzelFromTeammemberNames(List<string> teammembernames)
        {
            List<string> hskuerzelList = new List<string>();
            
            foreach(string teammembername in teammembernames)
            {
                string hskuerzel = GetHsKuerzelFromTeammemberName(teammembername);
                hskuerzelList.Add(hskuerzel);
            }
            return hskuerzelList;
        }

        public static List<string> GetMeetingNames()
        {
            List<string> columnNames = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Datenbank.GetConnectionString()))
                {
                    Datenbank.OpenConnection(connection);

                    string query = "SELECT bezeichnung, datum FROM meeting";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader["bezeichnung"].ToString();
                                string date = reader["datum"].ToString();
                                string columnName = name + "\n " + date;

                                columnNames.Add(columnName);
                            }
                        }
                    }
                }
                //connection is automatically closed at the end of this block
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:  " + ex.Message);
            }
            return columnNames;
        }

        private static bool InsertMeetingData(Meeting meeting)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    OpenConnection(connection);

                    string insertMeetingDataQuery = "INSERT INTO meeting (datum, ort, uhrzeit, bezeichnung) "
                        + "VALUES (@date, @location, @time, @name)";
                    SqlCommand insertMeetingDataCommand = new SqlCommand(insertMeetingDataQuery, connection);

                    insertMeetingDataCommand.Parameters.AddWithValue("@date", meeting.Date);
                    insertMeetingDataCommand.Parameters.AddWithValue("@location", meeting.Location);
                    insertMeetingDataCommand.Parameters.AddWithValue("@time", meeting.Time.ToTimeSpan());
                    insertMeetingDataCommand.Parameters.AddWithValue("@name", meeting.Name);

                    int rowsAffected = insertMeetingDataCommand.ExecuteNonQuery();
                    if (!IsInsertSuccess(rowsAffected))
                    {
                        Console.WriteLine("Error inserting meeting data to database.");
                    }
                }
                //connection is automatically closed at the end of this block
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:  " + ex.Message);
            }

            return false;
        }
        
        private static bool InsertProtocolData(Protocol protocol, int meetingId)
        {
            if(meetingId != -1) {
                string insertQuery = "INSERT INTO protokoll (meeting_id, meetingdatum, verfassungsdatum) "
                        + "VALUES (@meetingID, @meetingDate, @createDate)";

                try
                {
                    using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                    {
                        OpenConnection(connection);

                        SqlCommand command = new SqlCommand(insertQuery, connection);

                        SqlCommand insertProtocolDataCommand = new SqlCommand(insertQuery, connection);

                        insertProtocolDataCommand.Parameters.AddWithValue("@meetingID", meetingId);
                        insertProtocolDataCommand.Parameters.AddWithValue("@meetingDate", protocol.MeetingDate);
                        insertProtocolDataCommand.Parameters.AddWithValue("@createDate", protocol.CreateDate);

                        int rowsAffected = insertProtocolDataCommand.ExecuteNonQuery();
                        if (!IsInsertSuccess(rowsAffected))
                        {
                            Console.WriteLine("Error inserting protocol data to database.");
                        }
                    }
                    //connection is automatically closed at the end of this block
                    return true;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Exception: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception:  " + ex.Message);
                }
            }
            return false;
        }

        private static bool InsertProtocolTopicData(ProtocolTopic topic, int protocolId)
        {
            if(protocolId != -1)
            {
                List<string> contentAsList = topic.Content;
                string contentAsString = topic.GetContentAsString(contentAsList);

                string insertQuery = "INSERT INTO protokollthema (ueberschrift, protokoll_id, inhalt) "
                        + "VALUES (@headline, @protocolID, @content)";

                try
                {
                    using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                    {
                        OpenConnection(connection);

                        SqlCommand command = new SqlCommand(insertQuery, connection);

                        command.Parameters.AddWithValue("@headline", topic.Headline);
                        command.Parameters.AddWithValue("@protocolID", protocolId);
                        command.Parameters.AddWithValue("@content", contentAsString);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (!IsInsertSuccess(rowsAffected))
                        {
                            Console.WriteLine("Error inserting protocol topic data to database.");
                        }
                        return true;
                    }
                    //connection is automatically closed at the end of this block
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Exception: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception:  " + ex.Message);
                }
            }
            return false;
        }

        private static bool InsertAttendanceData(List<string> presentMembers, List<string> absentMembers)
        {
            if(InsertPresentMembers(presentMembers, GetLastInsertedMeetingId(), AttendanceStatus.Present)
                && InsertAbsentMembers(absentMembers, GetLastInsertedMeetingId(), AttendanceStatus.Absent))
            {
                return true;
            } 
            else
            {
                return false;
            }            
        }

        private static bool InsertPresentMembers(List<string> members, int meetingId, AttendanceStatus status)
        {
            int statusCode = (int)status;

            string insertQuery = "INSERT INTO anwesenheit (hs_kuerzel, meeting_id, anwesenheitsstatus) "
                    + "VALUES (@hskuerzel, @meetingID, @status)";

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    OpenConnection(connection);

                    SqlCommand command = new SqlCommand(insertQuery, connection);

                    foreach (string member in members)
                    {
                        command.Parameters.Clear(); /* reusing SqlCommand for each iterationm therefore
                                                            clear parameters before adding new ones */
                        command.Parameters.AddWithValue("hskuerzel", member);
                        command.Parameters.AddWithValue("@meetingID", meetingId);
                        command.Parameters.AddWithValue("@status", statusCode);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (!IsInsertSuccess(rowsAffected))
                        {
                            Console.WriteLine("Error inserting present members to database.");
                        }
                    }                    
                    return true;
                }
                //connection is automatically closed at the end of this block
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:  " + ex.Message);
            }
            return false;
        }

        private static bool InsertAbsentMembers(List<string> members, int meetingId, AttendanceStatus status)
        {
            int statusCode = (int)status;

            string insertQuery = "INSERT INTO anwesenheit (hs_kuerzel, meeting_id, anwesenheitsstatus) "
                    + "VALUES (@hskuerzel, @meetingID, @status)";

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    OpenConnection(connection);

                    SqlCommand command = new SqlCommand(insertQuery, connection);

                    foreach (string member in members)
                    {
                        command.Parameters.Clear(); /* reusing SqlCommand for each iterationm therefore
                                                            clear parameters before adding new ones */
                        command.Parameters.AddWithValue("hskuerzel", member);
                        command.Parameters.AddWithValue("@meetingID", meetingId);
                        command.Parameters.AddWithValue("@status", statusCode);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (!IsInsertSuccess(rowsAffected))
                        {
                            Console.WriteLine("Error inserting absent members to database.");
                        }
                    }
                    return true;
                }
                //connection is automatically closed at the end of this block
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:  " + ex.Message);
            }
            return false;
        }

        private static int GetLastInsertedMeetingId()
        {
            int meetingId = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    OpenConnection(connection);

                    //get id (primary key) of last inserted meeting
                    string meetingIdQuery = "SELECT IDENT_CURRENT('meeting');";
                    SqlCommand meetingIdCommand = new SqlCommand(meetingIdQuery, connection);
                    meetingId = Convert.ToInt32(meetingIdCommand.ExecuteScalar());
                }
                //connection is automatically closed at the end of this block
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:  " + ex.Message);
            }

            return meetingId;
        }

        private static int GetLastInsertedProtocolId()
        {
            int protocolId = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    OpenConnection(connection);

                    string protocolIdQuery = "SELECT IDENT_CURRENT('protokoll');";
                    SqlCommand protocolIdCommand = new SqlCommand(protocolIdQuery, connection);
                    protocolId = Convert.ToInt32(protocolIdCommand.ExecuteScalar());
                }
                //connection is automatically closed at the end of this block
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:  " + ex.Message);
            }

            return protocolId;
        }

        public static int CountTeammembers()
        {
            int numberOfTeammembers = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    OpenConnection(connection);

                    string countQuery = "SELECT COUNT(*) FROM teammitglied";

                    using (SqlCommand command = new SqlCommand(countQuery, connection))
                    {
                        numberOfTeammembers = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                //connection is automatically closed at the end of this block
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:  " + ex.Message);
            }
            return numberOfTeammembers;
        }

        public static DataView GetDataViewWithData(string table)
        {
            DataView dataViewWithData = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(Datenbank.GetConnectionString()))
                {
                    Datenbank.OpenConnection(connection);

                    string query = "SELECT * FROM " + table;

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable(table);
                        adapter.Fill(dataTable);
                        dataViewWithData = dataTable.DefaultView;

                    }
                }
                //connection is automatically closed at the end of this block
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:  " + ex.Message);
            }
            return dataViewWithData;
        }

        private static string GetConnectionString()
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();

            connectionStringBuilder.DataSource = "LAPTOP-D537U6PD\\SQLSERVERTMMTMS";
            connectionStringBuilder.InitialCatalog = "tmmbase";
            connectionStringBuilder.IntegratedSecurity = true;

            string connectionString = connectionStringBuilder.ConnectionString;

            return connectionString;
        }

        private static bool IsInsertSuccess(int rowsAffected)
        {
            if(rowsAffected == 1)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
