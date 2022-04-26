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

    }
}
