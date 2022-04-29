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
        public static void cwWrap(string sOut)
        {
            Console.WriteLine("***************************\n");
            Console.WriteLine(sOut);
            Console.WriteLine("\n***************************\n");
        }
        
        public static CodingSession createCustomSession(string editInsert)
        {
            UserInput.cwWrap($"Where would you like to {editInsert}?");
            int newId = UserInput.idInp();
            if (editInsert == "edit")
            {
                SqlDriver.GetSessionById(newId, newId);
            }
            
            CodingSession insSession = new CodingSession(newId, null, null, null);
            UserInput.cwWrap("Please Input the start dates");
            insSession.Start = timeInp(null);

            UserInput.cwWrap("Now please Input the end dates");
            insSession.End = secondDayInput(insSession.startYear());
            insSession.durationEval();
            return insSession;
        }
        public static string secondDayInput(string startYear)
        {
            string uOut = null;
            Console.WriteLine("Would you like to use the same day as for the Start Date?(y/n)");
            string uInp = Console.ReadLine();
            while (uOut == null)
            {
                if (uInp == "y")
                {
                    return timeInp(startYear);
                }
                uOut = timeInp(null);
            }
            return uOut;
        }
        public static string timeInp(string dateSelected)
        {   
            string uOut = null;
            string uInp;
            if (dateSelected == null)
            {
                Console.WriteLine("Would you like to use today as Date?(y/n)");
                uInp = Console.ReadLine();
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
            }
            string timeInp = null;
            
            while (timeInp == null)
            {
                Console.WriteLine("Please Input a time in 24h format (eg 16:30):");
                timeInp = Console.ReadLine();
                timeInp = rgxCheck(@"\d?\d:\d\d", timeInp);
            }
            if (uOut != null)
            {
                uOut = uOut + " " + timeInp;
            }
            else
            {
                uOut = dateSelected + " " + timeInp;
            }
            return uOut;
        }
        public static int idInp()
        {
            Console.WriteLine("Which Id would you like to select?");
            
            string uOut = null;
            while(uOut == null)
            {
                string uInp = Console.ReadLine();
                uOut = rgxCheck(@"\d+", uInp);
                if (uOut == null)
                {
                    Console.WriteLine("Invalid input. Please input a number");
                }
            }

            return Convert.ToInt32(uOut);
        }
        public static int menu( List<string> menue)
        //never initalize menu to negative numbers
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


        public static string rgxCheck(string pattern, string text)
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
