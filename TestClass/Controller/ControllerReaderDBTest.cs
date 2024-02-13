using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using wageсalculation.Persistance;

namespace TestClass.Controller
{
    class ControllerReaderDBTest
    {
        IModel mod;

        [OneTimeSetUp]
        public void Init()
        {
            mod = new StubModel();
        }

        [Test]
        [Category("Itegration")]
        public void StepCheckDataExistsAfterSaveTest()
        {
            using (var db = new ContextForTest())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.CurrentUsers.AddRange(mod.Users);
                db.InfoWorks.AddRange(mod.InfoWorks);
                db.SaveChanges();
                Assert.AreEqual(mod.Users[0].Name, db.CurrentUsers.First().Name);
                Assert.AreEqual(mod.Users[0].Level, db.CurrentUsers.First().Level);
                Assert.AreEqual(mod.Users[0].Role.Commands, db.CurrentUsers.First().Role.Commands);
                Assert.AreEqual(mod.Users[0].Role.Wage.MonthWage, db.CurrentUsers.First().Role.Wage.MonthWage);
                Assert.AreEqual(mod.InfoWorks[0].Name, db.InfoWorks.First().Name);
                Assert.AreEqual(mod.InfoWorks[0].Data, db.InfoWorks.First().Data);
                Assert.AreEqual(mod.InfoWorks[0].Time, db.InfoWorks.First().Time);
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (var db = new ContextForTest())
            {
                db.Database.EnsureDeleted();
            }
        }
    }
}
