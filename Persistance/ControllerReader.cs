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
        private FileInfo[] files = new FileInfo[4];
        public ControllerReader()
        {
            //инициируем путь для хранения файлов
            //узнаем путь к текущей папке проекта
            string pathExe = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Console.WriteLine(pathExe);
            PathModel = Path.Combine(pathExe, "Data");
            InitializeListFilesForSave();
        }

        

        //для тестирования
        internal ControllerReader(IControllerReader controllerReader)
        {
            PathModel = controllerReader.PathModel;
            InitializeListFilesForSave();
        }

        private void InitializeListFilesForSave()
        {
            string[] filesString = new string[4]
            {
            "users.csv",
            "infoWorksHeader.csv",
            "infoWorksWorker.csv",
            "infoWorksFreelancer.csv"
            };
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = new FileInfo(Path.Combine(PathModel, filesString[i]));
            }
        }

        public (List<User>, List<InfoWork>, List<InfoWork>, List<InfoWork>) ReadFiles()
        {
            var directory = new DirectoryInfo(PathModel);
            List<User> item1=new List<User>();
            var items=new List<InfoWork>[3];
            // Проверка на существование указанной директории.
            if (directory.Exists)
            {
                Console.WriteLine("Ищу файлы... в");
                Console.WriteLine("Директория с именем: {0}", directory.FullName);
                // Получаем все файлы с расширением .csv.
                if (directory.GetFiles("*.csv").Length!=4)
                    throw new Exception("В директории не все файлы!");
                // считываем файлы
                item1=ReadFileUser();
                for (int i = 0; i <= 2; i++)
                    items[i] = ReadOtherFile(files[i+1]);
            }
            else
            {
                Console.WriteLine("Директория с именем: {0}  не существует.", directory.FullName);
                throw new Exception("Директория отсутсвует!");
            }
            return (item1, items[0], items[1], items[2]);

        }

        private List<InfoWork> ReadOtherFile(FileInfo file)
        {
            List<InfoWork> result = new List<InfoWork>();
            // FileMode.OpenOrCreate - ЕСЛИ: существует ТО: открыть ИНАЧЕ: создать новый
            // FileAccess.Read - только для чтения,
            // FileShare.None - Совместный доступ - Нет.
            FileStream stream = file.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
            StreamReader streamR = new StreamReader(stream);
            string line;
            while ((line = streamR.ReadLine()) != null)
            {
                result.Add(new InfoWork(line));
            }
            stream.Close();
            return result;
        }

        List<User> ReadFileUser()
        {
            List<User> users = new List<User>();
            // FileMode.OpenOrCreate - ЕСЛИ: существует ТО: открыть ИНАЧЕ: создать новый
            // FileAccess.Read - только для чтения,
            // FileShare.None - Совместный доступ - Нет.
            FileStream stream = files[0].Open(FileMode.Open, FileAccess.Read, FileShare.None);
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

        public bool WriteFiles(List<User> users, List<InfoWork> infoWorksHeader,
            List<InfoWork> infoWorksWorker, List<InfoWork> infoWorksFreelancer)
        {
            try
            {
                // FileMode.OpenOrCreate - ЕСЛИ: существует ТО: открыть ИНАЧЕ: создать новый
                // FileAccess.Read - только для чтения,
                // FileShare.None - Совместный доступ - Нет.
                FileStream stream = files[0].Open(FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                StreamWriter streamW = new StreamWriter(stream);
                users.ForEach(u => streamW.WriteLine(u.Name + "," + Model.ConvertFromLevelToString(u.Level)));
                streamW.Close();
                WriteOthersFiles(infoWorksHeader, files[1]);
                WriteOthersFiles(infoWorksWorker, files[2]);
                WriteOthersFiles(infoWorksFreelancer, files[3]);
                return true;
            }
            catch (Exception)
            {
                throw new Exception("Не удалось сохранить данные в файл!"); 
            }
            
        }

        internal void DeleteDirectory()
        {
            var directory = new DirectoryInfo(PathModel);
            try
            {
                //будут ли удалены также и все вложенные подкаталоги
                directory.Delete(true);
                Console.WriteLine("Каталоги успешно удалены.");
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        private void WriteOthersFiles(List<InfoWork> list, FileInfo filename)
        {          
            // FileMode.OpenOrCreate - ЕСЛИ: существует ТО: открыть ИНАЧЕ: создать новый
            // FileAccess.Read - только для чтения,
            // FileShare.None - Совместный доступ - Нет.
            FileStream stream = filename.Open(FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            StreamWriter streamW = new StreamWriter(stream);
            list.ForEach(x => streamW.WriteLine(x.Data + "," + x.Name + "," + x.Time + "," + x.Work));
            streamW.Close();
        }
    }
}
