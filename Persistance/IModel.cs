using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wageсalculation.Persistance
{
    public interface IModel
    {
        List<User> Users { get;  }
        List<InfoWork> InfoWorksHeader { get;  }
        List<InfoWork> InfoWorksWorker { get;  }
        List<InfoWork> InfoWorksFreelancer { get;  }
        void RecieveDataFromReadController();
        void SentDataToReadController();
        void AddHour(InfoWork w);
        void AddUser(User u);
        List<InfoWork> MakeReport(User u, DateTime from, DateTime to);
    }
}
