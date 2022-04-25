using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Data.Sqlite;
using System.Globalization;
using System.Text.RegularExpressions;
using codeTimeTracker;
using ConsoleTableExt;

//UserInput uIn = new UserInput();
SqlDriver sql = new SqlDriver();



List<string> mainMenu = new List<string>()
{
    "Type 0 to close the application",
    "Type 1 to view all records",
    "Type 2 to start the counter",
    "Type 3 to end the counter",
    "Type 4 to manually change dates"
};

int userInput = 800;


static void createTable(List<List<object>> data, string title)
{
    ConsoleTableBuilder.From(data)
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
                    .WithColumnFormatter(0, (text) => "ID")
                    .WithColumnFormatter(1, (text) => "Start Date")
                    .WithColumnFormatter(2, (text) => "End Date")
                    .WithColumnFormatter(3, (text) => "Duration")
                    .ExportAndWriteLine();
}

//CodingSession session = new CodingSession();
//session.Id = 80;
//session.Start = "19.04.2022 15:30";
//session.End = "19.04.2022 19:00";

//CodingSession currentSession = new CodingSession();
//currentSession.Id = 5;
//currentSession.Start = "20.04.2022 11:00";
//currentSession.End = "20.04.2022 18:00";



List<CodingSession> sessionList = new List<CodingSession>();
//{
//    session,
//    currentSession
//};

//foreach(CodingSession s in sessionList)
//{
//    s.durationEval();
//    string a = @$"INSERT INTO codeTrack ( startDate, endDate , duration)
//                                         VALUES ( '{s.Start}', '{s.End}', '{s.Duration}' )";
//    SqlDriver.executeSql(a,"w");
//}
List<object> se = new List<object>();
string a = @$"SELECT * FROM codeTrack";
se.Add(SqlDriver.executeSql(a,"r"));

while (userInput != 0)
{
    userInput = UserInput.menu(mainMenu);
    Console.Clear();
    var tableData = new List<List<object>>{};

    //foreach (var ses in sessionList)
    //    tableData.Add(ses.expData());

    switch (userInput)
    {
        case 1:
            createTable(tableData, "all dates");
            break;
        case 2:

            break;
        case 3:
            break;
        case 4:
            List<string> manualMenu = new List<string>()
            {
                "Type 0 to return to main menu",
                "Type 1 to edit a ",
                "Type 2 to end the counter",
                "Type 3 to manually change dates"
            };

            break;
    }
}


