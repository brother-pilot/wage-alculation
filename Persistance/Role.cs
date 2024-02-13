using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wageсalculation.Persistance
{
    public class Role
    {
        //для EF
        public int Id { get; set; }
        //класс для соединения сущностей Header, Worker, Freelancer вместе+для уменьшения
        //дублирования кода в этих сущностях
        protected Wage wage;
        [Required]
        public Wage Wage { get { return wage; } } //set {; } для EF
        //public string[] methods;
        //public Action<InfoWork> del;
        [NotMapped]
        public Dictionary<int, Command> Commands { get;} = new Dictionary<int, Command>();

        [NotMapped]
        //возможные действия пользователя
        public readonly Dictionary<Command, string> mesRole = new Dictionary<Command, string>()
        {
            {Command.AddHour,"Добавить часы работы" },
            {Command.MakeReport,"Просмотреть отчет по отработанным часам"},
            {Command.AddUser,"Добавить сотрудника"},
            {Command.MakeReportInOtherUser,"Просмотреть отчет по конкретному сотруднику"},
            {Command.MakeReportAllUsers,"Просмотреть отчет по всем сотрудникам"},
            {Command.Exit,"Выход из программы"}
        };

        //для EF
        public Role()
        {

        }

        public virtual CurrentUser CurrentUser { get; set; }  // навигационное свойство

        [ForeignKey("Id")]
        public int WageId { get; set; } // внешний ключ
    }
}