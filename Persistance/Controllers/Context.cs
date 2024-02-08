using Microsoft.EntityFrameworkCore;
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

        public DbSet<Role> Roles { get; set; }

        public DbSet<Wage> Wages { get; set; }
        public DbSet<InfoWork> InfoWorks { get; set; }

        public Context()
        {
            Database.EnsureCreated();
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
            modelBuilder.Entity<CurrentUser>().Property("Role").HasField("role");
            modelBuilder.Entity<InfoWork>().Property("Time").HasField("time");
        }

    }
}
