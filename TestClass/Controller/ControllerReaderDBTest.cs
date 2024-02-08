﻿using System;
using System.Collections.Generic;
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
                db.CurrentUsers.Add(mod.Users[0]);
                db.InfoWorks.Add(mod.InfoWorks[0]);
                db.SaveChanges();
                Assert.AreEqual(mod.Users[0], db.CurrentUsers.First());
                Assert.AreEqual(mod.InfoWorks[0], db.InfoWorks.First());
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