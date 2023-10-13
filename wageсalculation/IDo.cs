using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wageсalculation
{
    public interface IDo
    {
        //void RegisterCommand(string command, Action<IDo> execute);
        void MakeReport(DateTime startData, DateTime endData);
        void AddHour(InfoWork work);
        
    }
}
