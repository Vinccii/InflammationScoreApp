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
            var tableCmd = connection.CreateCommand();
            
            tableCmd.CommandText = 
                @"CREATE TABLE IF NOT EXISTS nutrition_logs (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Date TEXT,
                    Quantity INTEGER
                )";
            tableCmd.ExecuteNonQuery();

            // 🔹 1. Insert Test-Datensatz
            var insertCmd = connection.CreateCommand();
            insertCmd.CommandText = @"INSERT INTO nutrition_logs (Date, Quantity)
                                      VALUES ($d, $q)";
            insertCmd.Parameters.AddWithValue("$d", DateTime.UtcNow.ToString("yyyy-MM-dd"));
            insertCmd.Parameters.AddWithValue("$q", 123);
            insertCmd.ExecuteNonQuery();

            // 🔹 2. Select alle Datensätze und ausgeben
            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = @"SELECT Id, Date, Quantity FROM nutrition_logs";

            using (var reader = selectCmd.ExecuteReader())
            {
                Console.WriteLine("------ nutrition_logs ------");
                while (reader.Read())
                {
                    Console.WriteLine($"Id={reader.GetInt32(0)}, Date={reader.GetString(1)}, Quantity={reader.GetInt32(2)}");
                }
            }
        }

        Console.WriteLine("Fertig! Datenbank funktioniert ✅");
        Console.ReadKey();
    }
}

