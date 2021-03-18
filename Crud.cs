using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace PIV_1
{
    static class Crud
    {

        static public void Read(string connectionString) //odczytaj pracownikow
        {
            SqlHelper.ShowTable(connectionString);
        }
        static public void Create(string connectionString)
        {
            var nextID = SqlHelper.GetHighestID(connectionString);
            var commandText = "INSERT INTO dbo.Pracownicy (IDpracownika, Nazwisko, Imię, Stanowisko) VALUES (@IDpracownika, @Nazwisko, @Imie, @Stanowisko)";

            //inicjalizacja zmiennych:
            Console.WriteLine("Podaj imie nowego pracownika: ");
            var fname = Console.ReadLine();
            Console.WriteLine("Podaj nazwisko pracownika: ");
            var lname = Console.ReadLine();
            Console.WriteLine("Podaj stanowisko pracownika: ");
            var position = Console.ReadLine();

            //inicjalizacja parametrow
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("IDpracownika", nextID + 1);
            parameters[1] = new SqlParameter("Imie", fname);
            parameters[2] = new SqlParameter("Nazwisko", lname);
            parameters[3] = new SqlParameter("Stanowisko", position);

            SqlHelper.ExecuteNonQuery(connectionString, commandText, parameters);
            SqlHelper.ShowTable(connectionString);
        }
        static public void Update(string connectionString)
        {
            var commandText = "UPDATE dbo.Pracownicy SET Stanowisko = @stanowisko WHERE Nazwisko = @nazwisko";

            Console.WriteLine("Podaj nazwisko pracownika ktorego stanowisko chcesz zmienic: ");
            var lname = Console.ReadLine();
            Console.WriteLine("Podaj jego nowe stanowisko pracy: ");
            var position = Console.ReadLine();
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("stanowisko", position);
            parameters[1] = new SqlParameter("nazwisko", lname);

            SqlHelper.ExecuteNonQuery(connectionString, commandText, parameters);
            SqlHelper.ShowTable(connectionString);
        }
        static public void Delete(string connectionString)
        {
            var commandText = "DELETE FROM dbo.Pracownicy WHERE Nazwisko = @nazwisko";

            Console.WriteLine("Podaj nazwisko pracownika do elimacji: ");
            var lname = Console.ReadLine();
            var parameter = new SqlParameter("nazwisko", lname);

            SqlHelper.ExecuteNonQuery(connectionString, commandText, parameter);
            SqlHelper.ShowTable(connectionString);
        }
    }
}
