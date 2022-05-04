using System.Globalization;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using flashcards;


namespace flashcards
{

    class Program
    {
        
        static void Main(string[] args)
        {
            List<string> mainMenu = new List<string>()
            {
            "E or 0 to close the application\n",
            "A or 1 to view all records",
            "S or 2 to start the counter",
            "End or 3 to end the counter",
            "Save or 4 to save the current session\n",
            "Type 5 to manually change dates",
            "Type 6 to view the current session"
            };
            var  a = UserInput.menu(mainMenu, "What would you like to do?");
            SqlDriver.initialize();
            Console.WriteLine(a);


        }
    }
}