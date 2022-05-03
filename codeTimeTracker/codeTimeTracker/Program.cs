using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Data.Sqlite;
using System.Globalization;
using System.Text.RegularExpressions;
using codeTimeTracker;


string sqlIn = @"SELECT MAX(id) FROM codeTrack";
var maxId = SqlDriver.executeSql(sqlIn, "r");
CodingSession currSession;
if (maxId[0] == "")
{
    currSession = new CodingSession(1, null, null, null);
}
else
{
    currSession = new CodingSession(Convert.ToInt32(maxId[0]), null, null, null);
}
List<string> mainMenu = new List<string>()
    {
        "Type 0 to close the application",
        "\nType 1 to view all records",
        "Type 2 to start the counter",
        "Type 3 to end the counter",
        "Type 4 to save the current session",
        "\nType 5 to manually change dates",
        "Type 6 to view the current session"
    };

static void showAllDates()
{   
    var sqlOut = SqlDriver.getAllSessions();
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
            //Console.Clear();
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
                Console.Clear();
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
                    case 3:
                        showAllDates();
                        UserInput.cwWrap("Which Id do you want to delete?");
                        var selectedId = UserInput.idInp();
                        var selectedSession = SqlDriver.GetSessionById(selectedId, selectedId);
                        UserInput.cwWrap("Are you sure you want to delete this Coding Session?(y/n)");
                        var delete= Console.ReadLine();
                        if (delete == "y")
                        {
                            UserInput.cwWrap(SqlDriver.deleteRow(selectedSession[selectedId]));
                        }
                        else if (delete == "n")
                        {
                            UserInput.cwWrap("Aborting. The Session will not be Deleted");
                        }
                        else
                        {
                            UserInput.cwWrap("Could not read your input. Delete will be aborted.");
                        }
                        break;
                }

            }
            consoleInput = 900;
            break;
        case 6:
            SqlDriver.createTable(new Dictionary<int, CodingSession>() {{ Convert.ToInt32(maxId[0]), currSession }}, "Current Session");
            break;
    }
}


