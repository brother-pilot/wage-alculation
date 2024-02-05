using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace wageсalculation.Persistance
{
    public class Model:IModel
    {
        IControllerData controllerData;
        /// <summary>
        /// Class for safe data
        /// </summary>
        List<User> users = new List<User>();
        List<InfoWork> infoWorks = new List<InfoWork>();
        public List<User> Users { get { return users; } } 
        public List<InfoWork> InfoWorks { get { return infoWorks; } } 

        public Model()
        {
            controllerData = new ControllerReaderFromFile();  
        }

        public void RecieveDataFromControllerData()
        {
            List<User> result  = controllerData.ReadData<User>();
            if (result != null)
                users = result;
            else
                throw new Exception("Пользователей не существует!");
            List<InfoWork> resultInfoWork = controllerData.ReadData<InfoWork>();
            if (resultInfoWork.Exists(i => Users.Exists(u => u.Name != i. Name)))
             throw new Exception("В файлах работ есть неизвестные пользователи!");
            if (resultInfoWork != null)
                infoWorks = resultInfoWork;
        }

        //конструктор для тестирования
        internal Model(IModel model)
        {
            users = model.Users;
            infoWorks = model.InfoWorks;
        }

        //конструктор для тестирования
        internal Model(IControllerData cr)
        {
            controllerData = cr;
        }

        public static string ConvertFromLevelToString(Level level)
        {
            switch (level)
            {
                case Level.Head:
                    return "руководитель";
                    break;
                case Level.Worker:
                    return "работник";
                    break;
                case Level.Freelancer:
                    return "фрилансер";
                    break;
                default:
                    throw new Exception("Не известный пользователь!");
                    break;
            }
        }

        public void AddHour(InfoWork w)
        {
            infoWorks.Add(w);
        }

        public void AddUser(User u)
        {
            while (Users.Exists(user => user.Name == u.Name))
            {
                throw new Exception("Такой пользователь уже есть!");
            }
            users.Add(u);
        }

        public List<InfoWork> MakeReport(User u,DateTime from, DateTime to)
        {
            return infoWorks.Where(w => w.Name == u.Name && w.Data >= from && w.Data <= to).ToList();
        }

        public void SentDataToControllerData()
        {
            try
            {
                bool resultU= controllerData.WriteData<User>(users);
                bool resultI = controllerData.WriteData<InfoWork>(infoWorks);
                if (!resultU) throw new Exception("Не удалось сохранить данные!");
                if (!resultI) throw new Exception("Не удалось сохранить данные!");
            }
            catch
            {
               throw new Exception("Не удалось сохранить данные!");
            }
        }
    }
}