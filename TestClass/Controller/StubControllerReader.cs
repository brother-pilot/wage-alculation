﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using wageсalculation.Persistance;
using wageсalculation.Persistance.Controllers;

namespace TestClass
{
    class StubControllerReader : IControllerData
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

        public List<T> ReadData<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public bool WriteData<T>(List<T> item) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
