using System;
using System.IO;
using Microsoft.Data.Sqlite;

class Program
{
    static void Main()
    {
        string connectionString = @"Data Source=inflammation-score.db";

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            // Tabelle erstellen, falls nicht vorhanden
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS nutrition_logs (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Date TEXT,
                    Quantity INTEGER
                )";
            tableCmd.ExecuteNonQuery();
        }
    }
    
    static void GetUserInput()
    {
        Console.Clear();
        bool closeApp = false;
        while (closeApp == false)
        {
            Console.WriteLine("\n\nMAIN MENU");
            Console.WriteLine("\n Hello my firends, please take care of your healths :)");
            Console.WriteLine("\n What do you want to do?");
            Console.WriteLine("Type 1 to View all stats.");
            Console.WriteLine("Type 2 to Insert a stat.");
            Console.WriteLine("Type 3 to Delete stat.");
            Console.WriteLine("Type 4 to Upadate stat.");
            Console.WriteLine("\nType 0 Close Application");
            Console.WriteLine("------------------------------------------------------------\n");
            
            string command = Console.ReadLine();

        }
    }

}


