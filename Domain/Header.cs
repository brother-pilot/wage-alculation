using System;
using System.Collections.Generic;

namespace wageсalculation
{
    public class Header: Role
    {
        public Header() 
        {
            wage = new Wage(120000, 20000);
            commands = new Dictionary<string, Action<object>>();
            commands["AddHour"] = w => AddHour(w as InfoWork);
            commands["MakeReport"] = ti => MakeReport(ti as TimeInterval);
            commands["AddUser"] = u => AddUser(u as User);
            commands["MakeReportInOtherUser"] = ud => MakeReportInOtherUser(ud as UserData);
            commands["MakeReportAllUsers"] = ti => MakeReportAllUsers(ti as TimeInterval);
        }


        public void AddHour(InfoWork work)
        {
            Console.WriteLine("bola");
            w=>mod.
        }
        public void MakeReport(TimeInterval ti)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void MakeReportInOtherUser(UserData ud)
        {
            throw new NotImplementedException();
        }

        public void MakeReportAllUsers(TimeInterval ti)
        {
            throw new NotImplementedException();
        }
    }
}