using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace GeneralScripts_Classes
{
    internal class SqlServer
    {
        internal static SqlConnection connector()
        {
            //creates and returns a SqlConnection when using sqlserver 
            //needs Nugget "ConfigurationBuilder"
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();
            var conn = new SqlConnection(root.GetConnectionString("ConnectionString"));
            return conn;
        }
        public void createQuery(string input)
        {
            //Runs a SQL Query (input) by creating and executing a connection.
            //Good to init a server or to create a Procedure.
            
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();
            using (var conn = new SqlConnection(root.GetConnectionString("ConnectionString")))
            {
                conn.Open();
                var tableCmd = conn.CreateCommand();
                tableCmd.CommandText = input;
                SqlDataReader reader = tableCmd.ExecuteReader();
            }
        }
        public void CT_dbo_TableName()
        {
            //Create Table in dbo
            //uses the createQuery function
            var table = @"if not exists (select * from sysobjects where name='TableName' and xtype='U') 
                            CREATE TABLE TableName(
                            id INTEGER IDENTITY(1,1) PRIMARY KEY,
                            prop2 INT NOT NULL,
                            prop3 INT NOT NULL,
                            prop4 DATETIME NOT NULL,
                            prop5 DATETIME NOT NULL,
                            prop6 INT NOT NULL,
                            prop7 BIT,
                            prop8 BIT,
                            prop9 BIT DEFAULT 0
                            )";
            createQuery(table);
        }
        private void CPR_sp_users_remainingDays()
        {
            // Creates a Procedure using the createQuery funtion
            //[dbo] is the database
            //in this example takes a userId as input
            //and summs the available and taken Vacationdays 
            //finaly it outputs the days as int

            var procedure = $@"CREATE PROCEDURE [dbo].[sp_TableName_ProcedureName]
                            @user int,
                            @days int OUTPUT
                            as
                            begin
	                            SET NOCOUNT ON;
	                            DECLARE @Vac int =ISNULL((SELECT baseYearlyVacation + transferedVacationDays + addedVacationDays FROM users WHERE id=@user),0);
	                            DECLARE @taken INT = ISNULL((SELECT sum(vacationLength) FROM antraege WHERE userId = @user and active = 1 and ((approved = 0 and seen = 0 ) or (approved = 1 and seen =1))),0);
	                            SELECT @days = @vac-@taken;
                            end";
            createQuery(procedure);
        }

        internal static int storedProcedDay(int user, string nameOfProcedure)
        {
            //procs a predefined procedure for a user
            //and gets one output as int
            var avDays = 0;
            using (var conn = connector())
            {
                conn.Open();
                var tableCmd = conn.CreateCommand();
                tableCmd.CommandText = $@"DECLARE @val INT
                                        EXEC  [dbo].[sp_users_{nameOfProcedure}] {user}, @val Output
                                        SELECT @val";
                SqlDataReader reader = tableCmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!(reader.IsDBNull(0)))
                    {
                        avDays = reader.GetInt32(0);
                    }
                }
            }
            return avDays;
        }
        public static List<String> executeSql(string inputString, string readWrite)
        {
            //creates a connection to a dbo and then queryes the inputString
            //"r" to read from the dbo,
            //"w" to write to the dbo.
            //to keep the function general returns a list of String vals even if parts are int.


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
    }
}
