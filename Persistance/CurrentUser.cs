using System;

namespace wageсalculation.Persistance
{
    public class CurrentUser
    {
        //для EF
        public int Id { get; set; }

        readonly string name;
        readonly Level level;
        readonly UserRole role;
        //для EF

        public string Name { get { return name; } }
        public Level Level { get { return level; } }
        public UserRole Role { get { return role; } }

        //для EF
        public CurrentUser()
        {

        }

        public CurrentUser(string name, Level level)
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

    //public static implicit operator User(T u) => new User{ };

}