using System.Globalization;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using flashcards;
using System.IO;



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
                    "A or 1 to Add a new language",
                    "D or 2 to Delete a language",
                    "R or 3 to Rename a language",
                    "I or 4 to Import a Language from a prepacked file"
                };
            List<string> manageCardsMenu = new List<string>()
                {
                    "A or 1 to view all Cards",
                    "N or 2 to create a new Card",
                    "E or 3 to edit a Card",
                    "D or 4 to delete a Card"
                };
            //ReadLangDb a = new ReadLangDb("French");
            //Console.WriteLine(a.cards.Count());
            //foreach (var card in a.cards)
            //{
            //    Console.WriteLine(card.Value.Front);
            //}

            SqlDriver.initialize();
            var langs = SqlDriver.getAllLanguages();
            Languages currentLanguage;
            var mainMenueInput = 9000;
            while (mainMenueInput != 0)
            {
                var tableData = new List<List<object>>();
                mainMenueInput = UserInput.menu(mainMenu, "What would you like to do?");
                switch (mainMenueInput)
                {
                    case 1: //manage Langs
                        Console.Clear();
                        tableData = new List<List<object>>();
                        langs = SqlDriver.getAllLanguages();
                        foreach (var lang in langs.Values)
                        {
                            tableData.Add(lang.expData());
                        }
                        SqlDriver.createTable(tableData, "All Languages", langHeaders);
                        int manageLangMenuInput = 99999999;
                        while (manageLangMenuInput != 0)
                        {
                            manageLangMenuInput = UserInput.menu(manageLangMenu, "What would you like to do?");
                            switch (manageLangMenuInput)
                            {
                                case 1:
                                    break;
                                case 2:
                                    break;
                                case 3:
                                    break;
                                case 4: //Import a Language
                                    string[] files = Directory.GetFiles(@".\", "*.prepackedlang");
                                    List<string> prepakedFileMenu = new List<string>();
                                    for (int i = 0; i < files.Length; i++)
                                    {
                                        prepakedFileMenu.Add($"{Path.GetFileName(files[i])[0..2]} or {i + 1} to access {Path.GetFileName(files[i])}");
                                    }
                                    int fselect = UserInput.menu(prepakedFileMenu, "select a File");
                                    if (fselect <= 0)
                                    {
                                        break;
                                    }
                                    Console.WriteLine(files[fselect - 1]);
                                    var importedLang = new ImportLangDb(files[fselect - 1][2..]);
                                    SqlDriver.importDbToServer(importedLang);
                                    break;

                            }
                        }
                        break;
                    case 2:// manage Cards
                        Console.Clear();

                        foreach (var lang in langs.Values)
                        {
                            tableData.Add(lang.expData());
                        }
                        SqlDriver.createTable(tableData, "All Languages", langHeaders);
                        var langInput = UserInput.menu(createLangSelectMenu(langs), "For which language would you like to manage the cards?");
                        if (langInput == 0)
                        {
                            break;
                        }
                        int manageCardsMenuInput = 9899;
                        while (manageCardsMenuInput != 0)
                        {
                            Console.Clear();
                            manageCardsMenuInput = UserInput.menu(manageCardsMenu, $"You selected { langs.ElementAt(langInput - 1).Value.Name}");

                        }
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