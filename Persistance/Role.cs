using System;
using System.Collections.Generic;

namespace wageсalculation.Persistance
{
    public class Role
    {  
        //класс для соединения сущностей Header, Worker, Freelancer вместе+для уменьшения
        //дублирования кода в этих сущностях
        protected Wage wage;
        //public string[] methods;
        //public Action<InfoWork> del;
        public Dictionary<int, Command> commands { get; set; }     
    }
}