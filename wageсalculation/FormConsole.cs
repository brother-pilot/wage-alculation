using System;
using System.Collections.Generic;
using wageсalculation.Domain;
using wageсalculation.Persistance;
using System.Linq;

namespace wageсalculation
{
    internal class FormConsole:IView
    {
        Controller c;
        string labelStatus;

        public FormConsole(Model mod)
        {
            this.c = new Controller(this, mod);
        }

        public string Status 
        { get => labelStatus; set => this.labelStatus = value; }

        public void ShowDo(Dictionary<int, string> commandAccessKey)
        {
            Console.WriteLine("Выберите желаемое действие (введите номер)");
            foreach (var item in commandAccessKey)
                Console.WriteLine("(" + item.Key + ") " + item.Value);
        }

        public void PrintReport(DateTime from,DateTime to, List<InfoWork> res,int time,decimal wage)
        {
            Console.WriteLine("Отчет по сотруднику:"+res.FirstOrDefault().Name+ " за период c"+
                from.Date+" по"+ to.Date);
            foreach (var item in res)
            {
                Console.WriteLine(item.Data.Date+", "+item.Time+"часов, "+item.Work);
            }
            Console.WriteLine("Итого: "+time+" часов, заработано: "+wage+"руб");
        }

        public void PrintFullReport(DateTime from, DateTime to, List<(List<InfoWork>, int, decimal)> res)
        {
            Console.WriteLine("Отчет по сотрудникам: за период c" + from.Date + " по" + to.Date);
            int allTime = 0;
            decimal allWage = 0;
            foreach (var item in res)
            {
                allTime += item.Item2;
                allWage += item.Item3;
                Console.WriteLine(item.Item1.FirstOrDefault().Name+"отработал" + item.Item2+
                    " часов и заработал за период "+item.Item3+ " руб");
            }
            Console.WriteLine("Всего часов отработано за период: "+ allTime+", сумма выплате"+ 
                allWage + " руб");
        }
    }
}