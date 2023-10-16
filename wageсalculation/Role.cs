using System;
using System.Collections.Generic;

namespace wageсalculation
{
    public class Role
    {  
        protected Wage wage;
        //public string[] methods;
        //public Action<InfoWork> del;
        public Dictionary<string, Action<object>> commands { get; set; }

        public void AddHour(InfoWork work)
        {
            
            Console.WriteLine("bola");
        }
    }
}