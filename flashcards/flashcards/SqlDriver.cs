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
        public static List<String> executeSql(string inputString, string readWrite)
        {
            SqlConnection _connection = new SqlConnection(@$"Data Source={ConfigurationManager.AppSettings.Get("dbLocation")}
                                                                {ConfigurationManager.AppSettings.Get("dbName")};
                                                             Integrated Security={ConfigurationManager.AppSettings.Get("Integrated Security")}
                                                             ");
            //SqlConnection _connection = new SqlConnection(@$"Data Source=(LocalDb)\flashcardsdb;Integrated Security=True");
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
                                        if (val != null)
                                        {
                                            rowData.Add(Convert.ToString(val));
                                        }
                                }
                                break;

                            }
                    };
                    //_connection.Close();
                    return rowData;
                }
            }
        }
        public static void initialize()
        {
            SqlDriver.executeSql(@"if not exists (select * from sysobjects where name='languages' and xtype='U') 
                            CREATE TABLE languages(
                            id INTEGER PRIMARY KEY,
                            name TEXT,
                            langId INTEGER
                            )", "w");
            SqlDriver.executeSql(@"if not exists (select * from sysobjects where name='cards' and xtype='U') 
                            CREATE TABLE cards(
                            id INTEGER PRIMARY KEY,
                            name TEXT
                            )", "w");
            //var a = SqlDriver.executeSql("select * from sysobjects where name='languages' and xtype='U'", "r");
        }
        public static void createTable(List<String> data, string title)
        {
            //var tableData = new List<List<object>> { };
            ////foreach (var ses in data)
            ////{
            //tableData.Add(data);
            ////}
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
                            //.WithFormatter(3, (text) =>
                            //{
                            //    if (string.IsNullOrEmpty(text) || text.Trim().Length == 0)
                            //    {
                            //        return "0 h";
                            //    }
                            //    else
                            //    {
                            //        return text + " h";
                            //    }
                            //})
                            .WithColumnFormatter(0, (text) => "ID")
                            .WithColumnFormatter(1, (text) => "Start Date")
                            .WithColumnFormatter(2, (text) => "End Date")
                            .WithColumnFormatter(3, (text) => "Duration")
                            .ExportAndWriteLine();
        }

    }
}
