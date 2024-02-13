using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using wageсalculation.Persistance;

namespace TestClass
{
    internal class StubModel : IModel
    {
        public List<CurrentUser> Users =>
            new List<CurrentUser>
            {
             new CurrentUser("HTest",Level.Head),
             new CurrentUser("WTest",Level.Worker),
             new CurrentUser("FTest",Level.Freelancer),
             new CurrentUser("HTest2",Level.Head)
            };

        public List<InfoWork> InfoWorks =>
            new List<InfoWork>
            {
             new InfoWork(DateTime.Today, "HTest", 24, "Work1"),
             new InfoWork(DateTime.Today, "HTest", 8, "Work2"),
             new InfoWork(DateTime.Today, "HTest", 12, "Work3"),
             new InfoWork(DateTime.Today, "WTest", 24, "Work1"),
             new InfoWork(DateTime.Today, "WTest", 8, "Work2"),
             new InfoWork(DateTime.Today, "WTest", 12, "Work3"),
             new InfoWork(DateTime.Today, "FTest", 24, "Work1"),
             new InfoWork(DateTime.Today, "FTest", 8, "Work2"),
             new InfoWork(DateTime.Today, "FTest", 12, "Work3"),
            };

        

    public void AddHour(InfoWork w)
        {
            throw new NotImplementedException();
        }

        public void AddUser(CurrentUser u)
        {
            throw new NotImplementedException();
        }

        public List<InfoWork> MakeReport(CurrentUser u, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        void IModel.RecieveDataFromControllerData()
        {
            throw new NotImplementedException();
        }

        void IModel.SentDataToControllerData()
        {
            throw new NotImplementedException();
        }
    }
}