using System;

namespace wageсalculation
{
   public class User
    {
        public string name;
        public Level level;
        public Role role;

        public User(string name, Level level)
        {
            this.name = name;
            this.level = level;
            switch (level)
            {
                case Level.Head:
                    role = new Header();
                    break;
                case Level.Worker:
                    role = new Worker();
                    break;
                case Level.Freelancer:
                    role = new Freelancer();
                    break;
            }
        }
    }

    //internal class User<T>
    //    where T : Freelance, Header, Worker//так писать нельзя т.к. одноправные типы
    //{
    //    string name;
    //    T role;

    //    public User(string name, T level)
    //    {
    //        this.name = name;
    //        this.role = level;
    //    }
    //}
}