using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using wageсalculation.Persistance;
using wageсalculation.Persistance.Controllers;

namespace TestClass
{
    [TestFixture]

    class ModelTest
    {

        [Test]
        [Ignore("доделать левел")]
        public void ConvertFromLevelToStringInCorrectNewUserTest()
        {
            //вообще неправильно здесь делать заглушку т.к. она не для доп функционала используется
            //а просто для возможности тестирования метода
            //Model mod=new Model(new StubModel());    
            Assert.Catch(typeof(Exception), () => Model.ConvertFromLevelToString(new Level()),
                "Неизвестный пользователь!");
            var ex = Assert.Throws<Exception>(() => Model.ConvertFromLevelToString(new Level()));
            Assert.AreEqual("Неизвестный пользователь!", ex.Message);
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
            CurrentUser newUser = new CurrentUser("WTest2", Level.Worker);
            mod.AddUser(newUser);
            Assert.Contains(newUser, mod.Users);
            Assert.AreEqual(5, mod.Users.Count);
        }

        [Test]
        public void AddExistUserTest()
        {
            CurrentUser newUser = new CurrentUser("WTest", Level.Worker);
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
        public void RecieveDataWithEmptyUserListFromControllerDataTest1()
        {
            var ControllerDataMock = new Mock<IControllerData>();
            Model model = new Model(ControllerDataMock.Object);
            ControllerDataMock.Setup((cr) => cr.ReadData<CurrentUser>())
                .Returns(new List<CurrentUser>());
            TestDelegate result = () => model.RecieveDataFromControllerData();
            Assert.Catch(
                typeof(Exception),
                result,
                "Пользователей не существует!"
                );
            var ex = Assert.Throws<Exception>(result);
            Assert.AreEqual("Пользователей не существует!", ex.Message);
        }

        [Test]
        public void RecieveDataWithUknownUserInfoworksFromControllerDataTest1()
        {
            var ControllerDataMock = new Mock<IControllerData>();
            Model model = new Model(ControllerDataMock.Object);
            ControllerDataMock.Setup((cr) => cr.ReadData<CurrentUser>())
                .Returns(new List<CurrentUser> { new CurrentUser("HTest", Level.Head) });
            ControllerDataMock.Setup((cr) => cr.ReadData<InfoWork>())
                .Returns
                (new List<InfoWork> { new InfoWork(DateTime.Now, "UnKnownUser", 1, "Work") }//,
                );
            TestDelegate result = () => model.RecieveDataFromControllerData();
            Assert.Catch(typeof(Exception),result);
            var ex = Assert.Throws<Exception>(result);
            Assert.AreEqual("В файлах работ есть неизвестные пользователи!", ex.Message);
        }

        [Test]
        [Category("Unit")]
        public void DoNotSentDataToControllerDataTest1()
        {
            var ControllerReaderMock = new Mock<IControllerData>();
            //ControllerReaderMock.
            //создаем модель с заглушкой. В модели поля будут пустыми т.к. конструктор
            //у нас ничего не задает. вместо связи с классом для сохранения данных
            //ставится мок заглушка с пустым значением PathModel
            //в класс ControllerReader не заходит. Он заходит в мок заглушку
            //возращаемое значение в методе writeFiles мок ставит на по умолчанию
            //если метод возщает void то ответ null
            Model model = new Model(ControllerReaderMock.Object);
            //задаем что нам должен ответить метод WriteFiles из мока
            ControllerReaderMock.Setup((cr) => cr.WriteData<CurrentUser>(model.Users)).
                Throws(new Exception("Не удалось сохранить данные в файл!"));
            TestDelegate result = () => model.SentDataToControllerData();
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
