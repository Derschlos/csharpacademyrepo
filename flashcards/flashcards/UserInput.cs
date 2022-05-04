using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace flashcards
{
    internal class UserInput
    {
        public static void cwWrap(string sOut)
        {
            Console.WriteLine("***************************\n");
            Console.WriteLine(sOut);
            Console.WriteLine("\n***************************\n");
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
        public static int idInp()
        {
            Console.WriteLine("Which Id would you like to select?");

            string uOut = null;
            while (uOut == null)
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

        public static int menu(List<string> menue, string header)
        //never initalize menu to negative numbers
        {
            int userInput = 900;
            Console.WriteLine("\n"+header+"\n");
            Console.WriteLine("------------------\n");
            foreach (string option in menue)
            {
                Console.WriteLine(" "+option);
            }
            Console.WriteLine("\n------------------");
            Console.WriteLine("\nInput your selection \nOr 0 to exit");
            while (!(userInput >= 0 && userInput <= menue.Count))
            {
                string input = Console.ReadLine();
                try
                {
                    userInput = Math.Abs(Convert.ToInt32(input));
                }
                catch (Exception)
                {
                    userInput = menue.IndexOf(input);
                    if (userInput == -1)
                    {
                        foreach (string option in menue)
                        {
                            if (option.Substring(0, 5).Contains(input))
                            {
                                userInput = menue.IndexOf(option);
                                break;
                            }
                            userInput = 9999;
                        }
                    }
                }
                if (userInput > (menue.Count-1)  || userInput<0)
                {
                    Console.WriteLine("Invalid Input. Try again:");
                }
            }
            
            return userInput;
        }
    }
}
