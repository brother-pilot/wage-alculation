using System;
using System.Collections.Generic;

namespace wageсalculation.Persistance
{
    public class Header: UserRole
    {
        public Header() 
        {
            wage = new Wage(200000, 20000);
            //commands = new Dictionary<string, Action<object>>();
            //commands["AddHour"] = w => AddHour(w as InfoWork);
            //commands["MakeReport"] = ti => MakeReport(ti as TimeInterval);
            //commands["AddUser"] = u => AddUser(u as User);
            //commands["MakeReportInOtherUser"] = ud => MakeReportInOtherUser(ud as UserData);
            //commands["MakeReportAllUsers"] = ti => MakeReportAllUsers(ti as TimeInterval);
            //commands = new Dictionary<int, Command>();
            Commands[1] = Command.AddHour;
            Commands[2] = Command.MakeReport;
            Commands[6] = Command.Exit;
            Commands[3] = Command.AddUser;
            Commands[4] = Command.MakeReportInOtherUser;
            Commands[5] = Command.MakeReportAllUsers;
        }

        
    }
}