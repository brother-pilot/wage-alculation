using System;
using System.Collections.Generic;

namespace wageсalculation.Persistance
{
    public class Worker : UserRole
    {
        //public string[] methods { get; set; }
        public Worker()
        {
            wage = new Wage(120000, 120000);
            //Commands = new Dictionary<int, Command>();
            Commands[1] = Command.AddHour;
            Commands[2] = Command.MakeReport;
            Commands[6] = Command.Exit;
        }


    }
}