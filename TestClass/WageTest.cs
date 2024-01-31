using NUnit.Framework;
using System;
using wageсalculation.Persistance;
using AutoFixture;

namespace TestClass
{
    public class WageTests
    {

        [Test]
        public void WageForAnyTypeWorkerTest1()
        {
            Assert.Catch(typeof(Exception), ()=>new Wage(0, 0), 
                "При работе по окладу зарплата не может быть 0");
        }

        [Test]
        public void WageForAnyTypeWorkerTest2()
        {
            Assert.Catch(typeof(Exception), () => new Wage(0, 0,false,false,0),
                "При работе по часовой оплате оплата часа не может быть 0");
        }



        [TestCase(1, 0, true, false, 1)]
        [TestCase(1, 0, false, false, 1)]
        public void WageForAnyTypeWorkerTest3(decimal monthWage, decimal bonus,
            bool isMonthWage, bool isBonus, decimal hourWage)
        {
            Assert.Catch(typeof(Exception), () => 
                new Wage(monthWage, bonus, isMonthWage, isBonus, hourWage),
                "Работник должен работать или по окладу или по часовой ставке");
        }

        [Test]
        [Repeat(25)]
        public void WageForAnyTypeWorkerWithFixture()
        {
            
                Fixture fixture = new Fixture();
                decimal monthWage = fixture.Create<decimal>() + 1;
                decimal bonus = fixture.Create<decimal>();
                bool isMonthWage = fixture.Create<bool>();
                bool isBonus = fixture.Create<bool>();
                decimal hourWage = 1;
                Assert.Catch(typeof(Exception), () =>
                    new Wage(monthWage, bonus, isMonthWage, isBonus, hourWage),
                    "Работник должен работать или по окладу или по часовой ставке");  
        }

        [TestCase(1250,1)]
        [TestCase(200000, 160)]
        public void WageMonthForHeaderTest1(decimal result,int time)
        {
            var user = new User("H",Level.Head);
            Assert.AreEqual(result,user.Role.wage.PayWage(time));
        }

        
        [TestCase(200000 + 125, 160 + 1)]
        [TestCase(200000 + 20000, 160 + 160)]
        [TestCase(200000 + 20000 + 125, 160 + 161)]
        public void WageMonthAndBonusForHeaderTest1(decimal result, int time)
        {
            var user = new User("H", Level.Head);
            Assert.AreEqual(result, user.Role.wage.PayWage(time));
        }

        [TestCase(750,1)]
        [TestCase(120000, 160)]
        public void WageMonthForWorkerTest1(decimal result, int time)
        {
            var user = new User("W", Level.Worker);
            Assert.AreEqual(result, user.Role.wage.PayWage(time));
        }

        [TestCase(120000 + 750, 160+1)]
        [TestCase(120000*2, 160 + 160)]
        [TestCase(120000 * 2 + 750, 160 + 161)]
        public void WageMonthAndBonusForWorkerTest1(decimal result, int time)
        {
            var user = new User("W", Level.Worker);
            Assert.AreEqual(result, user.Role.wage.PayWage(time));
        }


        [TestCase(1000, 1)]
        [TestCase(1000*160, 160)]
        [TestCase(1000 * 160, 161)]
        public void WageForFreelancerTest1(decimal result, int time)
        {
            var user = new User("F", Level.Freelancer);
            Assert.AreEqual(result, user.Role.wage.PayWage(time));
        }
    }
}