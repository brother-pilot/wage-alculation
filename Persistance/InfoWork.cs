using System;
using System.ComponentModel.DataAnnotations;

namespace wageсalculation.Persistance
{
    public class InfoWork
    {
        //для EF
        public int Id { get; set; }

        DateTime data;
        [Required]
        public DateTime Data { get { return data; } }

        //чтобы EF потом добавил это свойство в таблицу не забывать в onmodelcreating
        //Property("Name").HasField("name")
        string name;

        [Required]
        public string Name { get { return name; } }

        private int time;

        public int Time
        {
            get { return time; }
            private set 
            {
                if (value < 0 || value > 24)
                    throw new ArgumentOutOfRangeException("Время работы должно быть от 0 до 24");
                time = value; 
            }
        }

        string work;

        //[Required]
        public string Work { get { return work; } }

        //для EF
        public InfoWork()
        {
        }
        public InfoWork(DateTime Data, string Name, int Time, string Work)
        {
            this.data = Data;
            this.name = Name;
            this.Time = Time;
            this.work = Work;
        }

        public InfoWork(string line)
        {
            var items = line.Split(',');
            if (items.Length != 4)
                throw new Exception("Неверный формат данных строки!");
            DateTime dt;
            //this.Data = new DateTime(
            //                         int.Parse(items[0].Substring(0, 3)),
            //                         int.Parse(items[0].Substring(3, 2)),
            //                         int.Parse(items[0].Substring(5, 2))
            //                         );
            //this.Data = new DateTime(20231001);
            if (!DateTime.TryParse(items[0], out dt))
                throw new Exception("Неверный формат данных строки! неправильная дата"); ;
            this.data = dt;
            this.name = items[1];
            int t;
            if (!int.TryParse(items[2], out t)) 
                throw new ArgumentOutOfRangeException("Время работы должно быть числом от 0 до 24");
            this.Time = t;
            this.work = items[3];
        }
    }
}