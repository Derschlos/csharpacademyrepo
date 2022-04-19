using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeTimeTracker
{
    internal class CodingSession
    {
        private int id;
        public int Id { get { return id; } set { id = value;} }    
        
        private string start;
        public string Start { get { return start; } set { start = value; } }
        
        private string end;
        public string End { get { return end; } set { end = value; } }
        
        private string duration;
        public string Duration { get { return duration; } }

        

        //public static void start()
        //{
        //    Console.WriteLine("CodingSession");
        //}
    }

}
