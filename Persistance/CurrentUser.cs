using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wageсalculation.Persistance
{
    public class CurrentUser
    {
        //для EF
        public int Id { get; set; }

        //все трое должны бить readonly но EF не позволяет так сделать
        string name;
        Level level;
        [NotMapped]
        Role role;

        [Required]
        public string Name { get { return name; } }
        [Required]
        public Level Level { get { return level; } }
        [NotMapped]
        public Role Role { get { return role; } }

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