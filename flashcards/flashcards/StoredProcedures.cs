using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace flashcards
{
    internal class StoredProcedures
    {
        SqlConnection _connection = new SqlConnection(@$"Data Source={ConfigurationManager.AppSettings.Get("dbLocation")}
                                                                {ConfigurationManager.AppSettings.Get("dbName")};
                                                             Integrated Security={ConfigurationManager.AppSettings.Get("Integrated Security")}
                                                             ");
        //con.open();
        //    CREATE PROCEDURE RegionUpdate(@RegionID INTEGER,
        //    @RegionDescription NCHAR(50)) AS
        //    SET NOCOUNT OFF
        //    UPDATE Region
        //    SET RegionDescription = @RegionDescription
        //SqlCommand command = new SqlCommand("RegionUpdate", con);
        //command.CommandType = CommandType.StoredProcedure;
        //     command.Parameters.Add(new SqlParameter("@RegionID", SqlDbType.Int,0,"RegionID"));

        //    command.Parameters.Add(new SqlParameter("@RegionDescription", SqlDbType.NChar,50,"RegionDescription"));
        //     command.Parameters[0].Value=4;

        //    command.Parameters[1].Value="SouthEast";
        //    int i = command.ExecuteNonQuery();
        //ALTER PROCEDURE RegionFind(@RegionDescription NCHAR(50) OUTPUT,
        //    @RegionID INTEGER )AS

        //    SELECT @RegionDescription =RegionDescription from Region where<A href= "mailto:RegionID=@RegionID" > RegionID = @RegionID </ A >
    }
}
