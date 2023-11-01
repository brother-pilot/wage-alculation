using System;

namespace wageсalculation.Persistance
{
    public class InfoWork
    {
        public DateTime Data { get;  }
        public string Name { get;  }

        private int time;

        public int Time
        {
            get { return time; }
            private set 
            {
                if (value < 0 || value > 24)
                    throw new Exception("Время работы должно быть от 0 до 24");
                time = value; 
            }
        }

        public string Work { get;  }

        public InfoWork(DateTime Data, string Name, int Time, string Work)
        {
            this.Data = Data;
            this.Name = Name;
            this.Time = Time;
            this.Work = Work;
        }

        public InfoWork(string line)
        {
            var items = line.Split(',');
            if (items.Length != 4||items[0].Length<10)
                throw new Exception("Неверный формат данных строки!");
            this.Data = new DateTime(
                                     int.Parse(items[0].Substring(0, 3)),
                                     int.Parse(items[0].Substring(3, 2)),
                                     int.Parse(items[0].Substring(5, 2))
                                     );
            this.Name = items[1];
            this.Time = int.Parse(items[2]);
            this.Work = items[3];
        }
    }
}