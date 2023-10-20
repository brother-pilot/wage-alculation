using System;
using System.Collections.Generic;
using wageсalculation.Persistance;

namespace wageсalculation.Domain
{
    public class Controller
    {
        //возможные действия пользователя
        Dictionary<string, string> mesRole = new Dictionary<string, string>()
        {
            {"AddHour","Добавить часы работы" },
            {"MakeReport","Просмотреть отчет по отработанным часам"},
            {"AddUser","Добавить сотрудника"},
            {"MakeReportInOtherUser","Просмотреть отчет по конкретному сотруднику"},
            {"MakeReportAllUsers","Просмотреть отчет по всем сотрудникам"},
            {"Exit","Выход из программы"}
        };

        private Model mod;
        //активный юзер под которым произошел логин
        private User user;
        private IView view;//для передачи данных из приложения в UI

        //Role userType;
        Dictionary<string, Action<object>> command;
        Dictionary<int, string> commandKey = new Dictionary<int, string>();
        //string action;
        public Controller(IView v, Model model)
        {
            mod = model;
            mod.ReadFiles();
            view = v;
            view.Status="Пожалуйста, введите имя:";
            Console.WriteLine("Пожалуйста, введите имя:");
            string name = Console.ReadLine();
            while (!mod.users.Exists(u => u.name == name))
            {
                Console.WriteLine("Ошибка! Введено неизвестное имя");
                name = Console.ReadLine();
            }
            user = mod.users.Find(u => u.name == name);
            InitilizeCommand();
            while (true)
            {
                view.ShowDo(commandKey);
                var key = ReadAction();
                DoCommand(key);
            }
        }

        int ReadAction()
        {
            int kafn = -1;
            //если введено неправильно но ходим по циклу
            while (!Int32.TryParse(Console.ReadLine(), out kafn) || kafn < 1 || kafn >= command.Count + 1)
                Console.WriteLine("Неправильная команда. Введите номер еще раз");
            return kafn;
            //ttt("sdf");
            //Console.WriteLine(commandKey[kafn]);
        }

        private void DoCommand(int key)
        {
            var action = command[commandKey[key]];
            switch (commandKey[key])
            {
                case "Exit":
                    action("Завершение работы программы...");
                    break;
                case "AddHour":
                    AddHour(action);
                    break;
                case "MakeReport":
                    ;
                    break;
            }
        }

        public static void AddHour(Action<object> action)
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

        

        private void InitilizeCommand()
        {
            command = user.role.commands;
            command["Exit"] = (s) => StopProgram(s as string);
            int i = 0;
            foreach (var item in command)
                commandKey[++i] = item.Key;
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