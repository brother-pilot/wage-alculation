using System;
using System.Collections.Generic;

namespace wageсalculation
{
    class Program
    {
        Dictionary<string, string> mesRole = new Dictionary<string, string>()
        {
            {"AddHour","Добавить часы работы"},
            {"MakeReport","Просмотреть отчет по отработанным часам"},
            {"AddUser","Добавить сотрудника"},
            {"MakeReportInOtherUser","Просмотреть отчет по конкретному сотруднику"},
            {"MakeReportAllUsers","Просмотреть отчет по всем сотрудникам"}
        };

        static Model mod;
        static User user;
        
        Role userType;
        Dictionary<string, Action<InfoWork>> command; 

        static void Main(string[] args)
        {
            var p = new Program();
            p.InitialazeData();
            Console.WriteLine("Пожалуйста, введите имя:");
            string name= Console.ReadLine();
            while (!mod.users.Exists(u=>u.name== name))
            {
                Console.WriteLine("Ошибка! Введено неизвестное имя");
                name = Console.ReadLine();
            }
            user = mod.users.Find(u => u.name == name);
            p.CastToRole();
            p.ShowDo();
            p.InitilizeCommand();
        }

        private void CastToRole()
        {
            switch (user.level)
            {
                case Level.Head:
                    userType=user.role as Header<T>;
                    break;
                case Level.Worker:
                    userType = user.role as Worker;
                    break;
                case Level.Freelancer:
                    userType = user.role as Freelancer;
                    break;
            }
        }

        private void ShowDo()
        {
            for (int i = 0; i < user.role.methods.Length; i++)
            {
                Console.WriteLine("("+i+") "+ mesRole[user.role.methods[i]]);
            }
            Console.WriteLine("(" + user.role.methods.Length+1 + ") Выход из программы");
        }

        private void InitilizeCommand()
        {
            //user.role.RegisterCommand("AddHour", user.role.del);
            //command = new Dictionary<string, Action<InfoWork>>();

            for (int i = 0; i < user.role.methods.Length; i++)
            {
                command = user.role.commands;
                //command[mesRole[user.role.methods[i]]] = user.role.methods[i];
            }
            Console.WriteLine("(" + user.role.methods.Length + 1 + ") Выход из программы");
        }

            void InitialazeData()
        {
            mod = new Model();       
        }
    }
}
