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
        protected List<T> ReadData<T>() where T : class
        {
            return manager.ReadData<T>();
        }

        protected bool WriteData<T>(List<T> item) where T : class
        {
            return manager.WriteData<T>(item);
        }
    }
}
