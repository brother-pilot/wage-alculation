using System;
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
    }
}