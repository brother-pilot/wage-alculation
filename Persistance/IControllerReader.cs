using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wageсalculation.Persistance
{
    public interface IControllerReader
    {
        string PathModel { get; }
        (List<User>, List<InfoWork>, List<InfoWork>, List<InfoWork>) ReadFiles();
        bool WriteFiles(List<User> users, List<InfoWork> infoWorksHeader, 
            List<InfoWork> infoWorksWorker, List<InfoWork> infoWorksFreelancer);
    }
}
