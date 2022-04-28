using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Data.Sqlite;

namespace codeTimeTracker
{
    internal class SqlDriver
    {
        private static SqliteConnection con = new SqliteConnection(@"Data Source="+ConfigurationManager.AppSettings.Get("databasePath"));

        static List<string> outputObject = executeSql(@"CREATE TABLE IF NOT EXISTS codeTrack (
                                                id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                startDate TEXT,
                                                endDate TEXT,
                                                duration TEXT
                                                )", 
                                                "w");

        public static List<String> executeSql(string inputString, string readWrite)
        {
            using (con)
            {
                var tableCmd = con.CreateCommand();
                using (tableCmd)
                {
                    List<String> rowData = new List<string>();
                    tableCmd.CommandText = inputString;
                    con.Open();
                    switch (readWrite)
                    {
                        case "w":
                            {
                                tableCmd.ExecuteNonQuery();
                                break;
                            }
                        case "r":
                            {
                                var reader = tableCmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    object[] row = new object[4];
                                    reader.GetValues(row);
                                    foreach (var val in row)
                                        rowData.Add(Convert.ToString(val));
                                }
                                break;

                            }
                    };
                    return rowData;
                }
            }
        }
        
        public static Dictionary<int, CodingSession> getSessions(string sqlIn)
        //gets a dict of all coding sessions from the db sorted by Id
        {
            Dictionary<int, CodingSession> sessionList = new Dictionary<int, CodingSession>();
            List<string> sqlOut = SqlDriver.executeSql(sqlIn, "r");
            for (int i = 0; i < sqlOut.Count; i += 4)
            {
                //Console.WriteLine($"{sqlOut[i + 1]} {sqlOut[i + 2]}  {sqlOut[i + 3]} ");
                sessionList.Add(Convert.ToInt32(sqlOut[i]), new CodingSession(Convert.ToInt32(sqlOut[i]), sqlOut[i + 1], sqlOut[i + 2], sqlOut[i + 3]));
            }
            return sessionList;
        }
        public static void insertBetweenRows(CodingSession manualSess)
            //creates a new sessionand checks if it fits into the db rows
            //then shifts them to make room or        
        {
            
            var sqlIn = @"SELECT MAX(id) FROM codeTrack";
            var maxId = SqlDriver.executeSql(sqlIn, "r");
            List<String> loopSql;
            sqlIn = $@"SELECT * FROM codeTrack WHERE id = {manualSess.Id}";
            loopSql = SqlDriver.executeSql(sqlIn, "r");
            if (loopSql.Count > 0)
            {
                //shifts the rows up 1 for all rows till id
                for (int i = Convert.ToInt32(maxId[0]); i >= manualSess.Id; i--)
                {
                    sqlIn = @$"SELECT * FROM codeTrack WHERE id = {i}";
                    loopSql = SqlDriver.executeSql(sqlIn, "r");
                    sqlIn = $@"UPDATE codeTrack SET id = {i + 1} WHERE id = {i}";
                    loopSql = SqlDriver.executeSql(sqlIn, "w");
                    Console.WriteLine("done");
                }
            }
            insRowSql(manualSess ,true);
        }

        public static string insRowSql(CodingSession session,bool betweenRows)
        {
            string sqlIn;
            if (session.checkForTimeInputs())
            {
                if (betweenRows)
                {
                    sqlIn = $@"INSERT INTO codeTrack (id, startDate ,endDate , duration)
                VALUES ({session.Id}'{session.Start}','{session.End}','{session.Duration}')";
                }
                else
                {
                    sqlIn = $@"INSERT INTO codeTrack (startDate ,endDate , duration)
                VALUES ('{session.Start}','{session.End}','{session.Duration}')";
                }
                var sqlOut = SqlDriver.executeSql(sqlIn, "w");
                return "Row inserted succesfully";
            }
            return "Could not insert row because either the start or end were not set correctly";
        }
        public static string editRows(CodingSession session)
        {
            string sqlIn;
            if (session.checkForTimeInputs())
            {
                sqlIn = $@"UPDATE codeTrack SET startDate = '{session.Start}', 
                            endDate = '{session.End}', duration = '{session.Duration}' WHERE id = {session.Id}";
                return "Row succesfully edited";
            }
            return "Could not edit Rows because either the start or end were not set correctly";
        }
    }
}
