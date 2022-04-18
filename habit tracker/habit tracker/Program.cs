using Microsoft.Data.Sqlite;
using System.Globalization;
using System.Text.RegularExpressions;
namespace Variables
{
    class Program
    {
        static int userInputGenerator(int userInput)
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine(" ");
            Console.WriteLine("Type 0 to Close Application");
            Console.WriteLine("Type 1 to view All Records");
            Console.WriteLine("Type 2 to Insert Record");
            Console.WriteLine("Type 3 to Delete Record");
            Console.WriteLine("Type 4 to Update Record");
            userInput = -1;
            while (userInput < 0 || userInput > 4)
            {
                try
                {
                    userInput = Convert.ToInt32(Console.ReadLine());

                }
                catch (Exception)
                {

                    Console.WriteLine("Bitte eine Zahl eingeben");
                }
            }
            return userInput;
        }
        static List<String> executeSql(string inputString, string readWrite)
        {
            const string connectionString = @"Data Source=.\\habit-Tracker.db";
            var con = new SqliteConnection(connectionString);
            using (con)
            {
                var tableCmd = con.CreateCommand();
                using (tableCmd)
                {
                    List<String> rowData = new List<string>();
                    tableCmd.CommandText = inputString;
                    con.Open();
                    switch (readWrite)
                    {
                        case "w":
                            {
                                tableCmd.ExecuteNonQuery();
                                break;
                            }
                        case "r":
                            {
                                var reader = tableCmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    object[] row = new object[3];
                                    reader.GetValues(row);
                                    foreach (var val in row)
                                        rowData.Add(Convert.ToString(val));
                                }
                                break;

                            }
                    };
                    return rowData;
                }
            }
        }
        static String checkRegex(String pattern, String uInput)
        {
            Regex rgx = new Regex(pattern);
            MatchCollection matches = rgx.Matches(uInput);
            if (matches.Count > 0)
            {
                return matches[0].Value;
            }
            else
            {
                return null;
            }
        }
        static int maxId()
        {
            String inputString = @"SELECT MAX(ID) FROM yourHabit";
            var outputObject = executeSql(inputString, "r");
            return Convert.ToInt32(outputObject[0]);
        }
        static String getDate()   
        {
            String date = DateTime.Now.ToShortDateString();
            Console.WriteLine("Current Date is: " + date);
            Console.WriteLine("Do you want to change the Date? (y/n)");
            string changeDate = Console.ReadLine();
            string breakChain = "break";
           
            while (changeDate == "y")
            {
                Console.WriteLine("Please write the Date as dd.mm.yyyy:");
                date = Console.ReadLine();
                if (date.Contains(breakChain))
                {
                    date = DateTime.Now.ToShortDateString();
                    break;
                }
                String day = "";
                String month = "";
                String year = "";
                if (date.Length == 10)
                {
                    string pattern = @"\d\d(?=[.,/ \-]+\d\d[.,/ \\-]+\d{4})";
                    day = checkRegex(pattern, date);
                    pattern = @"(?<=[.,/\-])\d\d(?=[.,/\-]+\d{4})";
                    month = checkRegex(pattern, date);
                    pattern = @"(?<=[.,/\\-])\d{4}";
                    year = checkRegex(pattern, date);   
                    date = $"{day}.{month}.{year}";
                    if (Convert.ToInt32(day) <= 31 && Convert.ToInt32(month) <= 12)
                    {
                        break;
                    }
                    Console.WriteLine("Faulty input. Type (break) if you want to stop Input and use today as date.");
                }
            }
            Console.Clear();
            return date;
        }
        static string getQuant()
        {
            Console.WriteLine("Please Input the Quantity of the task");
            string uInput = Console.ReadLine();
            string quant = checkRegex(@"\d+", uInput);
            if (quant == null)
            {
                Console.WriteLine("Ivalid Input");
                return getQuant();
            }
            Console.Clear();
            return quant;
        }
        static void showAll()
        {
            var inputString = @"Select * FROM yourHabit";
            var outputObject = executeSql(inputString, "r");
            Console.WriteLine("ID       Date        Count");
            for (int i = 0; i < outputObject.Count(); i += 3)
            {
                Console.WriteLine($"{outputObject[i]}       {outputObject[i + 1]}     {outputObject[i + 2]}");
            }
        }
        static void Main(string[] args)
        {
            var commandText = @"CREATE TABLE IF NOT EXISTS yourHabit (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Date TEXT,
                            Quantity INTEGER
                            )";
            var outputObject = executeSql(commandText, "r");
            int userInput = -1;
            var inputString = "";
            while ((userInput = userInputGenerator(userInput)) != 0)
            {
                switch (userInput)
                {
                    case 0:
                        //Exit
                        Console.Clear();
                        Console.WriteLine("Haben Sie noch einen schönen Tag.");
                        System.Environment.Exit(0);
                        break;
                    case 1:
                        //View ALL
                        Console.Clear();
                        showAll();
                        break;

                    case 2:
                        //Insert Record
                        Console.Clear();
                        var date = getDate();
                        var quan = getQuant();
                        var index = Convert.ToString(maxId() + 1);
                        inputString = @$"INSERT INTO yourHabit ( Date, Quantity)
                                         VALUES ( '{date}', '{quan}')";
                        Console.WriteLine($"Inserting {quan} of habit on the {date} at index {index}");
                        outputObject = executeSql(inputString, "w");
                        break;
                    case 3:
                        //Delete Record
                        Console.Clear();
                        showAll();
                        Console.WriteLine("Input ID to delete:");
                        string delInput = Console.ReadLine();
                        delInput = checkRegex(@"\d+", delInput);
                        inputString = $"DELETE FROM yourHabit WHERE Id = {delInput}";
                        executeSql(inputString, "w");
                        showAll();
                        break;
                    case 4:
                        //Update Record
                        Console.Clear();
                        showAll();
                        Console.WriteLine("Which Id would you like to update?");
                        string upInput = Console.ReadLine();
                        upInput = checkRegex(@"\d+", upInput);
                        Console.WriteLine("Input the new amount");
                        string quantInput = Console.ReadLine();
                        quantInput = checkRegex(@"\d+", quantInput);
                        inputString = $"UPDATE yourHabit SET Quantity = {quantInput} WHERE Id= {upInput}";
                        executeSql(inputString, "w");
                        break;
                    default:
                        //Loop
                        Console.Clear();
                        userInputGenerator(userInput);
                        break;
                }
            }
        }
    }
}