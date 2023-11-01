using System;
using System.Collections.Generic;

namespace wageсalculation.Persistance
{
    public class Role
    {  
        //класс для соединения сущностей Header, Worker, Freelancer вместе+для уменьшения
        //дублирования кода в этих сущностях
        public Wage wage;
        //public string[] methods;
        //public Action<InfoWork> del;
        public Dictionary<int, Command> Commands { get;} = new Dictionary<int, Command>();

        //возможные действия пользователя
        public readonly Dictionary<Command, string> mesRole = new Dictionary<Command, string>()
        {
            {Command.AddHour,"Добавить часы работы" },
            {Command.MakeReport,"Просмотреть отчет по отработанным часам"},
            {Command.AddUser,"Добавить сотрудника"},
            {Command.MakeReportInOtherUser,"Просмотреть отчет по конкретному сотруднику"},
            {Command.MakeReportAllUsers,"Просмотреть отчет по всем сотрудникам"},
            {Command.Exit,"Выход из программы"}
        };
    }
}