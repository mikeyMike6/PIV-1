using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;


//Stwórz prostą aplikację konsolową. 
//Podłącz do niej dowolną bazę danych, (np. Northwind) przy pomocy
//ADO.NET i dodaj funkcjonalność CRUD na dowolnej tabeli.
//Najpierw wyświetl listę danych z tej tabeli, następnie pozwól dodać nowy wpis, 
//pozwól go edytować a na koniec usuń go.

namespace PIV_1
{
    class Program
    {
        static void Main()
        {
            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder
            {
                DataSource = @"HAL\MSSERVER",
                InitialCatalog = "ZNorthwind",
                IntegratedSecurity = true,
                ConnectTimeout = 30,
                Encrypt = false,
                TrustServerCertificate = false,
                ApplicationIntent = 0,
                MultiSubnetFailover = false
            };
            var conString = connectionString.ConnectionString;
            Read(conString);
            Create(conString);
            Update(conString);
            Delete(conString);
        }
        static void Read(string connectionString) //odczytaj pracownikow
        {
            var commandText = "SELECT * FROM dbo.Pracownicy";
            SqlHelper.ExecuteNonQuery(connectionString, commandText);
            SqlHelper.ShowTable(connectionString);
        }
        static void Create(string connectionString)
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
        static void Update(string connectionString)
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
        static void Delete(string connectionString)
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
