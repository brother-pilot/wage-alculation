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
        //string fileInfoWorksWorker;
        //string fileInfoWorksFreelancer;

        [OneTimeSetUp]
        public void Init()
        {
            var scr = new StubControllerReader();
            cr = new ControllerReaderFromFile(scr,scr.PathModel);
            mod = new StubModel();
            fileUser = Path.Combine(cr.PathModel, "users.csv");
            fileInfoWorks = Path.Combine(cr.PathModel, "infoWorks.csv");
            //fileInfoWorksWorker = Path.Combine(cr.PathModel, "infoWorksWorker.csv");
            //fileInfoWorksFreelancer = Path.Combine(cr.PathModel, "infoWorksFreelancer.csv");
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
            List<User> resultInfoWorks = cr.ReadData<InfoWork>();
            Assert.AreEqual(mod.InfoWorks.Count, resultInfoWorks.Count);
            for (int i = 0; i < mod.InfoWorks.Count; i++)
                Assert.IsTrue(mod.InfoWorks[0].Work == resultInfoWorks[0].Work);
            //(List<User>, List<InfoWork>, List<InfoWork>, List<InfoWork>) result = cr.ReadData();
            //Assert.AreEqual(mod.Users.Count, result.Item1.Count);
            //Assert.AreEqual(mod.InfoWorksHeader.Count, result.Item2.Count);
            //Assert.AreEqual(mod.InfoWorksWorker.Count, result.Item3.Count);
            //Assert.AreEqual(mod.InfoWorksFreelancer.Count, result.Item4.Count);
            //for (int i = 0; i < mod.Users.Count; i++)
            //    Assert.IsTrue(mod.Users[0].Name==result.Item1[0].Name);
            //for (int i = 0; i < mod.InfoWorksHeader.Count; i++)
            //    Assert.IsTrue(mod.InfoWorksHeader[0].Work==result.Item2[0].Work);
            //for (int i = 0; i < mod.InfoWorksWorker.Count; i++)
            //    Assert.IsTrue(mod.InfoWorksWorker[0].Work==result.Item3[0].Work);
            //for (int i = 0; i < mod.InfoWorksFreelancer.Count; i++)
            //    Assert.IsTrue(mod.InfoWorksFreelancer[0].Work==result.Item3[0].Work);
        }

        [Test]
        [Category("Unit")]
        public void Step3CheckDirectoryWasDeletedTest()
        {
            cr.DeleteDirectory();
            Assert.AreEqual(false, File.Exists(fileUser));
            Assert.AreEqual(false, File.Exists(fileInfoWorks));
            //Assert.AreEqual(false, File.Exists(fileInfoWorksWorker));
            //Assert.AreEqual(false, File.Exists(fileInfoWorksFreelancer));
        }

        [OneTimeTearDown]
        public void DeleteDirectory()
        {
            var directory = new DirectoryInfo(cr.PathModel);
            directory.Delete(true);
        }
    }

}
