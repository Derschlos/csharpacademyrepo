using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Data.Sqlite;
using System.Globalization;
using System.Text.RegularExpressions;
using codeTimeTracker;
using ConsoleTableExt;

//UserInput uIn = new UserInput();
//SqlDriver sql = new SqlDriver();



static void createTable(Dictionary<int, CodingSession> data, string title)
{
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
                    .WithColumnFormatter(0, (text) => "ID")
                    .WithColumnFormatter(1, (text) => "Start Date")
                    .WithColumnFormatter(2, (text) => "End Date")
                    .WithColumnFormatter(3, (text) => "Duration")
                    .ExportAndWriteLine();
}



string sqlIn = @"SELECT MAX(id) FROM codeTrack";
var maxId = SqlDriver.executeSql(sqlIn, "r");
CodingSession currSession = new CodingSession(Convert.ToInt32(maxId[0]), null, null, null);

List<string> mainMenu = new List<string>()
    {
        "Type 0 to close the application",
        "\nType 1 to view all records",
        "Type 2 to start the counter",
        "Type 3 to end the counter",
        "Type 4 to save the current session",
        "\nType 5 to manually change dates"
    };

int userInput = 800;
while (userInput != 0)
{
    userInput = UserInput.menu(mainMenu);
    Console.Clear();

    switch (userInput)
    {
        case 1:
            sqlIn = @$"SELECT * FROM codeTrack";
            var sqlOut = SqlDriver.getSessions(sqlIn);
            createTable(sqlOut, "all dates");
            break;
        case 2:
            UserInput.cwWrap("Starting the counter");
            currSession.startCounter();
            break;
        case 3:
            UserInput.cwWrap("Stoping the counter and evaluating data");
            currSession.endCounter();
            break;
        case 4:
            UserInput.cwWrap(currSession.insRowSql(false));
            break;
        case 5:
            Console.Clear();
            List<string> manualMenu = new List<string>()
            {
                "Type 0 to return to main menu\n",
                "Type 1 to insert a session",
                "Type 2 to edit a session",
                "Type 3 to delete a session"    
            };
            userInput = UserInput.menu(manualMenu);
            while (userInput != 0)
            {
                switch(userInput)
                {
                    case 1:
                        sqlIn = @$"SELECT * FROM codeTrack";
                        sqlOut = SqlDriver.getSessions(sqlIn);
                        createTable(sqlOut, "all dates");
                        var manualSess = UserInput.createCustomSession("insert");

                        break;
                }
            }
            userInput = 900;
            break;
    }
}


