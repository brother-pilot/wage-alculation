﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace wageсalculation.Persistance
{
    public class Model:IModel
    {
        IControllerReader controllerReader;
        /// <summary>
        /// Class for safe data
        /// </summary>
        List<User> users = new List<User>();
        List<InfoWork> infoWorksHeader = new List<InfoWork>();
        List<InfoWork> infoWorksWorker = new List<InfoWork>();
        List<InfoWork> infoWorksFreelancer = new List<InfoWork>();
        public List<User> Users { get { return users; } } 
        public List<InfoWork> InfoWorksHeader { get { return infoWorksHeader; } }

        public List<InfoWork> InfoWorksWorker { get { return infoWorksWorker; } } 
        public List<InfoWork> InfoWorksFreelancer { get { return infoWorksFreelancer; } } 

        public Model()
        {
            controllerReader = new ControllerReader();  
        }

        public void RecieveDataFromReadController()
        {
            (List<User>, List<InfoWork>, List<InfoWork>, List<InfoWork>) result
                 = controllerReader.ReadFiles();
            if (result.Item1 != null)
                users = result.Item1;
            if (result.Item2 != null)
                infoWorksHeader = result.Item2;
            if (result.Item3 != null)
                infoWorksWorker = result.Item3;
            if (result.Item4 != null)
                infoWorksFreelancer = result.Item4;
        }

        //конструктор для тестирования
        internal Model(IModel model)
        {
            users = model.Users;
            infoWorksHeader = model.InfoWorksHeader;
            infoWorksWorker = model.InfoWorksWorker;
            infoWorksFreelancer = model.InfoWorksFreelancer;
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
            if (Users.Find(u => u.name == w.Name).level==Level.Head)
                infoWorksHeader.Add(w);
            else if (Users.Find(u => u.name == w.Name).level == Level.Worker)
                infoWorksWorker.Add(w);
            else if (Users.Find(u => u.name == w.Name).level == Level.Freelancer)        
                infoWorksFreelancer.Add(w);
        }

        public void AddUser(User u)
        {
            while (Users.Exists(user => user.name == u.name))
            {
                throw new Exception("Такой пользователь уже есть!");
            }
            users.Add(u);
        }

        public List<InfoWork> MakeReport(User u,DateTime from, DateTime to)
        {
            List<InfoWork> works=new List<InfoWork>();
            if (Users.Find(user => user == u).level == Level.Head)
                works = infoWorksHeader.Where(w=>w.Data>=from&& w.Data<=to).ToList();
            else if (Users.Find(user => user == u).level == Level.Worker)
                works = infoWorksWorker.Where(w => w.Data >= from && w.Data <= to).ToList();
            else if (Users.Find(user => user == u).level == Level.Freelancer)
                works = infoWorksFreelancer.Where(w => w.Data >= from && w.Data <= to).ToList();
            return works;
        }

        public void SentDataToReadController()
        {
            controllerReader.WriteFiles(users, infoWorksHeader,infoWorksWorker, infoWorksFreelancer);
        }
    }
}