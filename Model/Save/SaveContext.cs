using Microsoft.EntityFrameworkCore;
using System.IO;


namespace Magic_Redone
{
    public class SaveContext : DbContext
    {
        public DbSet<SaveEntity> Saves { get; set; }
        public DbSet<ConstructToSave> ConstructsToSave { get; set; }
        public DbSet<ScalationToSave> ScalationsToSave { get; set; }

        public SaveContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=UserSaves.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SaveEntity>()
                .HasIndex(u => u.SaveName)
                .IsUnique();

            modelBuilder.Entity<ConstructToSave>()
                .HasOne(cts => cts.SaveEntity)
                .WithMany(se => se.SavedComponents)
                .HasForeignKey(cts => cts.SaveEntityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ScalationToSave>()
                .HasOne(sts => sts.SaveEntity)
                .WithMany(se => se.SavedScalations)
                .HasForeignKey(sts => sts.SaveEntityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
