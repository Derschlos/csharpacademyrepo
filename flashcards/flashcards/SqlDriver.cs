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
                            id INTEGER IDENTITY(1,1) PRIMARY KEY,
                            name VARCHAR(MAX)
                            )", "w");
            SqlDriver.executeSql(@"if not exists (select * from sysobjects where name='cards' and xtype='U') 
                            CREATE TABLE cards(
                            id INTEGER IDENTITY(1,1) PRIMARY KEY,
                            front VARCHAR(MAX),
                            back VARCHAR(MAX),
                            langId INTEGER
                            )", "w");
            //var a = SqlDriver.executeSql("select * from sysobjects where name='languages' and xtype='U'", "r");
        }
        public static void createTable(List<List<object>> data, string title, List<string> headers)
        {
            if (data.Count() == 0)
            {
                Console.WriteLine($"No Data found for \"{title}\"");
                return;
            }
            //var tableData = new List<List<object>> { };
            //foreach (var ses in data)
            //{
            //    tableData.Add(ses);
            //}
            ConsoleTableBuilder.From(data)
                            .WithTextAlignment(new Dictionary<int, TextAligntment> {
                                { 0, TextAligntment.Center },
                                { 1, TextAligntment.Center },
                                { 2, TextAligntment.Center },
                                {3, TextAligntment.Center },
                            })
                            .WithMinLength(new Dictionary<int, int> {
                                { 0, 5 },
                                { 1, 25 },
                                { 2, 25 }, 
                                { 3, 25 },

                            })
                            .WithTitle(title.ToUpper(), ConsoleColor.DarkYellow, ConsoleColor.DarkRed)
                            .WithCharMapDefinition(CharMapDefinition.FrameDoublePipDefinition)
                            .WithColumn(headers)
                            .ExportAndWriteLine();
        }
        public static Dictionary<int, Languages> getAllLanguages()
        {
            Dictionary<int, Languages> langs = new Dictionary<int, Languages>();
            var langIds = SqlDriver.executeSql("SELECT id FROM languages", "r");
            foreach (var langId in langIds)
            {
                var id = Convert.ToInt16(langId);
                langs.Add(id, new Languages(id));
            }
            return langs;
        }

        public static List<string> allCardsId(int langId)
        {
            var sqlIn = $@"SELECT id FROM cards WHERE langId = {langId}";
            var sqlOut = executeSql(sqlIn, "r");
            return sqlOut;
        }
        public static List<string> cardContent(int id)
        {
            var sqlIn = $@"SELECT front, back FROM cards WHERE id = {id}";
            var sqlOut = executeSql(sqlIn, "r");
            return sqlOut;
        }
        public static string loadLangName(int id)
        {
            var sqlIn = $"SELECT name FROM languages WHERE id = {id} ";
            try
            {
                return executeSql(sqlIn, "r")[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Language loading faulty. Could not find id {id} in the databank");
                return "No Name";
            }
        }
        public static void insertLanguage(string langName)
        {
            var sql = $"INSERT INTO languages (name) VALUES ('{langName}')";
            executeSql(sql, "w");
        }
        public static void insertCard(Card card, int langId)
        {
            var sql = @$"INSERT INTO cards (front, back,langId) VALUES ('{card.Front}', '{card.Back}', {langId})";
            executeSql(sql, "w");
        }
        public static void importDbToServer(ImportLangDb import)
        {
            var sqlIn = $"SELECT * FROM languages WHERE name = '{import.Name}'";
            var sqlOut = executeSql(sqlIn, "r");
            if (sqlOut.Count != 0)
            { return; };
            sqlIn = $"INSERT INTO languages (name) VALUES ('{import.Name}')";
            sqlOut = executeSql( sqlIn, "w");
            sqlIn = $"SELECT id FROM languages WHERE name = '{import.Name}'";
            string langId = executeSql(sqlIn, "r")[0];
            foreach (var cardIndex in import.cards)
            {
                Card card = cardIndex.Value;
                if (card.Front.Contains("'")|| card.Back.Contains("'"))
                {

                }
                sqlIn = $"INSERT INTO cards (front, back, langId) VALUES ('{card.Front}', '{card.Back}', '{langId}')";
                sqlOut = executeSql(sqlIn, "w");
            }
        }
    }
}
