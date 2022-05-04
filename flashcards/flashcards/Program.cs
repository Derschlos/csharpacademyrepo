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
            "S or 1 to Manage Stacks",
            "F or 2 to Manage Flashcards",
            "B or 3 to Beginn a Study session",
            "D or 4 to view study session Data"
            };
            var  a = UserInput.menu(mainMenu, "What would you like to do?");
            SqlDriver.initialize();
            Console.WriteLine(a);


        }
    }
}