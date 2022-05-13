using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace flashcards
{
    internal class ImportLangDb
    {
        private string _dbName;
        public string Name { get; }
        public Dictionary<int, Card> cards = new Dictionary<int, Card>();
        public ImportLangDb(string name)
        {
            _dbName = name;
            Name = name.Split(".")[0];
            if (File.Exists(_dbName))
            {
                cards = getCards(_dbName);
            }
        }
        private static List<String> getRows(string input, string Name)
        {
            SqliteConnection _connection = new SqliteConnection(@$"Data Source=.\{Name}");
            using (_connection)
            {
                var tableCmd = _connection.CreateCommand();
                using (tableCmd)
                {
                    List<String> rowData = new List<string>();
                    tableCmd.CommandText = input;
                    _connection.Open();
                    var reader = tableCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        object[] row = new object[2];
                        reader.GetValues(row);
                        foreach (var val in row)
                            if (val != null)
                            {
                                rowData.Add(Convert.ToString(val));
                            }
                    }
                    return rowData;
                };
                //_connection.Close();

            }
        }
        public static Dictionary<int, Card> getCards(string _dbName)
        {

            Dictionary<int, Card> cardsOut = new Dictionary<int, Card>();
            var sqlIn = $"SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY 1";
            var sqlOut = getRows(sqlIn, _dbName);
            int j = 1;
            foreach (var table in sqlOut)
            {
                sqlIn = $"SELECT * FROM '{table}'";
                var rows = getRows(sqlIn, _dbName);
                for (int i = 0; i<(rows.Count-1); i+=2)
                {
                    cardsOut.Add(j, new Card(i));
                    cardsOut[j].Front = rows[i];
                    cardsOut[j].Back = rows[i+1];
                    j ++;
                }
            }
            return cardsOut;
        }

    }
    
}
