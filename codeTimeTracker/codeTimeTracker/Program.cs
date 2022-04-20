using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Data.Sqlite;
using System.Globalization;
using System.Text.RegularExpressions;
using codeTimeTracker;



string sAtt = ConfigurationManager.AppSettings.Get("databasePath");
Console.WriteLine("between");
Console.WriteLine(sAtt);
Console.WriteLine("these");
//CodingSession.start();
CodingSession session = new CodingSession();
//session.id = 1;
//session.first.set() = "a"
session.End = "19.04.2022 15:30";
Console.WriteLine(session.Start);
session.Start = "17:00";
session.durationEval();
Console.WriteLine(session.Duration);