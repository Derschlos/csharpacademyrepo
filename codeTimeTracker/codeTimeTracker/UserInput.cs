using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace codeTimeTracker
{
    internal class UserInput
    {
        public string timeInput()
        {   
            string uOut = string.Empty;
            Console.WriteLine("Would you like to use today as Date?(y/n)");
            while (uOut == string.Empty)
            {
                string uInp = Console.ReadLine();
                if (uInp == "y")
                {
                    
                    uOut = DateTime.Today.ToString("d");
                    Console.WriteLine(uOut);
                }
                else if (uInp == "n")
                {
                    Console.WriteLine("Input your date with the following format: DD.MM.YYYY");
                    uOut = Console.ReadLine();

                }
                else
                {
                    Console.WriteLine("could not read your Respose");
                }

            }
            Console.WriteLine("Please Input a time in 24h format (eg 16:30):");
            string timeInp = Console.ReadLine();
            timeInp = rgxCheck(@"\d\d:\d\d", timeInp);

            uOut = uOut + " " + Console.ReadLine();
            return uOut;
        }

        public string rgxCheck(string pattern, string text)
        {
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
