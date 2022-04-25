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
            string uOut = null;
            Console.WriteLine("Would you like to use today as Date?(y/n)");
            string uInp = Console.ReadLine();
            while (uOut == null)
            {
                if (uInp == "y")
                {
                    uOut = DateTime.Today.ToString("d");
                    Console.WriteLine(uOut);
                }
                else if (uInp == "n")
                {
                    Console.WriteLine("Input your date with the following format: DD.MM.YYYY");
                    uOut = Console.ReadLine();
                    uOut = rgxCheck(@"\d\d.\d\d.\d\d\d\d", uOut);
                    if (uOut == null)
                    {
                        Console.WriteLine("Wrong format. Did you make a typo?");
                    }
                }
                else
                {
                    Console.WriteLine("could not read your Respose");
                }

            }
            string timeInp = null;
            while (timeInp == null)
            {
                Console.WriteLine("Please Input a time in 24h format (eg 16:30):");
                timeInp = Console.ReadLine();
                timeInp = rgxCheck(@"\d\d:\d\d", timeInp);
            }
            uOut = uOut + " " + timeInp;
            return uOut;
        }
        
        public static int menu( List<string> menue)
        //never initalize to negative numbers
        {
            int userInput = 900;
            Console.WriteLine("What would you like to do?\n\n");
            foreach (string opt in menue)
            {
                Console.WriteLine(opt);
            }
            while (userInput >= 0 & userInput >= menue.Count)
            {
                try
                {

                    userInput = Math.Abs(Convert.ToInt32(Console.ReadLine()));

                }
                catch (Exception)
                {
                    Console.WriteLine("Bitte eine Zahl eingeben");
                }
                if (userInput > menue.Count - 1)
                {
                    Console.WriteLine("Invalid number. Try again:");
                }
            }
            return userInput;
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
