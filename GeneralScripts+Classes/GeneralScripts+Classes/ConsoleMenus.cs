using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralScripts_Classes
{
    internal class ConsoleMenus
    {
        static List<string> createLangSelectList(Dictionary<int, Languages> langs)
        {
            //creates a list of the availabele Model(Languages in this example),
            //takes the first 2 Letters and the index+1 to add a menu choice to a list:
            //e.g.  "Fr or 1 to access French"
            //then returns that list to be displayed

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
        List<string> mainMenuText = new List<string>()
        //custom Menu list
                {
                "L or 1 to Manage Languages",
                "F or 2 to Manage Flashcards",
                "B or 3 to Beginn a Study session",
                "D or 4 to view study session Data"
        };


        public static int CreateMenuFromList(List<string> menue, string header)
        {
            //Creates a Menu from a list.
            //Index 0 is entered as a blank string to have the menue start at Index 1
            //the try catch block tries to convert the input to a absolute number.
            //if it cant it compares the input as string to the first 5 characters of the Option string.
            
            //returns the chosen index +1 of the input List as int

            int userInput = 900;
            var paddedMenue = new List<string>(menue);
            Console.WriteLine("\n" + header + "\n");
            Console.WriteLine("------------------\n");
            foreach (string option in paddedMenue)
            {
                Console.WriteLine(" " + option);
            }
            Console.WriteLine("\n------------------");
            Console.WriteLine("\nInput your selection \nOr 0 to exit");
            paddedMenue.Insert(0, " ");
            while (!(userInput >= 0 && userInput < paddedMenue.Count))
            {
                string input = Console.ReadLine();
                try
                {
                    userInput = Math.Abs(Convert.ToInt32(input));
                }
                catch (Exception)
                {
                    userInput = paddedMenue.IndexOf(input);
                    if (userInput == -1)
                    {
                        foreach (string option in paddedMenue.Skip(1))
                        {
                            if (option.Substring(0, 5).Contains(input))
                            {
                                userInput = paddedMenue.IndexOf(option);
                                break;
                            }
                            userInput = 9999;
                        }
                    }
                }
                if (userInput > (paddedMenue.Count - 1) || userInput < 0)
                {
                    Console.WriteLine("Invalid Input. Try again:");
                }
            }

            return userInput;
        }


        static void MainMenue()
        {
            ///the Main Logic to create Menus
            ///sets the input to a high number,
            ///prints the menu and gets input with the menu function
            ///then compares  it with a switch statment
            ///Breaks on 0
            
            var mainMenueInput = 9000;
            while (mainMenueInput != 0)
            {
                var tableData = new List<List<object>>();
                mainMenueInput = menu(mainMenu, "What would you like to do?");
                switch (mainMenueInput)
                {
                    case 1:
                        Console.Clear();
                        //do the thing for menu option 1
                        break;
                    case 2:
                        Console.Clear();
                        //do the thing for menu option 2
                        break;
                    //etc
                }
            }
        }
        public static void createTable(Dictionary<int, CodingSession> data, string title)
        {
            ///creates and prints a Table from a dict 
            ///needs Nugget "ConsoleTableExt"
            List<string> rows = new List<string>()
            { "ID", "Start Date","End Date","Duration"};
            var tableData = new List<List<object>> { };
            foreach (var ses in data)
            {
                tableData.Add(ses.Value.expData());
            }
            ConsoleTableBuilder.From(tableData)
                            .WithTextAlignment(new Dictionary<int, TextAligntment> {
                    { 0, TextAligntment.Center },
                    { 1, TextAligntment.Center },
                    { 2, TextAligntment.Center },
                    { 3, TextAligntment.Center }
                            })
                            .WithMinLength(new Dictionary<int, int> {
                    { 0, 5 },
                    { 1, 25 },
                    { 2, 25 },
                    { 3, 10 },
                            })

                            .WithTitle(title.ToUpper(), ConsoleColor.DarkYellow, ConsoleColor.DarkRed)
                            .WithCharMapDefinition(CharMapDefinition.FrameDoublePipDefinition)
                            .WithFormatter(3, (text) =>
                            {
                                if (string.IsNullOrEmpty(text) || text.Trim().Length == 0)
                                {
                                    return "0 h";
                                }
                                else
                                {
                                    return text + " h";
                                }
                            })
                            .WithColumn(rows)
                            //.WithColumnFormatter(0, (text) => "ID")
                            //.WithColumnFormatter(1, (text) => "Start Date")
                            //.WithColumnFormatter(2, (text) => "End Date")
                            //.WithColumnFormatter(3, (text) => "Duration")
                            .ExportAndWriteLine();
        }
    }
}
