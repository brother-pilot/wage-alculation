using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("TestClass")]
namespace wageсalculation.Persistance
{
    public class ControllerReader: IControllerReader
    {
        public string PathModel { get; }
        public ControllerReader()
        {
            //инициируем путь для хранения файлов
            //узнаем путь к текущей папке проекта
            string pathExe = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Console.WriteLine(pathExe);
            PathModel = Path.Combine(pathExe, "Data");
        }

        internal ControllerReader(IControllerReader controllerReader)
        {
            PathModel = controllerReader.PathModel;
        }

        public (List<User>, List<InfoWork>, List<InfoWork>, List<InfoWork>) ReadFiles()
        {
            var directory = new DirectoryInfo(PathModel);
            List<User> item1=new List<User>();
            var items=new List<List<InfoWork>>();
            // Проверка на существование указанной директории.
            if (directory.Exists)
            {
                Console.WriteLine("Ищу файлы... в");
                Console.WriteLine("Директория с именем: {0}", directory.FullName);
                // Получаем все файлы с расширением .csv.
                FileInfo[] files = directory.GetFiles("*.csv");
                //if ()
                // считываем файлы
                int i = 0;
                foreach (FileInfo file in files)
                {
                    Console.WriteLine("File name : {0}", file.Name);
                    if (file.Name == "Users.csv")
                        item1= ReadFileUser();
                    else
                        items=ReadOtherFile(PathModel, file);
                }
            }
            else
            {
                Console.WriteLine("Директория с именем: {0}  не существует.", directory.FullName);
            }
            return (item1, items[0], items[1], items[2]);

        }

        private List<List<InfoWork>> ReadOtherFile(string path, FileInfo file)
        {
            List<InfoWork> InfoWorksHeader = new List<InfoWork>();
            List<InfoWork> InfoWorksWorker = new List<InfoWork>();
            List<InfoWork> InfoWorksFreelancer = new List<InfoWork>();
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
            return new List<List<InfoWork>> { InfoWorksHeader, InfoWorksWorker, InfoWorksFreelancer };
        }

        List<User> ReadFileUser()
        {
            List<User> users = new List<User>();
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
            return users;
        }

        public void WriteFiles(List<User> users, List<InfoWork> infoWorksHeader,
            List<InfoWork> infoWorksWorker, List<InfoWork> infoWorksFreelancer)
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
            //var fileUser = new FileInfo(PathModel + @"..\..\Users.csv");
            var fileUser = new FileInfo(Path.Combine(PathModel, "Users.csv"));
            // FileMode.OpenOrCreate - ЕСЛИ: существует ТО: открыть ИНАЧЕ: создать новый
            // FileAccess.Read - только для чтения,
            // FileShare.None - Совместный доступ - Нет.
            FileStream stream = fileUser.Open(FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            StreamWriter streamW = new StreamWriter(stream);
            users.ForEach(u => streamW.WriteLine(u.name + "," + Model.ConvertFromLevelToString(u.level)));
            streamW.Close();
            WriteOthersFiles(infoWorksHeader, "infoWorksHeader.csv");
            WriteOthersFiles(infoWorksWorker, "infoWorksWorker.csv");
            WriteOthersFiles(infoWorksFreelancer, "infoWorksFreelancer.csv");
        }

        private void DeleteFiles(DirectoryInfo directory)
        {
            try
            {
                //будут ли удалены также и все вложенные подкаталоги
                directory.Delete(true);
                Console.WriteLine("Каталоги успешно удалены.");
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        private void WriteOthersFiles(List<InfoWork> list, string filename)
        {
            //var fileUser = new FileInfo(PathModel + @"..\..\"+ filename);
            var fileUser = new FileInfo(Path.Combine(PathModel, filename));
            // FileMode.OpenOrCreate - ЕСЛИ: существует ТО: открыть ИНАЧЕ: создать новый
            // FileAccess.Read - только для чтения,
            // FileShare.None - Совместный доступ - Нет.
            FileStream stream = fileUser.Open(FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            StreamWriter streamW = new StreamWriter(stream);
            list.ForEach(x => streamW.WriteLine(x.Data + "," + x.Name + "," + x.Time + "," + x.Work));
            streamW.Close();
        }
    }
}
