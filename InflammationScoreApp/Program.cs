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
}
