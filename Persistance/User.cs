using System;

namespace wageсalculation.Persistance
{
   public class User
    {
        string name;
        Level level;
        Role role;

        public string Name { get { return name; } }
        public Level Level { get { return level; } }
        public Role Role { get { return role; } }

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
                default:
                    throw new Exception("Неизвестная роль!");
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