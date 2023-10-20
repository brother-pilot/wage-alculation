using System;
using System.Collections.Generic;

namespace wageсalculation.Persistance
{
    public class Freelancer : Role
    {
        //public string[] methods { get; set; }

        public Freelancer()
        {
            wage = new Wage(0, 0, false, false, 1000);
            commands = new Dictionary<int, Command>();
            commands[1] = Command.AddHour;
            commands[2] = Command.MakeReport;
            commands[6] = Command.Exit;
        }
        
    }
}