using System;
using System.Data;
using System.Globalization;
using System.IO;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic.FileIO;

class Program
{
    static string connectionString = @"Data Source=inflammation-score.db";
    static void Main()
    {
       
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

        GetUserInput();
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

            switch (command)
            {
                case "0":
                    Console.WriteLine("\nSee you next time!!\n");
                    closeApp = true;
                    Environment.Exit(0);
                    break;
                
                case "1":
                    GetAllStats();
                    break;
                
                case "2":
                    Insert();
                    break;

                case "3":
                    Delete();
                    break;

                case "4":
                   Update();
                   break;
                
                default:
                    Console.WriteLine("\nInvalid Command :(\n");
                    break;
            }

        }
    }
    
    private static void GetAllStats()
    {
        Console.Clear();
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                $"SELECT * FROM nutrition_logs;";

            List<NutritionLog> tableData = new();

            SqliteDataReader reader = tableCmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var s = reader.GetString(1);
                    string[] formats = { "yyyy-MM-dd", "dd-MM-yy", "dd-MM-yyyy" };

                    DateTime dt = DateTime.ParseExact(s, formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
                    tableData.Add(new NutritionLog()
                    {
                        Id = reader.GetInt32(0),
                        Date = dt,
                        Quantity = reader.GetInt32(2)
                    }); ;
                }
            }
            else
            {
                Console.WriteLine("\nNo data found :(\n");
            }

            connection.Close();

            Console.WriteLine("-----------------------------------------------------------\n");
            foreach (var log in tableData)
            {
                Console.WriteLine($"ID: {log.Id} | Date: {log.Date.ToString("dd-MM-yyyy")} | Quantity: {log.Quantity}");
            }
            Console.WriteLine("-----------------------------------------------------------\n");
        }
    }
    private static void Insert()
    {
        string date = GetDateInput();

        int quantity = GetNumberInput("\n\nPlease insert an amount of your messured inflammaitorial food (only integers for now :c)\n\n");

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open(); 
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                tableCmd.CommandText =
                $"INSERT INTO nutrition_logs (Date, Quantity) VALUES ('{date}', {quantity});";


            tableCmd.ExecuteNonQuery();

            connection.Close();
        }
    }

    private static void Delete()
    {    
        Console.Clear();
        GetAllStats();

        var statId = GetNumberInput("\n\nPlease insert the ID of the entry you want to delete or type 0 to go back to the Menu :)\n\n");
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                tableCmd.CommandText =
                $"DELETE FROM nutrition_logs WHERE Id = '{statId}'";

            int rowCount = tableCmd.ExecuteNonQuery();

            if (rowCount == 0)
            {
                Console.WriteLine($"\nNo entry with the Id {statId} found. :(\n");
                Delete();
            }
        }

        Console.WriteLine($"\nEntry with the Id {statId} was deleted successfully! :)\n");

        GetUserInput();
    }

    internal static void Update()
    {
        Console.Clear();
        GetAllStats();

        var statId = GetNumberInput("\n\nPlease insert the ID of the entry you want to update or type 0 to go back to the Menu :)\n\n");

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var checkCmd = connection.CreateCommand();
            checkCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM nutrition_logs WHERE Id = '{statId}');";
            int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (checkQuery == 0)
            {
                Console.WriteLine($"\nNo entry with the Id {statId} found. :(\n");
                connection.Close();
                Update();
            }

            string date = GetDateInput();

            int quantity = GetNumberInput("\n\nPlease insert an amount of your messured inflammaitorial food (only integers for now :c)\n\n");

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =               
                $"UPDATE nutrition_logs SET Date = '{date}', Quantity = {quantity} WHERE Id = '{statId}'";

            tableCmd.ExecuteNonQuery();

            connection.Close();
        }
    }
    internal static string GetDateInput()
    {
        Console.WriteLine("\n\nPlease isnert a date in the format of (dd-mm-yy) :). In order to return to the main menu type 0.");

        string DateInput = Console.ReadLine();

        if (DateInput == "0") GetUserInput();

        while (!DateTime.TryParseExact(DateInput, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
        {
            Console.WriteLine("\nInvalid date format. Please use (dd-mm-yy) format.\n");
            DateInput = Console.ReadLine();
        }

        return DateInput;
    }

    internal static int GetNumberInput(string message)
    {
        Console.WriteLine(message);

        string numberInput = Console.ReadLine();

        if (numberInput == "0") GetUserInput();

        while (!Int32.TryParse(numberInput, out _) || Convert.ToInt32(numberInput) < 0)
        {
            Console.WriteLine("\nInvalid number. Please try agian!\n");
            numberInput = Console.ReadLine();
        }

        int finalInput = Convert.ToInt32(numberInput);

        return finalInput;
    }
}



internal class NutritionLog
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int Quantity { get; set; }
}


