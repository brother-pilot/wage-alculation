using System;
using System.Collections.Generic;

namespace wageсalculation.Persistance
{
    public class Worker : Role
    {
        //public string[] methods { get; set; }
        public Worker()
        {
            wage = new Wage(120000, 240000);
            commands = new Dictionary<int, Command>();
            commands[1] = Command.AddHour;
            commands[2] = Command.MakeReport;
            commands[6] = Command.Exit;
        }


    }
}