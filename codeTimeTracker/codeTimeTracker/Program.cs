using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Data.Sqlite;
using System.Globalization;
using System.Text.RegularExpressions;
using codeTimeTracker;
using ConsoleTableExt;

//UserInput uIn = new UserInput();
SqlDriver sql = new SqlDriver();


//string sAtt = ConfigurationManager.AppSettings.Get("databasePath");
//Console.WriteLine("between");
//Console.WriteLine(@"Data Source="+sAtt);
//Console.WriteLine("these");
//CodingSession.start();

//session.id = 1;
////session.first.set() = "a"
//session.Start = "19.04.2022 15:30";
//Console.WriteLine(session.Start);
//session.End = "20.04.2022 19:00";
//Console.WriteLine(session.End);
////session.durationEval();
//Console.WriteLine(session.Duration);

List<string> mainMenu = new List<string>()
{
    "Type 0 to close the application",
    "Type 1 to view all records",
    "Type 2 to start the counter",
    "Type 3 to end the counter",
    "Type 4 to manually change dates"
};

List<string> manualMenu = new List<string>()
{
    "Type 0 to return to main menu",
    "Type 1 to view all records",
    "Type 2 to start the counter",
    "Type 3 to end the counter",
    "Type 4 to manually change dates"
};
int userInput = 800;

var tableData = new List<List<object>>
{
    new List<object>{ "Sakura Yamamoto", "Support Engineer", "London", 46},
    new List<object>{ "Serge Baldwin", "Data Coordinator", "San Francisco", 28, "something else" },
    new List<object>{ "Shad Decker", "Regional Director", "Edinburgh"},
};

ConsoleTableBuilder.From(tableData)
                .WithTextAlignment(new Dictionary<int, TextAligntment> {
                    { 0, TextAligntment.Center },
                    { 1, TextAligntment.Center },
                    { 3, TextAligntment.Center },
                    { 4, TextAligntment.Center }
                })
                .WithMinLength(new Dictionary<int, int> {
                    { 1, 35 },
                    { 3, 10 },
                    { 4, 30 },
                })
                //.WithFormatter(2, (text) => {
                //    char[] chars = text.ToCharArray();
                //    Array.Reverse(chars);
                //    return new String(chars);
                //})
                .WithTitle("Hello, everyone! This is the LONGEST TEXT EVER! I was inspired by the various other 'longest texts ever' on the internet, and I wanted to make my own. So here it is!".ToUpper(), ConsoleColor.Yellow, ConsoleColor.DarkMagenta)
                .WithCharMapDefinition(CharMapDefinition.FrameDoublePipDefinition)
                .WithFormatter(3, (text) => {
                    if (string.IsNullOrEmpty(text) || text.Trim().Length == 0)
                    {
                        return "0 $";
                    }
                    else
                    {
                        return text + " $";
                    }
                })
                .WithColumnFormatter(3, (text) => "#")
                .ExportAndWriteLine();

//CodingSession currentSession = new CodingSession();

//ConsoleTableBuilder
//    .From(tableData)
//    .ExportAndWriteLine();
while (userInput != 0)
{
    userInput = UserInput.menu(mainMenu);
    switch (userInput)
    {
        case 1:

            break;
        case 2:

            break;
    }
}




//string b = uIn.timeInput();
//Console.WriteLine(b);
