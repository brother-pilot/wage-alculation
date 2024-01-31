using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wageсalculation.Persistance
{
    public interface IControllerData
    {
        (List<User>, List<InfoWork>, List<InfoWork>, List<InfoWork>) ReadData();
        bool WriteData(List<User> users, List<InfoWork> infoWorksHeader, 
            List<InfoWork> infoWorksWorker, List<InfoWork> infoWorksFreelancer);
    }
}
