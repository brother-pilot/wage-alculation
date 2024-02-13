using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace wageсalculation.Persistance.Controllers
{
    public class ControllerData:IControllerData
    {

        private readonly IControllerData managerReaderFromFile = new ControllerReaderFromFile();
        private readonly IControllerData managerReaderDB = new ControllerReaderDB();

        public List<T> ReadData<T>() where T : class
        {
            var dataFromFile = managerReaderFromFile.ReadData<T>();
            var dataFromDB = managerReaderDB.ReadData<T>();
            if (dataFromFile.Count == dataFromDB.Count&& dataFromFile[0].Equals(dataFromDB[0])
                && dataFromFile[dataFromFile.Count-1].Equals(dataFromDB.Count - 1))
                return dataFromDB;
            else
                throw new NotImplementedException("Разные данные в файлах и БД!");
        }

        public bool WriteData<T>(List<T> item) where T : class
        {
            var statusFromFile = managerReaderFromFile.WriteData<T>(item);
            var statusFromDB = managerReaderDB.WriteData<T>(item);
            if (statusFromFile && statusFromDB)
                return true;
            else
                throw new NotImplementedException("Не удалось сохранить данные!");
        }
    }
}
