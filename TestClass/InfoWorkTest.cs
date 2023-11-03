using NUnit.Framework;
using System;
using wageсalculation.Persistance;

namespace TestClass
{
    class InfoWorkTest
    {
        [Test]
        public void NewWorkWithInCorrectTime1()
        {
            Assert.Catch(
                typeof(ArgumentOutOfRangeException),
                () => new InfoWork(DateTime.Now,"X",-1,"Work"),
                "Время работы должно быть от 0 до 24"
                );
        }

        [Test]
        public void NewWorkWithInCorrectTime2()
        {
            Assert.Catch(
                typeof(ArgumentOutOfRangeException),
                () => new InfoWork(DateTime.Now, "X", 25, "Work"),
                "Время работы должно быть от 0 до 24"
                );
        }

        [Test]
        public void NewWorkWithInCorrectTime3()
        {
            Assert.Catch(
                typeof(ArgumentOutOfRangeException),
                () => new InfoWork("2023.10.01,X,-1,Work"),
                "Время работы должно быть от 0 до 24"
                );
        }
        [Test]
        public void NewWorkWithCorrectLine()
        {
            Assert.DoesNotThrow(()=>new InfoWork("2023.10.01,X,1,4"));
        }

        [Test]
        public void NewWorkWithInCorrectTime4()
        {
            Assert.Catch(
                typeof(ArgumentOutOfRangeException),
                () => new InfoWork("2023.10.01,X, ,Work"),
                "Время работы должно быть числом от 0 до 24"
                );
        }

        [Test]
        public void NewWorkWithInCorrectLine1()
        {
            Assert.Catch(
                typeof(Exception),
                () => new InfoWork("2023.10.01,X,1"),
                "Неверный формат данных строки!"
                );
        }

        [Test]
        public void NewWorkWithInCorrectLine2()
        {
            Assert.Catch(
                typeof(Exception),
                () => new InfoWork("2023.10.01,X,1,4,5"),
                "Неверный формат данных строки!"
                );
        }

        //[Test]
        //public void NewWorkWithInCorrectLine3()
        //{
        //    Assert.Catch(
        //        typeof(Exception),
        //        () => new InfoWork("2023.10.1,X,1,4"),
        //        "Неверный формат данных строки!"
        //        );
        //}
    }
}
