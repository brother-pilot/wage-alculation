using System;

namespace wageсalculation
{
    public class Freelancer : Role, IDo
    {
        //public string[] methods { get; set; }

        public Freelancer()
        {
            wage = new Wage(0, 0, false, false, 1000);
            methods = new string[2];
            methods[0] = "AddHour";
            methods[1] = "MakeReport";
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