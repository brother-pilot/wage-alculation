using System;
using NUnit.Framework;
using wageсalculation.Persistance;


namespace TestClass
{
    class UserTest
    {
        [Test]
        public void InCorrectNewUser()
        {
            Assert.Catch(typeof(Exception), () => new User("H", Level.Test), "Неизвестная роль!");
        }
    }
}
