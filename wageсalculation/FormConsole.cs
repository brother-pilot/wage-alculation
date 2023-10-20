using System;
using System.Collections.Generic;
using wageсalculation.Domain;
using wageсalculation.Persistance;

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
    }
}