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
        CurrentUser Logon();
        void PrepareAddHour();
        void PrepareAddUser();
        void PrepareReport(CurrentUser user);
        void PrepareReportInOtherUser();
        void PrepareReportAllUsers();
    }
}
