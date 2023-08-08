﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            if(InsertMeetingData(meeting) && InsertProtocolData(protocol, GetLastInsertedMeetingId())
                && InsertProtocolTopicData(topic, GetLastInsertedProtocolId()))
            {
                return true;
            } 
            else
            {
                return false;
            }
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
            DateTime meetingDate = protocol.MeetingDate;
            DateTime createDate = protocol.CreateDate;

            if(meetingId != -1) {
                string insertQuery = "INSERT INTO protokoll (meeting_id) "
                        + "VALUES (@meetingID)";

                try
                {
                    using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                    {
                        OpenConnection(connection);

                        SqlCommand command = new SqlCommand(insertQuery, connection);

                        SqlCommand insertProtocolDataCommand = new SqlCommand(insertQuery, connection);

                        insertProtocolDataCommand.Parameters.AddWithValue("@meetingID", meetingId);
                        //insertProtocolDataCommand.Parameters.AddWithValue("@meetingDate", protocol.MeetingDate);
                        //insertProtocolDataCommand.Parameters.AddWithValue("@createDate", protocol.CreateDate);

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
            string headline = topic.Headline;
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

                    command.Parameters.AddWithValue("@headline", headline);
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

            return false;
        }

        private static bool InsertAttendanceData()
        {
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
