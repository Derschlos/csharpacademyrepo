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
            static List<string> createLangSelectMenu(Dictionary<int, Languages> langs)
            {
                List<string> langSelectMenu = new List<string>();
                var i = 1;
                foreach (var lang in langs)
                {
                    langSelectMenu.Add($"{lang.Value.Name[0..2]} or {i} to access {lang.Value.Name} ");
                    i++;
                    //Console.WriteLine(lang.Key);
                }
                return langSelectMenu;
            }
            List<string> langHeaders = new List<string>()
            {
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
            List<string> manageLangMenu = new List<string>()
            {   
                "A or 1 to view all Cards",
                "N or 2 to create a new Card",
                "E or 3 to edit a Card",
                "D or 4 to delete a Card"
            };
            
            SqlDriver.initialize();
            var langs = SqlDriver.getAllLanguages();
            Languages currentLanguage;
            var menueInput = 9000;
            while (menueInput != 0)
            {
                menueInput = UserInput.menu(mainMenu, "What would you like to do?");
                switch (menueInput)
                {
                    case 1:
                        Console.Clear();

                        var tableData = new List<List<object>>();
                        foreach (var lang in langs.Values)
                        {
                            tableData.Add(lang.expData());
                        }
                        SqlDriver.createTable(tableData, "All Languages", langHeaders);
                        var langInput = UserInput.menu(createLangSelectMenu(langs), "Which Language would you like to manage?");
                        if (langInput == 0)
                        {
                            break;
                        }
                        
                        while (menueInput != 0)
                        {
                            Console.Clear();
                            menueInput = UserInput.menu(manageLangMenu, $"You selected { langs.ElementAt(langInput - 1).Value.Name}");

                        }
                        menueInput = 9999;
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        Console.WriteLine("d");
                        break;
                }
            }


        }
    }
}