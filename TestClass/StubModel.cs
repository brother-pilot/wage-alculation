using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using wageсalculation.Persistance;

namespace TestClass
{
    internal class StubModel : IModel
    {
        public List<User> Users =>
            new List<User>
            {
             new User("HTest",Level.Head),
             new User("WTest",Level.Worker),
             new User("FTest",Level.Freelancer)
            };

        public List<InfoWork> InfoWorksHeader =>
            new List<InfoWork>
            {
             new InfoWork(DateTime.Today, "HTest", 24, "Work1"),
             new InfoWork(DateTime.Today, "HTest", 8, "Work2"),
             new InfoWork(DateTime.Today, "HTest", 12, "Work3"),
            };

        public List<InfoWork> InfoWorksWorker =>
            new List<InfoWork>
            {
             new InfoWork(DateTime.Today, "WTest", 24, "Work1"),
             new InfoWork(DateTime.Today, "WTest", 8, "Work2"),
             new InfoWork(DateTime.Today, "WTest", 12, "Work3"),
            };

        public List<InfoWork> InfoWorksFreelancer =>
            new List<InfoWork>
            {
             new InfoWork(DateTime.Today, "FTest", 24, "Work1"),
             new InfoWork(DateTime.Today, "FTest", 8, "Work2"),
             new InfoWork(DateTime.Today, "FTest", 12, "Work3"),
            };

        //public string PathModel 
        //{
        //    get //==>@"C:\\Test\\Test.txt";
        //    {
        //        string pathExe = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //        var pathDirectory= Path.Combine(pathExe, "TestData");
        //        var directory = new DirectoryInfo(pathDirectory);
        //        // Создание в TESTDIR новых подкаталогов.
        //        if (!directory.Exists)
        //        {
        //            var subDirectory = new DirectoryInfo(pathExe);
        //            // Создаем директорию
        //            subDirectory.CreateSubdirectory("TestData");
        //        }
        //        return pathDirectory;
        //    }
        //}

    public void AddHour(InfoWork w)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User u)
        {
            throw new NotImplementedException();
        }

        public List<InfoWork> MakeReport(User u, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        void IModel.RecieveDataFromControllerReader()
        {
            throw new NotImplementedException();
        }

        void IModel.SentDataToControllerReader()
        {
            throw new NotImplementedException();
        }
    }
}