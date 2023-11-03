using System;
using System.Collections.Generic;
using wageсalculation.Persistance;
using System.Linq;

namespace wageсalculation.Domain
{
    public class Controller:IController
    {
        private Model mod;
        //активный юзер под которым произошел логин
        private User user;
        private IView view;//для передачи данных из приложения в UI

        //Role userType;
        //Dictionary<string, Action<object>> command;
        //Важно! commandAccessKey
        Dictionary<int, string> commandAccessKey = new Dictionary<int, string>();
        //string action;
        public Controller(IView v, Model model)
        {
            mod = model;
            mod.RecieveDataFromReadController();
            view = v;
            user=Logon();
            InitilizeUserCommand();
            while (true)
            {
                view.ShowDo(commandAccessKey);
                var key = ReadAction();
                DoCommand(key);
            }
        }

        public User Logon()
        {
            string name = InputName();
            return mod.Users.Find(u => u.name == name);
        }

        private string InputName()
        {
            view.Status = "Пожалуйста, введите имя:";
            Console.WriteLine("Пожалуйста, введите имя:");
            string name = Console.ReadLine();
            while (!mod.Users.Exists(u => u.name == name))
            {
                Console.WriteLine("Ошибка! Введено неизвестное имя");
                name = Console.ReadLine();
            }
            return name;
        }

        Command ReadAction()
        {
            int kafn = -1;
            //если введено неправильно но ходим по циклу
            while (!Int32.TryParse(Console.ReadLine(), out kafn) || !commandAccessKey.ContainsKey(kafn))
                Console.WriteLine("Неправильная команда. Введите номер еще раз");
            return (Command)kafn-1;
            //ttt("sdf");
            //Console.WriteLine(commandAccessKey[kafn]);
        }

        private void DoCommand(Command key)
        {
            switch (key)
            {
                case Command.Exit:
                    //action("Завершение работы программы...");
                    StopProgram("Завершение работы программы...");
                    break;
                case Command.AddHour:
                    PrepareAddHour();
                    break;
                case Command.AddUser:
                    PrepareAddUser();
                    break;
                case Command.MakeReport:
                    PrepareReport(user);
                    break;
                case Command.MakeReportAllUsers:
                    PrepareReportAllUsers();
                    break;
                case Command.MakeReportInOtherUser:
                    PrepareReportInOtherUser();
                    break;

            }
        }

        public void PrepareAddUser()
        {
            Console.WriteLine("Пожалуйста, введите имя нового пользователя с заглавной буквы:");
            string name = Console.ReadLine();
            while (mod.Users.Exists(u => u.name == name))
            {
                Console.WriteLine("Ошибка! Введено занятое имя");
                name = Console.ReadLine();
            }
            Console.WriteLine("Выберите роль пользователя(введите номер):");
            Array levels = Level.GetValues(typeof(Level));
            foreach (var item in levels)
            {
                Console.WriteLine("(" + (int)item + ") " +Model.ConvertFromLevelToString((Level)item));
            }
            int kafn = -1;
            //если введено неправильно но ходим по циклу
            while (!Int32.TryParse(Console.ReadLine(), out kafn) || kafn<0||kafn>levels.Length-1)
                Console.WriteLine("Неправильная команда. Введите номер еще раз");
            mod.AddUser(new User(name, (Level)kafn));
        }

        public void PrepareReportInOtherUser()
        {
            User user = Logon();
            PrepareReport(user);
        }

        public void PrepareReportAllUsers()
        {
            DateTime[] dates = InputDates();
            var from = dates[0];
            var to = dates[1];
            List<(List<InfoWork>,int,decimal)> res = new List<(List<InfoWork>, int, decimal)>();
            foreach (var user in mod.Users)
            {
                var item = mod.MakeReport(user, from, to);
                int time = item.Sum(i => i.Time);
                decimal wage = user.role.wage.PayWage(time);
                res.Add((item, time, wage));
            }
            view.PrintFullReport(from, to, res);
        }

        public void PrepareReport(User user)
        {
            DateTime[] dates=InputDates();
            var from = dates[0];
            var to = dates[1];
            var res=mod.MakeReport(user,from,to);
            int time = res.Sum(i => i.Time);
            decimal wage=user.role.wage.PayWage(time);       
            view.PrintReport(from,to,res,time,wage);
        }

        private DateTime[] InputDates()
        {
            DateTime[] dates = new DateTime[2];
            Console.WriteLine("Введите дату начала отчета в формате ГГГГ.ММ.ДД:");
            dates[0] = InputDate();
            Console.WriteLine("Введите дату конца отчета в формате ГГГГ.ММ.ДД:");
            dates[1] = InputDate();
            return dates;
        }

        private DateTime InputDate()
        {
            DateTime date;
            while (!DateTime.TryParse(Console.ReadLine(), out date))
                Console.WriteLine("Неправильная команда. Введите номер еще раз");
            return date;
        }

        public void PrepareAddHour()
        {
            Console.WriteLine("Добавляем часы работы. Введите дату работы в формате ГГГГ.ММ.ДД");
            DateTime dt;
            while (!DateTime.TryParse(Console.ReadLine(), out dt))
                Console.WriteLine("Неправильная команда. Введите номер еще раз");
            string name;
            if (user.role.GetType() == typeof(Header))
            {
                Console.WriteLine("Введите имя пользователя");
                name = Console.ReadLine();
                while (!mod.Users.Exists(u => u.name == name))
                {
                    Console.WriteLine("Ошибка! Введено неизвестное имя");
                    name = Console.ReadLine();
                }
            }
            else
                name = user.name;
            Console.WriteLine("Введите количество отработанных часов");
            int hours = -1;
            //если введено неправильно но ходим по циклу
            while (!Int32.TryParse(Console.ReadLine(), out hours) || hours < 1 || hours > 24)
                Console.WriteLine("Количество часов должно быть целым числом от 1 до 24");
            Console.WriteLine("Введение описание работы");
            string work = Console.ReadLine();
            mod.AddHour(new InfoWork(dt, name, hours, work));
        }

        

        private void InitilizeUserCommand()
        {
            //command["Exit"] = (s) => StopProgram(s as string);
            int i = 0;
            foreach (var item in user.role.Commands)
                commandAccessKey[item.Key] = user.role.mesRole[item.Value] ;
        }
        void StopProgram(string message)
        {
            mod.SentDataToReadController();
            Console.WriteLine(message);
            Console.WriteLine("Нажмите enter для выхода из программы!");
            Console.ReadLine();
            Environment.Exit(0);
        }

        
    }
}