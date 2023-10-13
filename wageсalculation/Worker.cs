using System;
using System.Collections.Generic;

namespace wageсalculation
{
    public class Worker : Role,IDo
    {
        //public string[] methods { get; set; }
        public Worker()
        {
            wage = new Wage(120000, 240000);
            methods = new string[2];
            methods[0] = "AddHour";
            methods[1] = "MakeReport";
            //del+= AddHour2();
            commands= new Dictionary<string, Action<InfoWork>>();
        }


        public void AddHour(InfoWork work)
        {
            throw new NotImplementedException();
        }

        public void MakeReport(DateTime startData, DateTime endData)
        {
            throw new NotImplementedException();
        }
    }
}