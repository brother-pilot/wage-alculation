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

        public DbSet<UserRole> UserRoles { get; set; }

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
            //привязка свойства Id и поля id!!!!!
            modelBuilder.Entity<CurrentUser>().Property("Name").HasField("name");
            modelBuilder.Entity<CurrentUser>().Property("Level").HasField("level");
            //modelBuilder.Entity<CurrentUser>().Property("UserRole").HasField("userRole");
            modelBuilder.Entity<InfoWork>().Property("Data").HasField("data");
            modelBuilder.Entity<InfoWork>().Property("Name").HasField("name");
            modelBuilder.Entity<InfoWork>().Property("Time").HasField("time");
            modelBuilder.Entity<InfoWork>().Property("Work").HasField("work");
        }
    }
}
