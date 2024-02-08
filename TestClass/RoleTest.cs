using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using wageсalculation.Persistance;

namespace TestClass
{
    class RoleTest
    {
        [Test]
        public void AccordanceMesRoleWithNumberCommandTest()
        {
            var enumCount = Enum.GetNames(typeof(Command)).Length;
            Assert.AreEqual(enumCount, new UserRole().mesRole.Count);
        }
    }
}
