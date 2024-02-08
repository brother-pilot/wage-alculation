using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wageсalculation.Persistance.Controllers
{
    public interface IControllerData
    {
        List<T> ReadData<T>() where T : class;
        bool WriteData<T>(List<T> item) where T : class;
    }
}
