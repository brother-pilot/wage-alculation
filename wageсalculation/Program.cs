using System;
using System.Collections.Generic;
using wageсalculation.Domain;
using wageсalculation.Persistance;

namespace wageсalculation
{
    class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var mod = new Model();
            var form = new FormConsole(mod);
            //Application.Run(form);

        }


       

  
    }
}
