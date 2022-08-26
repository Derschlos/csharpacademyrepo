using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralScripts_Classes
{
    internal class MiscFunctions
    {
        internal static int getDayRange(DateTime start, DateTime ende)
        {
            //calculates the amount of German business days from two dates
            //by itterating the week while checking if its a Holiday,Sunday or Saturday
            //needs Nugget: "PublicHolidays"
            //returns an int
            GermanPublicHoliday holidays = new GermanPublicHoliday { State = GermanPublicHoliday.States.HH };
            var holidaysInRange = holidays.GetHolidaysInDateRange(start, ende);

            if (!(start.CompareTo(ende) > 0))
            {
                var range = 0;
                while (start <= ende)
                {
                    if (!(holidays.IsPublicHoliday(start) || start.DayOfWeek == DayOfWeek.Sunday || start.DayOfWeek == DayOfWeek.Saturday))
                    {
                        range++;
                    }
                    start = start.AddDays(1);
                }
                return range;
            }
            return 0;
        }

        public static string rgxCheck(string pattern, string text)
        {
            ///searches a Regex Pattern in a text
            ///returns the first found value
            ///needs "System.Text.RegularExpressions"
            Regex rgx = new Regex(pattern);
            MatchCollection matches = rgx.Matches(text);
            if (matches.Count > 0)
            {
                return matches[0].Value;
            }
            else
            {
                return null;
            }
        }

    }
}
