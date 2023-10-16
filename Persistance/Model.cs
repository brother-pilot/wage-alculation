using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace wageсalculation
{
    internal class Model
    {
        public List<User> users = new List<User>();
        public List<InfoWork> infoWorksHeader = new List<InfoWork>();
        public List<InfoWork> infoWorksWorker = new List<InfoWork>();
        public List<InfoWork> infoWorksFreelancer = new List<InfoWork>();
        string path;

        public Model()
        {
            //инициируем путь для хранения файлов
            //узнаем путь к текущей папке проекта
            string pathExe = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Console.WriteLine(pathExe);
            path = Path.Combine(pathExe, "Data");
            ReadFiles();
        }

        void ReadFiles()
        {
            var directory = new DirectoryInfo(path);

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
                        ReadOtherFile(path, file);
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
                    infoWorksHeader.Add(new InfoWork(line));
                else if (file.Name == "infoWorksWorker.csv")
                    infoWorksWorker.Add(new InfoWork(line));
                else if (file.Name == "infoWorksFreelancer.csv")
                    infoWorksFreelancer.Add(new InfoWork(line));
            }
            stream.Close();
        }

        void ReadFileUser()
        {
            var fileUser = new FileInfo(Path.Combine(path, "Users.csv"));
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
                        item2 = Level.None;
                        Console.WriteLine("Не верная структура файла Users");
                        break;
                }
                users.Add(new User(line.Split(',')[0], item2));
            }
            stream.Close();
        }

        public void WriteFiles()
        {
            var directory = new DirectoryInfo(path);

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
                        ReadOtherFile(path, file);
                }
            }
            var fileUser = new FileInfo(path + @"..\..\Users.csv");
            // FileMode.OpenOrCreate - ЕСЛИ: существует ТО: открыть ИНАЧЕ: создать новый
            // FileAccess.Read - только для чтения,
            // FileShare.None - Совместный доступ - Нет.
            FileStream stream = fileUser.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
            StreamWriter streamW = new StreamWriter(stream);
            users.ForEach(u=>streamW.WriteLine(u.name+","+ ConvertFromLevelToString(u.level)));
            WriteOthersFiles(infoWorksHeader, "infoWorksHeader.csv");
            WriteOthersFiles(infoWorksWorker, "infoWorksWorker.csv");
            WriteOthersFiles(infoWorksFreelancer, "infoWorksFreelancer.csv");
        }

        private void WriteOthersFiles(List<InfoWork> list,string filename)
        {
            var fileUser = new FileInfo(path + @"..\..\"+ filename);
            // FileMode.OpenOrCreate - ЕСЛИ: существует ТО: открыть ИНАЧЕ: создать новый
            // FileAccess.Read - только для чтения,
            // FileShare.None - Совместный доступ - Нет.
            FileStream stream = fileUser.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
            StreamWriter streamW = new StreamWriter(stream);
            list.ForEach(x => streamW.WriteLine(x.Data + "," + x.Name+","+x.Time+","+x.Work));
        }

        string ConvertFromLevelToString(Level level)
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
                    return "";
                    break;
            }
        }

    }
}