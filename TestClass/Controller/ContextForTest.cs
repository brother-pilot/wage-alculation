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
        public DbSet<User> Users { get; set; }
        public DbSet<InfoWork> InfoWorks { get; set; }

        public ContextForTest()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestDB_001;Trusted_Connection=True;");

        }
    }
}
