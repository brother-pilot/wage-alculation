using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using wageсalculation.Persistance.Controllers;

[assembly: InternalsVisibleTo("TestClass")]
namespace wageсalculation.Persistance
{
    public class ControllerReaderFromFile: IControllerData
    {
        public string PathModel { get; }
        public ControllerReaderFromFile()
        {
            //инициируем путь для хранения файлов
            //узнаем путь к текущей папке проекта
            string pathExe = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Console.WriteLine(pathExe);
            PathModel = Path.Combine(pathExe, "Data");
        }

        

        //для тестирования
        internal ControllerReaderFromFile(IControllerData controllerReader, string pathModel)
        {
            PathModel = pathModel;
        }

        public List<T> ReadData<T>() where T : class
        {
            var result = new List<T>();
            var directory = new DirectoryInfo(PathModel);
            // Проверка на существование указанной директории.
            if (directory.Exists)
            {
                Console.WriteLine("Ищу файлы... в");
                Console.WriteLine("Директория с именем: {0}", directory.FullName);
                // считываем файлы
                try
                {
                    var fileName = Path.Combine(PathModel, typeof(T).Name+"s.csv");
                    var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                    StreamReader streamR = new StreamReader(fs);
                    if (typeof(T) == typeof(CurrentUser))
                    {
                        result = ReadFileUser(streamR) as List<T>; ;
                    }
                    else if (typeof(T) == typeof(InfoWork))
                    {
                        string line;
                        List<InfoWork> works = new List<InfoWork>();
                        while ((line = streamR.ReadLine()) != null)
                        {
                            works.Add(new InfoWork(line));
                        }
                        result = works as List<T>;
                    }
                    streamR.Close();
                    return result;
                }
                catch (Exception)
                {
                    Console.WriteLine($"Не верная структура файла {typeof(T).Name}s.scv");
                    throw new NotImplementedException($"Не верная структура файла {typeof(T).Name}s.scv"); ;
                }  
            }
            else
            {
                Console.WriteLine("Директория с именем: {0}  не существует.", directory.FullName);
                throw new Exception("Директория отсутсвует!");
            }
            return result;

        }


        List<CurrentUser> ReadFileUser(StreamReader streamR)
        {
            List<CurrentUser> users = new List<CurrentUser>();          
            string line;
            while ((line = streamR.ReadLine()) != null)
            {
                Level item2;
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
                users.Add(new CurrentUser(line.Split(',')[0], item2));
            }
            return users;
        }

        public bool WriteData<T>(List<T> item) where T : class
        {
            //var fileName = typeof(T).Name;
            var fileName = Path.Combine(PathModel, typeof(T).Name + "s.csv");
            try
            {
                // FileMode.OpenOrCreate - ЕСЛИ: существует ТО: открыть ИНАЧЕ: создать новый
                // FileAccess.Read - только для чтения,
                // FileShare.None - Совместный доступ - Нет.
                //FileStream stream = files[0].Open(FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                
                var fs = new FileStream(fileName, FileMode.OpenOrCreate);
                StreamWriter streamW = new StreamWriter(fs);
                if (typeof(T) == typeof(CurrentUser))
                {
                    List<CurrentUser> CurrentUsers = item as List<CurrentUser>;
                    CurrentUsers.ForEach(u => streamW.WriteLine(u.Name + "," + Model.ConvertFromLevelToString(u.Level)));
                }
                else if (typeof(T) == typeof(InfoWork))
                {
                    List<InfoWork> works = item as List<InfoWork>;
                    works.ForEach(x => streamW.WriteLine(x.Data + "," + x.Name + "," + x.Time + "," + x.Work));
                }
                streamW.Close();
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

    }
}
