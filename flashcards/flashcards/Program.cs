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
            List<string> langHeaders = new List<string>()
            {
                "Id",
                "Name",
                "# of Cards"
            };
            List<string> mainMenu = new List<string>()
            {
            "L or 1 to Manage Languages",
            "F or 2 to Manage Flashcards",
            "B or 3 to Beginn a Study session",
            "D or 4 to view study session Data"
            };
            SqlDriver.initialize();
               //SqlDriver.executeSql("INSERT INTO languages(id,name, langId) VALUES(2,'Spanish', 2)", "w");
            SqlDriver.createTable(SqlDriver.executeSql("SELECT id,name FROM languages", "r"), "title", langHeaders);
            var menueInput = 9000;
            while (menueInput != 0)
            {
                UserInput.menu(mainMenu, "What would you like to do?");
                switch (menueInput)
                {
                    case 1:
                        var a = new Languages(1, "Spanish");
                        
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                }
            }


        }
    }
}