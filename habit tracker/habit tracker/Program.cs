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

        static void insert()
        {
            int i = 0;
            //Console.WriteLine("in 3");
            inputString = @"SELECT MAX(ID) FROM yourHabit";
            //inputString = @"SELECT * FROM yourHabit WHERE ID = 1";
            outputObject = executeSql(inputString, "r");
            var index = Convert.ToString(Convert.ToInt32(outputObject[0]) + 1);
            Console.WriteLine("Inserting new entry with ID: " + index);
            String date = DateTime.Now.ToShortDateString();
            Console.WriteLine("Current Date is: " + date);
            Console.WriteLine("Do you want to change the Date? (y/n)");
            string changeDate = Console.ReadLine();
            while (changeDate == "y")
            {
                String day = "";
                String month = "";
                String year = "";
                static String checkDayMonthYear(String pattern, String uInput) 
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
                Console.WriteLine("Please write the Date as dd.mm.yyyy:");
                date = Console.ReadLine();
                if (date.Length == 10)
                {
                    string pattern = @"\d\d[.-/ ]+";

                }

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
                        Console.WriteLine("Haben Sie noch einen schönen Tag.");
                        System.Environment.Exit(0);
                        break;
                    case 1:
                        //View ALL
                        Console.WriteLine("in 1");
                        //var inputString = 
                        break;

                    case 2:
                        //Insert Record
                        

                        //Console.WriteLine("Please write the Date as dd.mm.yyyy:");
                        //String date = Console.ReadLine();

                        Console.WriteLine(date);

                        inputString = String.Format(@"INSERT INTO yourHabit (ID, Date, Quantity)
                                        VALUES ('{0}', '03.03.2022', '{1}')", index , i + 1);
                        
                        //outputObject = executeSql(inputString, "w");
                        break;
                    case 3:
                        //Delete Record
                        break;
                    case 4:
                        //Update Record
                        break;
                    default:
                        //Loop
                        userInputGenerator(userInput);
                        break;
                }
            }
            


            //Console.WriteLine(userInput);
            

            ///*Creating a connection passing the connection string as an argument
            //This will create the database for you, there's no need to manually create it.
            //And no need to use File.Create().*/
            //using (var connection = new SqliteConnection(connectionString))
            //{
            //    var tableCmd = connection.CreateCommand();
            //    //Creating the command that will be sent to the database
            //    using (tableCmd);
            //    {
            //        //Declaring what is that command (in SQL syntax)
            //        tableCmd.CommandText =
            //            @"CREATE TABLE IF NOT EXISTS yourHabit (
            //                Id INTEGER PRIMARY KEY AUTOINCREMENT,
            //                Date TEXT,
            //                Quantity INTEGER
            //                )";
            //        connection.Open();
            //        // Executing the command, which isn't a query, it's not asking to return data from the database.
            //        tableCmd.ExecuteNonQuery();
            //        connection.Close();

            //    }
            //    // We don't need to close the connection or the command. The 'using statement' does that for us.
            //}

            ///* Once we check if the database exists and create it (or not),
            //we will call the next method, which will handle the user's input.*/
            ////GetUserInput();

        }
    }

}


