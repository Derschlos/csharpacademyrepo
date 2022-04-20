using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeTimeTracker
{
    internal class CodingSession
    {
        private int _id;
        public int Id { get { return _id; } set { _id = value;} }

        //private DateTime _date;
        //public string Date
        //{
        //    get { return _date.ToString(); }
        //    set {_date = parseDate(value);}
        //}
        
        private DateTime _start;
        public string Start 
        { 
            get { return _start.ToString(); } 
            set { _start = parseDate(value); }
        }
        
        private DateTime _end;
        public string End 
        { 
            get { return _end.ToString(); } 
            set { _end = parseDate(value); }
        }
        
        private double _duration;
        public string Duration 
        { 
            get { return _duration.ToString(); } 
        }

        private static DateTime parseDate(string inVal)
        {
            try
            {
                DateTime outVal = DateTime.Parse(inVal);
                return outVal;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not parse the Date.\nSetting Date to NOW because of following reasons:\n");
                Console.WriteLine(ex+"\n");

                return DateTime.UtcNow;
            }

        }

        public void durationEval()
        {
            _duration = Math.Abs((_end - _start).TotalHours);
        }
        public void startCounter()
        {
            _start = DateTime.Now;
        }
        public void endCounter()
        {
            _end = DateTime.Now;

        }
        
        
    }   

}
