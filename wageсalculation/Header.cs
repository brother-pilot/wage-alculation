using System;
using System.Collections.Generic;

namespace wageсalculation
{
    public class Header<T>: Role<T>, IDo
    {
        public Header() 
        {
            wage = new Wage(120000, 20000);
            methods = new string[5];
            methods[0] = "AddHour";
            methods[1] = "MakeReport";
            methods[2] = "AddUser";
            methods[3] = "MakeReportInOtherUser";
            methods[4] = "MakeReportAllUsers";
            //del += w=> AddHour(w);
            commands = new Dictionary<string, Action<T>>();
            commands["AddHour"] = w => AddHour(w);
        }


        public void AddHour(InfoWork work)
        {
            Console.WriteLine("bola"); ;
        }
        public void MakeReport(DateTime startData, DateTime endData)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void MakeReportInOtherUser(DateTime startData, DateTime endData, User user)
        {
            throw new NotImplementedException();
        }

        public void MakeReportAllUsers(DateTime startData, DateTime endData)
        {
            throw new NotImplementedException();
        }
    }
}