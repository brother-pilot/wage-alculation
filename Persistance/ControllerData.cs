using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wageсalculation.Persistance
{
    class ControllerData
    {

        private readonly IControllerData manager = new ControllerReaderFromFile();
        protected (List<User>, List<InfoWork>, List<InfoWork>, List<InfoWork>) ReadData()
        {
            return manager.ReadData();
        }

        protected bool WriteData(List<User> users, List<InfoWork> infoWorksHeader, List<InfoWork> infoWorksWorker, List<InfoWork> infoWorksFreelancer)
        {
            return manager.WriteData(users, infoWorksHeader, infoWorksWorker, infoWorksFreelancer);
        }
    }
}
