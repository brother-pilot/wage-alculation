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
        void ShowMessage(string message);
        void ShowDo(Dictionary<int, string> commandAccessKey);

        string ReadNotEmptyLine(string title);
        int ReadIntLine(string title);
        void WriteErrorMessage(string message);

        DateTime ReadNotEmptyDateTime(string title);

        void PrintReport(DateTime from, DateTime to,List<InfoWork> res, int time, decimal wage);
        void PrintFullReport(DateTime from, DateTime to, List<(List<InfoWork>, int, decimal)> res);
    }
}
