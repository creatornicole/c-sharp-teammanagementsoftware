using System;
using System.Collections.Generic;
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

        public static bool InsertTeammemberData(string vorname, string nachname, string handynummer, string position, string abteilung,
            string bereich, string seminargruppe, string hskuerzel, DateTime geburtstag, DateTime eintrittsdatum)
        {
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

        public static void CloseConnection(SqlConnection connection)
        {
            try
            {
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error closing connection to database: " + ex.Message);
            }
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
