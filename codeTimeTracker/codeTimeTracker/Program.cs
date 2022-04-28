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

static void showAllDates()
{
    var sqlIn = @$"SELECT * FROM codeTrack";
    var sqlOut = SqlDriver.getSessions(sqlIn);
    createTable(sqlOut, "all dates");
}

int consoleInput = 800;
while (consoleInput != 0)
{
    
    consoleInput = UserInput.menu(mainMenu);
    Console.Clear();

    switch (consoleInput)
    {
        case 1:
            showAllDates();
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
            UserInput.cwWrap(SqlDriver.insRowSql(currSession,false));
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
            CodingSession manualSess;
            while (consoleInput != 0)
            {
                consoleInput = UserInput.menu(manualMenu);

                switch (consoleInput)
                {
                    case 1:
                        showAllDates();
                        manualSess = UserInput.createCustomSession("insert");
                        SqlDriver.insertBetweenRows(manualSess);
                        break;
                    case 2:
                        showAllDates();
                        manualSess = UserInput.createCustomSession("edit");
                        UserInput.cwWrap(SqlDriver.editRows(manualSess));
                        break;
                }

            }
            consoleInput = 900;
            break;
    }
}


