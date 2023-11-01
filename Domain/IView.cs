using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wageсalculation.Persistance;

namespace wageсalculation.Domain
{
    public interface IView
    {

        //строка состояния
        string Status { get; set; }

        public void ShowDo(Dictionary<int, string> commandAccessKey) { }

        void PrintReport(DateTime from, DateTime to,List<InfoWork> res, int time, decimal wage);
        void PrintFullReport(DateTime from, DateTime to, List<(List<InfoWork>, int, decimal)> res);
    }
}
