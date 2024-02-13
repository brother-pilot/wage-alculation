using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wageсalculation.Persistance;

namespace TestClass.Controller
{
    class ContextForTest : DbContext
    {
        public DbSet<CurrentUser> CurrentUsers { get; set; }

        public DbSet<Header> Headers { get; set; }

        public DbSet<Worker> Workers { get; set; }

        public DbSet<Freelancer> Freelancers { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Wage> Wages { get; set; }
        public DbSet<InfoWork> InfoWorks { get; set; }

        public ContextForTest()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WageCalcTestDB;Trusted_Connection=True;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //привязка свойства Name и поля name!!!!! по другому не поймет
            modelBuilder.Entity<CurrentUser>().Property("Name").HasField("name");
            modelBuilder.Entity<CurrentUser>().Property("Level").HasField("level");
            //так нельзя
            //modelBuilder.Entity<CurrentUser>().Property("URole");
            //так нельзя
            //modelBuilder.Entity<CurrentUser>().Property("URole").HasField("uRole");
            //так нельзя
            //modelBuilder.Entity<URole>().Property("Wage").HasField("wage");
           
            //благодаря этому удалось добиться сделать поле public Wage Wage только с get
            modelBuilder
            .Entity<Role>() //для какой таблицы нужен FK, для подчиненной
            .HasOne(u => u.Wage) //главная
            .WithOne(p => p.Role)//подчиненная
            .HasForeignKey<Role>(p => p.WageId);//прописываем что FK идет от подчиненной к главной через свойство
            modelBuilder.Entity<InfoWork>().Property("Data").HasField("data");
            modelBuilder.Entity<InfoWork>().Property("Name").HasField("name");
            modelBuilder.Entity<InfoWork>().Property("Time").HasField("time");
            modelBuilder.Entity<InfoWork>().Property("Work").HasField("work");
            modelBuilder.Entity<Wage>().Property("MonthWage").HasField("monthWage");
            modelBuilder.Entity<Wage>().Property("Bonus").HasField("bonus");
            modelBuilder.Entity<Wage>().Property("IsBonus").HasField("isBonus");
            modelBuilder.Entity<Wage>().Property("IsMonthWage").HasField("isMonthWage");
            modelBuilder.Entity<Wage>().Property("HourWage").HasField("hourWage");
        }
    }
}
