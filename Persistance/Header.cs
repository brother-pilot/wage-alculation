﻿using System;
using System.Collections.Generic;

namespace wageсalculation.Persistance
{
    public class Header: Role
    {
        public Header() 
        {
            wage = new Wage(120000, 20000);
            //commands = new Dictionary<string, Action<object>>();
            //commands["AddHour"] = w => AddHour(w as InfoWork);
            //commands["MakeReport"] = ti => MakeReport(ti as TimeInterval);
            //commands["AddUser"] = u => AddUser(u as User);
            //commands["MakeReportInOtherUser"] = ud => MakeReportInOtherUser(ud as UserData);
            //commands["MakeReportAllUsers"] = ti => MakeReportAllUsers(ti as TimeInterval);
            commands = new Dictionary<int, Command>();
            commands[1] = Command.AddHour;
            commands[2] = Command.MakeReport;
            commands[6] = Command.Exit;
            commands[3] = Command.AddUser;
            commands[4] = Command.MakeReportInOtherUser;
            commands[5] = Command.MakeReportAllUsers;
        }

        
    }
}