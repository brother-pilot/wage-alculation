using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using wageсalculation.Persistance;

namespace TestClass
{
    class StubControllerReader : IControllerReader
    {


        private string pathModel;
        public string PathModel => pathModel;
       
        public StubControllerReader()
        {
            string pathExe = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var pathDirectory = Path.Combine(pathExe, "TestData");
            var directory = new DirectoryInfo(pathDirectory);
            // Создание в TESTDIR новых подкаталогов.
            if (!directory.Exists)
            {
                var subDirectory = new DirectoryInfo(pathExe);
                // Создаем директорию
                subDirectory.CreateSubdirectory("TestData");
            }
            pathModel = pathDirectory;
        }

        public (List<User>, List<InfoWork>, List<InfoWork>, List<InfoWork>) ReadFiles()
        {
            throw new NotImplementedException();
        }

        public bool WriteFiles(List<User> users, List<InfoWork> infoWorksHeader, List<InfoWork> infoWorksWorker, List<InfoWork> infoWorksFreelancer)
        {
            throw new NotImplementedException();
        }
    }
}
