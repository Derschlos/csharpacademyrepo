using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using ConsoleTableExt;

namespace flashcards
{
    internal class SqlDriver
    {
        //ConfigurationManager.AppSettings.Get("databasePath")
        private static SqlConnection _connection = new SqlConnection(@$"Data Source={ConfigurationManager.AppSettings.Get("dbLocation")}{ConfigurationManager.AppSettings.Get("dbName")};
                                                                        Integrated Security={ConfigurationManager.AppSettings.Get("Integrated Security")}");
        public static void initialize()
        {
           executeSql(@"if not exists (select * from sysobjects where name='languages' and xtype='U') 
                            CREATE TABLE languages(
                            id INTEGER PRIMARY KEY,
                            name TEXT
                            )", "w");
        }

        public static List<String> executeSql(string inputString, string readWrite)
        {
            using (_connection)
            {
                var tableCmd = _connection.CreateCommand();
                using (tableCmd)
                {
                    List<String> rowData = new List<string>();
                    tableCmd.CommandText = inputString;
                    _connection.Open();
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
        List<String> a = executeSql("CREATE TABLE languages(id INTEGER PRIMARY KEY, name TEXT)", "w");
    }
}
