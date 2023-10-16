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