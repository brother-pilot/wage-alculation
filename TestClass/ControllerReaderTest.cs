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
        ControllerReader cr;
        IModel mod;
        string fileUser;
        string fileInfoWorksHeader;
        string fileInfoWorksWorker;
        string fileInfoWorksFreelancer;

        [OneTimeSetUp]
        public void Init()
        {
            cr = new ControllerReader(new StubControllerReader());
            mod = new StubModel();
            fileUser = Path.Combine(cr.PathModel, "users.csv");
            fileInfoWorksHeader = Path.Combine(cr.PathModel, "infoWorksHeader.csv");
            fileInfoWorksWorker = Path.Combine(cr.PathModel, "infoWorksWorker.csv");
            fileInfoWorksFreelancer = Path.Combine(cr.PathModel, "infoWorksFreelancer.csv");
        }

        [Test]
        [Category("Itegration")]
        public void Step1CheckFilesExistsAfterSaveTest()
        {
            cr.WriteFiles(mod.Users, mod.InfoWorksHeader, mod.InfoWorksWorker, mod.InfoWorksFreelancer);
            Assert.AreEqual(true, File.Exists(fileUser));
            Assert.AreEqual(true, File.Exists(fileInfoWorksHeader));
            Assert.AreEqual(true, File.Exists(fileInfoWorksWorker));
            Assert.AreEqual(true, File.Exists(fileInfoWorksFreelancer));
        }

        [Test]
        [Category("Itegration")]
        public void Step2CheckReadingFilesAfterSaveTest()
        {
            (List<User>, List<InfoWork>, List<InfoWork>, List<InfoWork>) result = cr.ReadFiles();
            Assert.AreEqual(mod.Users.Count, result.Item1.Count);
            Assert.AreEqual(mod.InfoWorksHeader.Count, result.Item2.Count);
            Assert.AreEqual(mod.InfoWorksWorker.Count, result.Item3.Count);
            Assert.AreEqual(mod.InfoWorksFreelancer.Count, result.Item4.Count);
            for (int i = 0; i < mod.Users.Count; i++)
                Assert.IsTrue(mod.Users[0] == result.Item1[0]);
            for (int i = 0; i < mod.InfoWorksHeader.Count; i++)
                Assert.IsTrue(mod.InfoWorksHeader[0] == result.Item2[0]);
            for (int i = 0; i < mod.InfoWorksWorker.Count; i++)
                Assert.IsTrue(mod.InfoWorksWorker[0] == result.Item3[0]);
            for (int i = 0; i < mod.InfoWorksFreelancer.Count; i++)
                Assert.IsTrue(mod.InfoWorksFreelancer[0] == result.Item3[0]);
        }

        [Test]
        [Category("Unit")]
        public void Step3CheckDirectoryWasDeletedTest()
        {
            cr.DeleteDirectory();
            Assert.AreEqual(false, File.Exists(fileUser));
            Assert.AreEqual(false, File.Exists(fileInfoWorksHeader));
            Assert.AreEqual(false, File.Exists(fileInfoWorksWorker));
            Assert.AreEqual(false, File.Exists(fileInfoWorksFreelancer));
        }

        [OneTimeTearDown]
        public void DeleteDirectory()
        {
            var directory = new DirectoryInfo(cr.PathModel);
            directory.Delete(true);
        }
    }

}
