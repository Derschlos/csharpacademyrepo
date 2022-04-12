using Microsoft.Data.Sqlite;

namespace Variables
{
    class Program
    {
        static void Main(string[] args)
        {
            //creates the database if not allready existing
            //const string connectionString = @"Data Source=.\\habit-Tracker.db";
            //var con = new SqliteConnection(connectionString);
            //using (con)
            //{
            //    var tableCmd = con.CreateCommand();
            //    using (tableCmd) 
            //    {
            //        tableCmd.CommandText =
            //           @"CREATE TABLE IF NOT EXISTS yourHabit (
            //                Id INTEGER PRIMARY KEY AUTOINCREMENT,
            //               Date TEXT,
            //                Quantity INTEGER
            //                )";
            //        con.Open();
            //        tableCmd.ExecuteNonQuery();
            //        con.Close();
            //    }
            //}
            var commandText = @"CREATE TABLE IF NOT EXISTS yourHabit (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Date TEXT,
                            Quantity INTEGER
                            )";

            executeSql(commandText, "r");
            static byte userInputGenerator()
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine(" ");
                Console.WriteLine("Type 1 to Close Application");
                Console.WriteLine("Type 2 to view All Records");
                Console.WriteLine("Type 3 to Insert Record");
                Console.WriteLine("Type 4 to Delete Record");
                Console.WriteLine("Type 5 to Update Record");
                byte userInput = 0;
                while (userInput < 1 || userInput > 5)
                {
                    try
                    {
                        userInput = Convert.ToByte(Console.ReadLine());

                    }
                    catch (Exception)
                    {

                        Console.WriteLine("Bitte eine Zahl eingeben");
                    }
                }
                return userInput;
            }

            static void executeSql(string inputString, string readWrite)
            {
                const string connectionString = @"Data Source=.\\habit-Tracker.db";
                //var outputString = "";
                var con = new SqliteConnection(connectionString);
                using (con)
                {
                    var tableCmd = con.CreateCommand();
                    using (tableCmd)
                    {
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
                                        
                                    }   
                                    break;
                                 
                                }
                        };
                        //else 
                        //{
                        //    tableCmd.ExecuteNonQuery();
                        //};
                        //con.Close();
                    }
                }
            }

            static void insert() 
            {

            }

            byte userInput = 0;
            var inputString = "";
            while ((userInput = userInputGenerator()) != 1)
            {
                switch (userInput)
                {
                    case 1:
                        Console.WriteLine("Haben Sie noch einen schönen Tag.");
                        System.Environment.Exit(0);
                        break;
                    case 2:
                        Console.WriteLine("in 2");
                        //var inputString = 
                        break;

                    case 3:
                        //Console.WriteLine("in 3");
                        inputString = @"SELECT MAX(ID) FROM yourHabit";
                        executeSql(inputString, "r");
                        //inputString = @"INSERT INTO yourHabit (ID, Date, Quantity)
                        //                VALUES ('2', '03.03.2022', '2')";
                        //executeSql(inputString, "w");
                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                    default:
                        userInputGenerator();
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


