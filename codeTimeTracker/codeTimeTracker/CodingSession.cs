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
        private DateTime _start;
        public string Start 
        { 
            get { return _start.ToString(); } 
            set { if (value != null) { _start = parseDate(value); } }
        }
        private DateTime _end;
        public string End 
        { 
            get { return _end.ToString(); } 
            set {if (value != null) { _end = parseDate(value); }}
        }       
        private double _duration;
        public string Duration 
        { 
            get { return _duration.ToString(); } 
        }

        public CodingSession(int Id, string Start, string End, string Duration)
        {
            this.Id = Id;
            this.Start = Start;
            this.End = End;
            _duration = Convert.ToDouble( Duration);
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
            if(_end.Year<1950 || _start.Year <1950)
            {
                Console.WriteLine("Start or Enddate is faulty. Duration will not be considered");
                return;
            }
            _duration = Math.Abs((_end - _start).TotalHours);
        }
        public void startCounter()
        {
            _start = DateTime.Now;
        }
        public void endCounter()
        {
            _end = DateTime.Now;
            if (_start.Year > 1900)
            {
                durationEval();
            }
        }
        public List<object> expData()
        {
            return new List<object> { Convert.ToString(Id), Start, End, Duration };
        }

        public string insRowSql(bool edit) 
        {
            string sqlIn;
            if (_start.Year > 1900 && _end.Year > 1900)
            {
                if(edit)
                {
                     sqlIn = $@"INSERT INTO codeTrack (id, startDate ,endDate , duration)
                VALUES ({Id}'{Start}','{End}','{Duration}')";
                }
                else
                {
                    sqlIn = $@"INSERT INTO codeTrack (startDate ,endDate , duration)
                VALUES ('{Start}','{End}','{Duration}')";
                }    
                var sqlOut = SqlDriver.executeSql(sqlIn, "w");
                return "Row inserted succesfully";
            }
            return "Could not insert row because either the start or end were not set";
        }
    }   

}
