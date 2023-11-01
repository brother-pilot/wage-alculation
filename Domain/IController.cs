using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wageсalculation.Persistance;

namespace wageсalculation.Domain
{
    interface IController
    {
        User Logon();
        void PrepareAddHour();
        void PrepareAddUser();
        void PrepareReport(User user);
        void PrepareReportInOtherUser();
        void PrepareReportAllUsers();
    }
}
