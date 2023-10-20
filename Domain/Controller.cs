using System;
using System.Collections.Generic;
using wageсalculation.Persistance;

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
            mod.ReadFiles();
            view = v;
            Logon();
            InitilizeUserCommand();
            while (true)
            {
                view.ShowDo(commandAccessKey);
                var key = ReadAction();
                DoCommand(key);
            }
        }

        public void Logon()
        {
            view.Status = "Пожалуйста, введите имя:";
            Console.WriteLine("Пожалуйста, введите имя:");
            string name = Console.ReadLine();
            while (!mod.users.Exists(u => u.name == name))
            {
                Console.WriteLine("Ошибка! Введено неизвестное имя");
                name = Console.ReadLine();
            }
            user= mod.users.Find(u => u.name == name);
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
                    AddHour();
                    break;
                case Command.MakeReport:
                    ;
                    break;
            }
        }

        public void AddHour()
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
                while (!mod.users.Exists(u => u.name == name))
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
            mod.infoWorksHeader.Add(new InfoWork(dt, name, hours, work));
        }

        

        private void InitilizeUserCommand()
        {
            //command["Exit"] = (s) => StopProgram(s as string);
            int i = 0;
            foreach (var item in user.role.commands)
                commandAccessKey[item.Key] = user.role.mesRole[item.Value] ;
        }
        void StopProgram(string message)
        {
            mod.WriteFiles();
            Console.WriteLine(message);
            Console.WriteLine("Нажмите enter для выхода из программы!");
            Console.ReadLine();
            Environment.Exit(0);
        }

        
    }
}