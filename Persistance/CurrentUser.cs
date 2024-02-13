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
        //URole uRole;

        [Required]
        public string Name { get { return name; } }
        [Required]
        public Level Level { get { return level; } }
        [Required]
        public Role Role { get; set; }//{ get { return uRole; } set {; } }

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
                    Role = new Header();
                    break;
                case Level.Worker:
                    Role = new Worker();
                    break;
                case Level.Freelancer:
                    Role = new Freelancer();
                    break;
                default:
                    throw new Exception("Неизвестная роль!");
            }
        }

        public int URoleId { get; set; } // внешний (Forien) ключ
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