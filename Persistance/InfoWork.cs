using System;

namespace wageсalculation
{
    public class InfoWork
    {
        public DateTime Data { get;  }
        public string Name { get;  }
        public int Time { get;  }
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