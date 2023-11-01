using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace wageсalculation.Persistance
{
    public class Model:IModel
    {
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
        public string PathModel { get;}

        public Model()
        {
            //инициируем путь для хранения файлов
            //узнаем путь к текущей папке проекта
            string pathExe = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Console.WriteLine(pathExe);
            PathModel = Path.Combine(pathExe, "Data");
        }

        public void ReadFiles()
        {
            var directory = new DirectoryInfo(PathModel);

            // Проверка на существование указанной директории.
            if (directory.Exists)
            {
                Console.WriteLine("Ищу файлы... в");
                Console.WriteLine("Директория с именем: {0}", directory.FullName);
                // Получаем все файлы с расширением .csv.
                FileInfo[] files = directory.GetFiles("*.csv");
                // Выводим информацию о каждом файле.
                foreach (FileInfo file in files)
                {
                    Console.WriteLine("File name : {0}", file.Name);
                    if (file.Name == "Users.csv")
                        ReadFileUser();
                    else
                        ReadOtherFile(PathModel, file);
                }
            }
            else
            {
                Console.WriteLine("Директория с именем: {0}  не существует.", directory.FullName);
            }
            
        }

        private void ReadOtherFile(string path, FileInfo file)
        {
            var fileUser = new FileInfo(Path.Combine(path, file.Name));
            // FileMode.OpenOrCreate - ЕСЛИ: существует ТО: открыть ИНАЧЕ: создать новый
            // FileAccess.Read - только для чтения,
            // FileShare.None - Совместный доступ - Нет.
            FileStream stream = fileUser.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
            StreamReader streamR = new StreamReader(stream);
            string line;
            while ((line = streamR.ReadLine()) != null)
            {

                if (file.Name == "infoWorksHeader.csv")
                    InfoWorksHeader.Add(new InfoWork(line));
                else if (file.Name == "infoWorksWorker.csv")
                    InfoWorksWorker.Add(new InfoWork(line));
                else if (file.Name == "infoWorksFreelancer.csv")
                    InfoWorksFreelancer.Add(new InfoWork(line));
            }
            stream.Close();
        }

        void ReadFileUser()
        {
            var fileUser = new FileInfo(Path.Combine(PathModel, "Users.csv"));
            Console.WriteLine(fileUser.DirectoryName);
            // FileMode.OpenOrCreate - ЕСЛИ: существует ТО: открыть ИНАЧЕ: создать новый
            // FileAccess.Read - только для чтения,
            // FileShare.None - Совместный доступ - Нет.
            FileStream stream = fileUser.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader streamR = new StreamReader(stream);
            string line;
            while ((line = streamR.ReadLine()) != null)
            {
                Level item2;
                //if (line.Split(',')[1] == "руководитель")
                //    item2 = Level.Head;
                //else if (line.Split(',')[1] == "работник")
                //    item2 = Level.Worker;
                //else if (line.Split(',')[1] == "фрилансер")
                //    item2 = Level.Freelancer;
                //else
                //{
                //    item2 = Level.None;
                //    Console.WriteLine("Не верная структура файла Users");
                //}
                switch (line.Split(',')[1])
                {
                    case "руководитель":
                        item2 = Level.Head;
                        break;
                    case "работник":
                        item2 = Level.Worker;
                        break;
                    case "фрилансер":
                        item2 = Level.Freelancer;
                        break;
                    default:
                        throw new NotImplementedException("Не верная структура файла Users");
                }
                users.Add(new User(line.Split(',')[0], item2));
            }
            stream.Close();
        }

        public void WriteFiles()
        {
            var directory = new DirectoryInfo(PathModel);

            // Проверка на существование указанной директории.
            if (directory.Exists)
            {
                Console.WriteLine("Сохраняю файлы...");
                // Получаем все файлы с расширением .csv.
                FileInfo[] files = directory.GetFiles("*.csv");
                // Выводим информацию о каждом файле.
                foreach (FileInfo file in files)
                {
                    Console.WriteLine("File name : {0}", file.Name);
                    if (file.Name == "Users.csv")
                        ReadFileUser();
                    else
                        ReadOtherFile(PathModel, file);
                }
            }
            var fileUser = new FileInfo(PathModel + @"..\..\Users.csv");
            // FileMode.OpenOrCreate - ЕСЛИ: существует ТО: открыть ИНАЧЕ: создать новый
            // FileAccess.Read - только для чтения,
            // FileShare.None - Совместный доступ - Нет.
            FileStream stream = fileUser.Open(FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            StreamWriter streamW = new StreamWriter(stream);
            Users.ForEach(u=>streamW.WriteLine(u.name+","+ ConvertFromLevelToString(u.level)));
            WriteOthersFiles(infoWorksHeader, "infoWorksHeader.csv");
            WriteOthersFiles(infoWorksWorker, "infoWorksWorker.csv");
            WriteOthersFiles(infoWorksFreelancer, "infoWorksFreelancer.csv");
        }

        private void WriteOthersFiles(List<InfoWork> list,string filename)
        {
            var fileUser = new FileInfo(PathModel + @"..\..\"+ filename);
            // FileMode.OpenOrCreate - ЕСЛИ: существует ТО: открыть ИНАЧЕ: создать новый
            // FileAccess.Read - только для чтения,
            // FileShare.None - Совместный доступ - Нет.
            FileStream stream = fileUser.Open(FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            StreamWriter streamW = new StreamWriter(stream);
            list.ForEach(x => streamW.WriteLine(x.Data + "," + x.Name+","+x.Time+","+x.Work));
        }

        public string ConvertFromLevelToString(Level level)
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

    }
}