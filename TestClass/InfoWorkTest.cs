using NUnit.Framework;
using System;
using wageсalculation.Persistance;

namespace TestClass
{
    class InfoWorkTest
    {
        [Test]
        public void NewWorkWithInCorrectTimeTest1()
        {
            Assert.Catch(
                typeof(ArgumentOutOfRangeException),
                () => new InfoWork(DateTime.Now,"X",-1,"Work"),
                "Время работы должно быть от 0 до 24"
                );
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new InfoWork(DateTime.Now, "X", -1, "Work"));
            Assert.AreEqual("Время работы должно быть от 0 до 24", ex.ParamName);
        }

        [Test]
        public void NewWorkWithInCorrectTimeTest2()
        {
            Assert.Catch(
                typeof(ArgumentOutOfRangeException),
                () => new InfoWork(DateTime.Now, "X", 25, "Work"),
                "Время работы должно быть от 0 до 24"
                );
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new InfoWork(DateTime.Now, "X", 25, "Work"));
            Assert.AreEqual("Время работы должно быть от 0 до 24", ex.ParamName);
        }

        [Test]
        public void NewWorkWithInCorrectTimeTest3()
        {
            Assert.Catch(
                typeof(ArgumentOutOfRangeException),
                () => new InfoWork("2023.10.01,X,-1,Work"),
                "Время работы должно быть от 0 до 24"
                );
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new InfoWork("2023.10.01,X,-1,Work"));
            Assert.AreEqual("Время работы должно быть от 0 до 24", ex.ParamName);
        }
        [Test]
        public void NewWorkWithCorrectLineTest()
        {
            Assert.DoesNotThrow(()=>new InfoWork("2023.10.01,X,1,4"));
        }

        [Test]
        public void NewWorkWithInCorrectTimeTest4()
        {
            Assert.Catch(
                typeof(ArgumentOutOfRangeException),
                () => new InfoWork("2023.10.01,X, ,Work"),
                "Время работы должно быть числом от 0 до 24"
                );
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new InfoWork("2023.10.01,X, ,Work"));
            Assert.AreEqual("Время работы должно быть числом от 0 до 24", ex.ParamName);
        }

        [Test]
        public void NewWorkWithInCorrectLineTest1()
        {
            Assert.Catch(
                typeof(Exception),
                () => new InfoWork("2023.10.01,X,1"),
                "Неверный формат данных строки!"
                );
            var ex = Assert.Throws<Exception>(() => new InfoWork("2023.10.01,X,1"));
            Assert.AreEqual("Неверный формат данных строки!", ex.Message);
        }

        [Test]
        public void NewWorkWithInCorrectLineTest2()
        {
            Assert.Catch(
                typeof(Exception),
                () => new InfoWork("2023.10.01,X,1,4,5"),
                "Неверный формат данных строки!"
                );
            var ex = Assert.Throws<Exception>(() => new InfoWork("2023.10.01,X,1,4,5"));
            Assert.AreEqual("Неверный формат данных строки!", ex.Message);
        }

        //[Test]
        //public void NewWorkWithInCorrectLineTest3()
        //{
        //    Assert.Catch(
        //        typeof(Exception),
        //        () => new InfoWork("2023.10.1,X,1,4"),
        //        "Неверный формат данных строки!"
        //        );
        //}
    }
}
