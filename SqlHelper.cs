using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace PIV_1
{
    static class SqlHelper
    {
        public static int GetHighestID(string connectionString)
        {
            var commandText = "SELECT MAX(IDpracownika) FROM dbo.Pracownicy";
            using var con = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(commandText, con);
            con.Open();
            using var reader = cmd.ExecuteReader();
            reader.Read();
            return reader.GetInt32(0);

        }
        public static void ShowTable(string connectionString)
        {
            var commandText = "SELECT * FROM dbo.Pracownicy";
            using var con = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(commandText, con);
            con.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader.GetInt32(0) + "\t" + reader.GetString(2) + " " +
                    reader.GetString(1) + "\t" + reader.GetString(3));
            }
        }
        //otwiera polaczenie z bd i wykonuje przeslana komende
        public static int ExecuteNonQuery(string connectionString, string commandText,
            params SqlParameter[] parameters)
        {
            using var con = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(commandText, con);

            cmd.Parameters.AddRange(parameters);
            con.Open(); //otwarcie polaczenia z baza

            return cmd.ExecuteNonQuery(); //wykonanie zapytania i zwrocenie ilosci zmodyfikowanych wierszy
        }
    }
}
