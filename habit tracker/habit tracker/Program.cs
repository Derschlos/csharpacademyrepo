using Microsoft.Data.Sqlite;

namespace Variables
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = @"Data Source=.\\habit-Tracker.db";

            /*Creating a connection passing the connection string as an argument
            This will create the database for you, there's no need to manually create it.
            And no need to use File.Create().*/
            using (var connection = new SqliteConnection(connectionString))
            {
                var tableCmd = connection.CreateCommand();
                //Creating the command that will be sent to the database
                using (tableCmd);
                {
                    //Declaring what is that command (in SQL syntax)
                    tableCmd.CommandText =
                        @"CREATE TABLE IF NOT EXISTS yourHabit (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Date TEXT,
                            Quantity INTEGER
                            )";
                    connection.Open();
                    // Executing the command, which isn't a query, it's not asking to return data from the database.
                    tableCmd.ExecuteNonQuery();
                    connection.Close();

                }
                // We don't need to close the connection or the command. The 'using statement' does that for us.
            }

            /* Once we check if the database exists and create it (or not),
            we will call the next method, which will handle the user's input.*/
            //GetUserInput();

        }
    }
}


