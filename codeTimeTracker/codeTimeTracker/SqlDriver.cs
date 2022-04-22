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

        List<string> outputObject = executeSql(@"CREATE TABLE IF NOT EXISTS codeTrack (
                                                id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                startDate TEXT,
                                                endDate TEXT,
                                                duration TEXT
                                                )", 
                                                "w");

        private static List<String> executeSql(string inputString, string readWrite)
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
                                    object[] row = new object[3];
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
        
    }
}
