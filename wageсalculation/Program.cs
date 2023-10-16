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
            //var c=new Controller(mod)
            var form = new FormConsole(mod);
            //Application.Run(form);

        }


       


        //private void CastToRole()
        //{
        //    switch (user.level)
        //    { 
        //        case Level.Head:
        //            userType=user.role as Header;
        //            break;
        //        case Level.Worker:
        //            userType = user.role as Worker;
        //            break;
        //        case Level.Freelancer:
        //            userType = user.role as Freelancer;
        //            break;
        //    }
        //}
  
    }
}
