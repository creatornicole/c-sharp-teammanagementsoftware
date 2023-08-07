using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private static string GetConnectionString()
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            
            connectionStringBuilder.DataSource = "LAPTOP-D537U6PD\\SQLSERVERTMMTMS";
            connectionStringBuilder.InitialCatalog = "tmmbase";
            connectionStringBuilder.IntegratedSecurity = true;

            string connectionString = connectionStringBuilder.ConnectionString;

            return connectionString;
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
                            while(reader.Read())
                            {
                                string firstName = reader["vorname"].ToString();
                                string lastName = reader["nachname"].ToString();
                                string completeName = lastName + ", " + firstName;

                                teammembernames.Add(completeName);
                            }
                            reader.Close();
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

        public static bool StoreProtocol(Meeting meeting, Protocol protocol, ProtocolTopic topic)
        {
            //call methods in one method to define order of method calling

            //is working
            //

            //is not working
            //
            //InsertProtocolData(protocol)
            //InsertProtocolTopicData(topic)
            //InsertAttendanceData()

            if (InsertMeetingData(meeting) && InsertProtocolData(protocol))
                return true;
            
            return false;
        }

        private static bool InsertMeetingData(Meeting meeting)
        {
            DateTime date = meeting.Date;
            string location = meeting.Location;
            TimeOnly time = meeting.Time;
            string name = meeting.Name;

            TimeSpan timeForDatabase = time.ToTimeSpan(); //datatype time in database matches datatype TimeSpan in C#

            string insertQuery = "INSERT INTO meeting (datum, ort, uhrzeit, bezeichnung) "
                    + "VALUES (@date, @location, @time, @name)";

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    OpenConnection(connection);

                    SqlCommand command = new SqlCommand(insertQuery, connection);

                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@location", location);
                    command.Parameters.AddWithValue("@time", timeForDatabase);
                    command.Parameters.AddWithValue("@name", name);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (!IsInsertSuccess(rowsAffected))
                    {
                        Console.WriteLine("Error inserting meeting data to database.");
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

        private static bool InsertProtocolData(Protocol protocol)
        {
            //int meetingID = GetMeetingID(protocol.Meeting);
            int meetingID = 1;
            DateTime meetingDate = protocol.MeetingDate;
            DateTime createDate = protocol.CreateDate;

            if(meetingID != -1) {
                string insertQuery = "INSERT INTO protokoll (meeting_id, meeetingdatum, verfassungsdatum) "
                        + "VALUES (@meetingID, @meetingDate, @createDate)";

                try
                {
                    using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                    {
                        OpenConnection(connection);

                        SqlCommand command = new SqlCommand(insertQuery, connection);

                        command.Parameters.AddWithValue("@meetingID", 1);
                        command.Parameters.AddWithValue("@meetingDate", meetingDate);
                        command.Parameters.AddWithValue("@createDate", createDate);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (!IsInsertSuccess(rowsAffected))
                        {
                            Console.WriteLine("Error inserting protocol data to database.");
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

        private static int GetMeetingID(Meeting meeting)
        {
            int meetingId = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    OpenConnection(connection);

                    string query = "SELECT meeting_id FROM meeting " +
                        "WHERE datum = @date AND ort = @location AND uhrzeit = @time";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@date", meeting.Date);
                        command.Parameters.AddWithValue("@location", meeting.Location);
                        command.Parameters.AddWithValue("@time", meeting.Time);

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            meetingId = Convert.ToInt32(result);
                        }
                        else
                        {
                            Console.WriteLine("Parent Record Meeting not found for Protocol");
                        }
                    }
                    //connection is automatically closed at the end of this block
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:  " + ex.Message);
            }

            MessageBoxHelper.ShowFailurePopUp(meeting.Name);

            return meetingId;
        }

        private static int GetProtocolID(Protocol protocol)
        {
            int protocolId = -1; //return -1 in case of error or not found
            int meetingId = GetMeetingID(protocol.Meeting);

            if (meetingId == -1)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                    {
                        OpenConnection(connection);

                        string query = "SELECT protokoll_id FROM protokoll " +
                            "WHERE meeting_id = @meetingId";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.Add("@meetingId", SqlDbType.Int).Value = meetingId;

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    protocolId = Convert.ToInt32(reader["protokoll_id"]);
                                }
                                reader.Close();
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
            }
            return protocolId;
        }

        private static bool InsertProtocolTopicData(ProtocolTopic topic)
        {
            string headline = topic.Headline;
            List<string> contentAsList = topic.Content;
            string contentAsString = topic.GetContentAsString(contentAsList);
            int protocolID = GetProtocolID(topic.AssociatedProtocol);


            string insertQuery = "INSERT INTO protokollthema (ueberschrift, protokoll_id, inhalt) "
                    + "VALUES (@headline, @protocolID, @content)";

            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    OpenConnection(connection);

                    SqlCommand command = new SqlCommand(insertQuery, connection);

                    command.Parameters.AddWithValue("@headline", headline);
                    command.Parameters.AddWithValue("@protocolID", protocolID);
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

            return false;
        }

        private static bool InsertAttendanceData()
        {
            return false;
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
