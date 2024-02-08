using System;
using System.Collections.Generic;

namespace wageсalculation.Persistance
{
    public class Freelancer : UserRole
    {
        //public string[] methods { get; set; }

        public Freelancer()
        {
            wage = new Wage(0, 0, false, false, 1000);
            //commands = new Dictionary<int, Command>();
            Commands[1] = Command.AddHour;
            Commands[2] = Command.MakeReport;
            Commands[6] = Command.Exit;
        }
        
    }
}