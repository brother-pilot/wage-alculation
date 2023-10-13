using System;
using System.Collections.Generic;

namespace wageсalculation
{
    public class Role<T>
    {  
        protected Wage wage;
        public string[] methods;
        //public Action<InfoWork> del;
        public Dictionary<string, Action<T>> commands { get; set; }
        //public void RegisterCommand(string command, Action<InfoWork> execute)
        //{
        //    commands.Add(command, execute);
        //}
    }
}