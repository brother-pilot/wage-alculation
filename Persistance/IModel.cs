using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wageсalculation.Persistance
{
    public interface IModel
    {
        List<CurrentUser> Users { get;  }
        List<InfoWork> InfoWorks { get;  }
        void RecieveDataFromControllerData();
        void SentDataToControllerData();
        void AddHour(InfoWork w);
        void AddUser(CurrentUser u);
        List<InfoWork> MakeReport(CurrentUser u, DateTime from, DateTime to);
    }
}
