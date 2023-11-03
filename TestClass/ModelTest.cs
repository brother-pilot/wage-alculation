using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using wageсalculation.Persistance;

namespace TestClass
{
    [TestFixture]

    class ModelTest
    {

        [Test]
        public void ConvertFromLevelToStringInCorrectNewUser()
        {
            //вообще неправильно здесь делать заглушку т.к. она не для доп функционала используется
            //а просто для возможности тестирования метода
            //Model mod=new Model(new StubModel());    
            Assert.Catch(typeof(Exception), () => Model.ConvertFromLevelToString(Level.Test), "Не известный пользователь!");
        }

        Model mod;

        //[TestFixtureSetUp]
        [SetUp]
        public void Init()
        {
            mod = new Model(new StubModel());
        }

        [Test]
        public void AddAnotherUser()
        {
            User newUser = new User("WTest2", Level.Worker);
            mod.AddUser(newUser);
            Assert.Contains(newUser, mod.Users);
            Assert.AreEqual(4, mod.Users.Count);
        }

        [Test]
        public void AddExistUser()
        {
            User newUser = new User("WTest", Level.Worker);
            Assert.Catch(
                typeof(Exception),
                () => mod.AddUser(newUser),
                "Такой пользователь уже есть!"
                );
        }

        [Test]
        public void MakeReport()
        {
            var result = mod.MakeReport(mod.Users.FirstOrDefault(), DateTime.Today, DateTime.Today);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(44, result.Sum(iw=>iw.Time));
        }

        

        //[TearDown]
    }
}
