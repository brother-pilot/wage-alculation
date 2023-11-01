using NUnit.Framework;
using System;
using wageсalculation.Persistance;

namespace TestClass
{
    public class WageTests
    {

        [Test]
        public void WageForAnyTypeWorkerTest1()
        {
            Assert.Catch(typeof(Exception), ()=>new Wage(0, 0), "При работе по окладу зарплата не может быть 0");
        }

        [Test]
        public void WageForAnyTypeWorkerTest2()
        {
            Assert.Catch(typeof(Exception), () => new Wage(0, 0,false,false,0),
                "При работе по часовой оплате оплата часа не может быть 0");
        }

        [Test]
        public void WageForAnyTypeWorkerTest3()
        {
            Assert.Catch(typeof(Exception), () => new Wage(1, 0, true, false, 1),
                "Работник должен работать или по окладу или по часовой ставке");
        }

        [Test]
        public void WageForAnyTypeWorkerTest4()
        {
            Assert.Catch(typeof(Exception), () => new Wage(1, 0, false, false, 1),
                "Работник должен работать или по окладу или по часовой ставке");
        }

        [Test]
        public void WageMonthForHeaderTest1()
        {
            var user = new User("H",Level.Head);
            decimal result = 1250;
            Assert.AreEqual(result,user.role.wage.PayWage(1));
        }

        [Test]
        public void WageMonthForHeaderTest2()
        {
            var user = new User("H", Level.Head);
            decimal result = 200000;
            Assert.AreEqual(result, user.role.wage.PayWage(160));
        }

        [Test]
        public void WageMonthAndBonusForHeaderTest1()
        {
            var user = new User("H", Level.Head);
            decimal result = 200000+125;
            Assert.AreEqual(result, user.role.wage.PayWage(160+1));
        }

        [Test]
        public void WageMonthAndBonusForHeaderTest2()
        {
            var user = new User("H", Level.Head);
            decimal result = 200000+20000;
            Assert.AreEqual(result, user.role.wage.PayWage(160+160));
        }

        [Test]
        public void WageMonthAndBonusForHeaderTest3()
        {
            var user = new User("H", Level.Head);
            decimal result = 200000 + 20000+125;
            Assert.AreEqual(result, user.role.wage.PayWage(160 + 161));
        }

        [Test]
        public void WageMonthForWorkerTest1()
        {
            var user = new User("W", Level.Worker);
            decimal result = 750;
            Assert.AreEqual(result, user.role.wage.PayWage(1));
        }

        [Test]
        public void WageMonthForWorkerTest2()
        {
            var user = new User("W", Level.Worker);
            decimal result = 120000;
            Assert.AreEqual(result, user.role.wage.PayWage(160));
        }

        [Test]
        public void WageMonthAndBonusForWorkerTest1()
        {
            var user = new User("W", Level.Worker);
            decimal result = 120000 + 750;
            Assert.AreEqual(result, user.role.wage.PayWage(160 + 1));
        }

        [Test]
        public void WageMonthAndBonusForWorkerTest2()
        {
            var user = new User("W", Level.Worker);
            decimal result = 120000*2;
            Assert.AreEqual(result, user.role.wage.PayWage(160 + 160));
        }

        [Test]
        public void WageMonthAndBonusForWorkerTest3()
        {
            var user = new User("W", Level.Worker);
            decimal result = 120000 *2 + 750;
            Assert.AreEqual(result, user.role.wage.PayWage(160 + 161));
        }

        [Test]
        public void WageForFreelancerTest1()
        {
            var user = new User("F", Level.Freelancer);
            decimal result = 1000;
            Assert.AreEqual(result, user.role.wage.PayWage(1));
        }

        [Test]
        public void WageForFreelancerTest2()
        {
            var user = new User("F", Level.Freelancer);
            decimal result = 1000*160;
            Assert.AreEqual(result, user.role.wage.PayWage(160));
        }

        [Test]
        public void WageForFreelancerTest3()
        {
            var user = new User("F", Level.Freelancer);
            decimal result = 1000 * 160;
            Assert.AreEqual(result, user.role.wage.PayWage(161));
        }
    }
}