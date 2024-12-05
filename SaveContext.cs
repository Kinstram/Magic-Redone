using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Magic_Redone
{
    public class SaveContext : DbContext
    {
        public DbSet<SaveEntity> Saves { get; set; }
        public SaveContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=UserSaves.db");
        }
    }
}
