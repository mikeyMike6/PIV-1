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
            Crud.Read(conString);
            Crud.Create(conString);
            Crud.Update(conString);
            Crud.Delete(conString);
        }
    }
}
