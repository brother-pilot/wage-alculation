﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wageсalculation.Persistance.Controllers
{
    class Context : DbContext
    {
        public DbSet<CurrentUser> CurrentUsers { get; set; }
        public DbSet<InfoWork> InfoWorks { get; set; }
        
        public Context()
        {
            //когда используем миграции то это должно быть отключено
            if (Database.EnsureCreated()) 
                Console.WriteLine("База данных не существовала, поэтому она была создана");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WageCalculationDB;Trusted_Connection=True;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //привязка свойства Id и поля id!!!!!
            modelBuilder.Entity<CurrentUser>().Property("Name").HasField("name");
            modelBuilder.Entity<CurrentUser>().Property("Level").HasField("level");
            modelBuilder.Entity<InfoWork>().Property("Data").HasField("data");
            modelBuilder.Entity<InfoWork>().Property("Name").HasField("name");
            modelBuilder.Entity<InfoWork>().Property("Time").HasField("time");
            modelBuilder.Entity<InfoWork>().Property("Work").HasField("work");
        }

    }
}
