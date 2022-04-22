using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Data.Sqlite;
using System.Globalization;
using System.Text.RegularExpressions;
using codeTimeTracker;

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

CodingSession currentSession = new CodingSession();

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
