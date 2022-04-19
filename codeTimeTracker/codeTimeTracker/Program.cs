using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Data.Sqlite;
using System.Globalization;
using System.Text.RegularExpressions;



string sAtt = ConfigurationManager.AppSettings.Get("databasePath");
Console.WriteLine("between");
Console.WriteLine(sAtt);
Console.WriteLine("these");