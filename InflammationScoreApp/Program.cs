using System;
using System.Data;
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
                
                /*case "1":
                    GetAllStats();
                    break;*/
                
                case "2":
                    Insert();
                    break;

                /*case "3":
                    Delete();
                    break;

                case "4":
                    Update();
                    break;
                
                default:
                    Console.WriteLine("\nInvalid Comand :(\n");
                    break;*/
            }

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

    internal static string GetDateInput()
    {
        Console.WriteLine("\n\nPlease isnert a date in the format of (dd-mm-yy) :). In order to return to the main menu type 0.");

        string DateInput = Console.ReadLine();

        if (DateInput == "0") GetUserInput();

        return DateInput;
    }

    internal static int GetNumberInput(string message)
    {
        Console.WriteLine(message);

        string numberInput = Console.ReadLine();

        if (numberInput == "0") GetUserInput();

        int finalInput = Convert.ToInt32(numberInput);

        return finalInput;
    }
}


