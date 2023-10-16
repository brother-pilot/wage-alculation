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
            commands = new Dictionary<string, Action<object>>();
            commands["AddHour"] = w => AddHour(w as InfoWork);
            commands["MakeReport"] = ti => MakeReport(ti as TimeInterval);
        }


        public void AddHour(InfoWork work)
        {
            throw new NotImplementedException();
        }

        public void MakeReport(TimeInterval ti)
        {
            throw new NotImplementedException();
        }
    }
}