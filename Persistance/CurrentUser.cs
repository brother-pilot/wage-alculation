using System;
using System.ComponentModel.DataAnnotations;

namespace wageсalculation.Persistance
{
    public class CurrentUser
    {
        //для EF
        public int Id { get; set; }

        //все трое должны бить readonly но EF не позволяет так сделать
        string name;
        Level level;
        UserRole userRole;

        [Required]
        public string Name { get { return name; } }
        public Level Level { get { return level; } }
        public UserRole UserRole { get { return userRole; } }

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
                    userRole = new Header();
                    break;
                case Level.Worker:
                    userRole = new Worker();
                    break;
                case Level.Freelancer:
                    userRole = new Freelancer();
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