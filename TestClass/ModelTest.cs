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

    class ModelTest
    {

        [Test]
        public void ConvertFromLevelToStringInCorrectNewUserTest()
        {
            //вообще неправильно здесь делать заглушку т.к. она не для доп функционала используется
            //а просто для возможности тестирования метода
            //Model mod=new Model(new StubModel());    
            Assert.Catch(typeof(Exception), () => Model.ConvertFromLevelToString(Level.Test),
                "Не известный пользователь!");
            var ex = Assert.Throws<Exception>(() => Model.ConvertFromLevelToString(Level.Test));
            Assert.AreEqual("Не известный пользователь!", ex.Message);
        }

        Model mod;

        //[TestFixtureSetUp]
        [SetUp]
        public void Init()
        {
            mod = new Model(new StubModel());
        }

        [Test]
        public void AddAnotherUserTest()
        {
            User newUser = new User("WTest2", Level.Worker);
            mod.AddUser(newUser);
            Assert.Contains(newUser, mod.Users);
            Assert.AreEqual(4, mod.Users.Count);
        }

        [Test]
        public void AddExistUserTest()
        {
            User newUser = new User("WTest", Level.Worker);
            Assert.Catch(
                typeof(Exception),
                () => mod.AddUser(newUser),
                "Такой пользователь уже есть!"
                );
            var ex = Assert.Throws<Exception>(() => mod.AddUser(newUser));
            Assert.AreEqual("Такой пользователь уже есть!", ex.Message);
        }

        [Test]
        public void MakeReportTest()
        {
            var result = mod.MakeReport(mod.Users.FirstOrDefault(), DateTime.Today, DateTime.Today);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(44, result.Sum(iw=>iw.Time));
        }

        
        [Test]
        //[Ignore("TO DO")]
        public void RecieveDataWithEmptyUserListFromControllerReaderTest1()
        {
            var ControllerReaderMock = new Mock<IControllerReader>();
            Model model = new Model(ControllerReaderMock.Object);
            TestDelegate result = () => model.RecieveDataFromControllerReader();
            Assert.Catch(
                typeof(Exception),
                result,
                "Пользователей не существует!"
                );
            var ex = Assert.Throws<Exception>(result);
            Assert.AreEqual("Пользователей не существует!", ex.Message);
        }

        [Test]
        [Category("Unit")]
        public void DoNotSentDataToControllerReaderTest1()
        {
            var ControllerReaderMock = new Mock<IControllerReader>();
            //ControllerReaderMock.
            //создаем модель с заглушкой. В модели поля будут пустыми т.к. конструктор
            //у нас ничего не задает. вместо связи с классом для сохранения данных
            //ставится мок заглушка с пустым значением PathModel
            //в класс ControllerReader не заходит. Он заходит в мок заглушку
            //возращаемое значение в методе writeFiles мок ставит на по умолчанию
            //если метод возщает void то ответ null
            Model model = new Model(ControllerReaderMock.Object);
            //задаем что нам должен ответить метод WriteFiles из мока
            ControllerReaderMock.Setup((cr) => cr.WriteFiles(model.Users, model.InfoWorksHeader,
                model.InfoWorksWorker, model.InfoWorksFreelancer)).
                Throws(new Exception("Не удалось сохранить данные в файл!"));
            TestDelegate result = () => model.SentDataToControllerReader();
            //внимание!!! проверяет только тип исключения!!!
            Assert.Catch(
                typeof(Exception),
                result,
                "Не удалось сохранить данные!"
            );
            Assert.Throws(
                typeof(Exception),
                result,
                "Не удалось сохранить данные!"
            );
            var ex = Assert.Throws<Exception>(result);
            Assert.AreEqual("Не удалось сохранить данные!", ex.Message);
        }
    }
}
