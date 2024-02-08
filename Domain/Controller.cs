using System;
using System.Collections.Generic;
using wageсalculation.Persistance;
using System.Linq;
using wageсalculation;

namespace wageсalculation.Domain
{
    public class Controller:IController
    {
        private Model mod;
        //активный юзер под которым произошел логин
        private CurrentUser currentUser;
        private IView view;//для передачи данных из приложения в UI

        //Role userType;
        //Dictionary<string, Action<object>> command;
        //Важно! commandAccessKey
        Dictionary<int, string> commandAccessKey = new Dictionary<int, string>();
        //string action;
        public Controller(IView v, Model model)
        {
            mod = model;
            mod.RecieveDataFromControllerData();
            view = v;
            currentUser = Logon();
            InitilizeUserCommand();
            while (true)
            {
                view.ShowDo(commandAccessKey);
                var key = ReadCommand();
                DoCommand(key);
            }
        }

        public CurrentUser Logon()
        {
            string name = InputName();
            return mod.Users.Find(u => u.Name == name);
        }

        private string InputName()
        {
            string name=view.ReadNotEmptyLine("Введите имя пользователя программы");
            while (!mod.Users.Exists(u => u.Name == name))
            {
                view.WriteErrorMessage("Ошибка! Введено неизвестное имя");
                name = view.ReadNotEmptyLine("Введите имя пользователя программы");
            }
            return name;
        }

        
        Command ReadCommand()
        {
            int kafn = -1;
            //если введено неправильно то ходим по циклу
            while (!Int32.TryParse(view.ReadNotEmptyLine("Введите команду"), out kafn) || 
                !commandAccessKey.ContainsKey(kafn))
                view.WriteErrorMessage("Неправильная команда. Введите номер еще раз");
            return (Command)kafn-1;
        }

        //как еще можно было сделать команды, но у меня разные доступные команды у разных пользоывателй
        ////Command ReadCommand()
        //{
        //    while (true)
        //    {
        //        var input = view.ReadIntLine("");
        //        if (Enum.TryParse(input, true, out Command command))
        //        {
        //            return command;
        //        }

        //        view.WriteErrorMessage("Не известная команда. Введите номер еще раз");
        //    }
        //}

        private void DoCommand(Command key)
        {
            switch (key)
            {
                case Command.Exit:
                    StopProgram("Завершение работы программы...");
                    break;
                case Command.AddHour:
                    PrepareAddHour();
                    break;
                case Command.AddUser:
                    PrepareAddUser();
                    break;
                case Command.MakeReport:
                    PrepareReport(currentUser);
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
            string name = view.ReadNotEmptyLine("Введите имя нового пользователя с заглавной буквы");
            while (mod.Users.Exists(u => u.Name == name))
            {
                view.WriteErrorMessage("Ошибка! Введено занятое имя");
                name = view.ReadNotEmptyLine("имя нового пользователя с заглавной буквы");
            }
            view.ShowMessage("Выберите роль пользователя(введите номер)");
            Array levels = Level.GetValues(typeof(Level));
            foreach (var item in levels)
            {
                view.ShowMessage("(" + (int)item + ") " +Model.ConvertFromLevelToString((Level)item));
            }
            int kafn = -1;
            //если введено неправильно но ходим по циклу
            while (!Int32.TryParse(view.ReadNotEmptyLine(" "), out kafn) ||
                kafn < 0 || kafn > levels.Length)
                Console.WriteLine("Неправильная команда. Введите номер еще раз");
            mod.AddUser(new CurrentUser(name, (Level)kafn));
        }

        public void PrepareReportInOtherUser()
        {
            CurrentUser user = Logon();
            PrepareReport(user);
        }

        public void PrepareReportAllUsers()
        {
            //DateTime[] dates = InputDates();
            var from = view.ReadNotEmptyDateTime("Введите дату начала отчета в формате ГГГГ.ММ.ДД");
            var to = view.ReadNotEmptyDateTime("Введите дату конца отчета в формате ГГГГ.ММ.ДД");
            List<(List<InfoWork>,int,decimal)> res = new List<(List<InfoWork>, int, decimal)>();
            foreach (var user in mod.Users)
            {
                var item = mod.MakeReport(user, from, to);
                int time = item.Sum(i => i.Time);
                decimal wage = user.Role.wage.PayWage(time);
                res.Add((item, time, wage));
            }
            view.PrintFullReport(from, to, res);
        }

        public void PrepareReport(CurrentUser currentuser)
        {
            //DateTime[] dates=InputDates();
            var from = view.ReadNotEmptyDateTime("Введите дату начала отчета в формате ГГГГ.ММ.ДД");
            var to = view.ReadNotEmptyDateTime("Введите дату конца отчета в формате ГГГГ.ММ.ДД");
            var res=mod.MakeReport(currentuser, from,to);
            int time = res.Sum(i => i.Time);
            decimal wage= currentuser.Role.wage.PayWage(time);       
            view.PrintReport(from,to,res,time,wage);
        }

        //private DateTime[] InputDates()
        //{
        //    DateTime[] dates = new DateTime[2];
        //    Console.WriteLine("Введите дату начала отчета в формате ГГГГ.ММ.ДД:");
        //    dates[0] = InputDate();
        //    Console.WriteLine("Введите дату конца отчета в формате ГГГГ.ММ.ДД:");
        //    dates[1] = InputDate();
        //    return dates;
        //}

        

        public void PrepareAddHour()
        {
            DateTime dt=view.ReadNotEmptyDateTime("Добавляем часы работы. Введите дату работы в формате ГГГГ.ММ.ДД");
            string name;
            if (currentUser.Role.GetType() == typeof(Header))
            {
                name=view.ReadNotEmptyLine("Введите имя пользователя");
                while (!mod.Users.Exists(u => u.Name == name))
                {
                    view.WriteErrorMessage("Ошибка! Введено неизвестное имя");
                    name = view.ReadNotEmptyLine("Введите имя пользователя");
                }
            }
            else
                name = currentUser.Name;
            int hours = -1;
            //если введено неправильно но ходим по циклу
            while (!Int32.TryParse(view.ReadNotEmptyLine("Введите количество отработанных часов"), out hours) || hours < 1 || hours > 24)
                view.WriteErrorMessage("Количество часов должно быть целым числом от 1 до 24");
            string work = view.ReadNotEmptyLine("Введение описание работы");
            mod.AddHour(new InfoWork(dt, name, hours, work));
        }

        

        private void InitilizeUserCommand()
        {
            //command["Exit"] = (s) => StopProgram(s as string);
            foreach (var item in currentUser.Role.Commands)
                commandAccessKey[item.Key] = currentUser.Role.mesRole[item.Value] ;
        }
        void StopProgram(string message)
        {
            mod.SentDataToControllerData();
            view.ShowMessage(message);
            Environment.Exit(0);
        }

        
    }
}