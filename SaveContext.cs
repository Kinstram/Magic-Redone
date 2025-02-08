using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Magic_Redone
{
    public class SaveContext : DbContext
    {
        public DbSet<SaveEntity> Saves { get; set; }
        public DbSet<Construct> Constructs { get; set; }
        public DbSet<ConstructToSave> ConstructsToSave { get; set; }
        public DbSet<ScalationToSave> ScalationsToSave { get; set; }

        public SaveContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=UserSaves.db")
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SaveEntity>()
                .HasIndex(u => u.SaveName)
                .IsUnique();

            // Настройка связи между ConstructToSave и SaveEntity (один-ко-многим)
            modelBuilder.Entity<ConstructToSave>()
                .HasOne(cts => cts.SaveEntity)
                .WithMany() // Использование .WithMany() для настройки обратной связи
                .HasForeignKey(cts => cts.SaveEntityId)
                 .OnDelete(DeleteBehavior.Cascade);

            // Настройка связи между ConstructToSave и Construct (один-ко-одному)
            modelBuilder.Entity<ConstructToSave>()
                .HasOne(cts => cts.Construct)
                .WithMany()
                .HasForeignKey(cts => cts.ConstructId)
                 .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ScalationToSave>()
                .HasOne(sts => sts.SaveEntity)
                .WithMany(se => se.SavedScalations)
                .HasForeignKey(sts => sts.SaveEntityId)
                 .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
