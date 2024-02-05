using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using wageсalculation.Persistance;

namespace TestClass
{
    [TestFixture]
    class ControllerReaderTest
    {
        ControllerReaderFromFile cr;
        IModel mod;
        string fileUser;
        string fileInfoWorks;

        [OneTimeSetUp]
        public void Init()
        {
            var scr = new StubControllerReader();
            cr = new ControllerReaderFromFile(scr,scr.PathModel);
            mod = new StubModel();
            fileUser = Path.Combine(cr.PathModel, "users.csv");
            fileInfoWorks = Path.Combine(cr.PathModel, "infoWorks.csv");
        }

        [Test]
        [Category("Itegration")]
        public void Step1CheckFilesExistsAfterSaveTest()
        {
            cr.WriteData<User>(mod.Users);
            cr.WriteData<InfoWork>(mod.InfoWorks);
            Assert.AreEqual(true, File.Exists(fileUser));
            Assert.AreEqual(true, File.Exists(fileInfoWorks));
        }

        [Test]
        [Category("Itegration")]
        public void Step2CheckReadingFilesAfterSaveTest()
        {
            List<User> result = cr.ReadData<User>();
            Assert.AreEqual(mod.Users.Count, result.Count);
            for (int i = 0; i < mod.Users.Count; i++)
                Assert.IsTrue(mod.Users[0].Name == result[0].Name);
            List<InfoWork> resultInfoWorks = cr.ReadData<InfoWork>();
            Assert.AreEqual(mod.InfoWorks.Count, resultInfoWorks.Count);
            for (int i = 0; i < mod.InfoWorks.Count; i++)
                Assert.IsTrue(mod.InfoWorks[0].Work == resultInfoWorks[0].Work);
        }

        [Test]
        [Category("Unit")]
        public void Step3CheckDirectoryWasDeletedTest()
        {
            cr.DeleteDirectory();
            Assert.AreEqual(false, File.Exists(fileUser));
            Assert.AreEqual(false, File.Exists(fileInfoWorks));
        }

        [OneTimeTearDown]
        public void DeleteDirectory()
        {
            var directory = new DirectoryInfo(cr.PathModel);
            directory.Delete(true);
        }
    }

}
