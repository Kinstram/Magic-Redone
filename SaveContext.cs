using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Magic_Redone.EffectToSave;


namespace Magic_Redone
{
    public class SaveContext : DbContext
    {
        public DbSet<SaveEntity> Saves { get; set; }
        public DbSet<ConstructToSave> ConstructsToSave { get; set; }
        public DbSet<ScalationToSave> ScalationsToSave { get; set; }
        public DbSet<EffectToSave> EffectToSave { get; set; }

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

            // Настройка связей
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

            var converter = new ValueConverter<List<string>, string>(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                        v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) ?? new List<string>()
                    );

            modelBuilder.Entity<EffectToSave>(entity =>
            {
                // Применяем конвертер к свойству EffectDescs
                entity.Property(e => e.EffectDescs)
                      .HasConversion(converter);

                // Настройка связи с SaveEntity
                entity.HasOne(fts => fts.SaveEntity)
                      .WithMany(se => se.SavedEffects)
                      .HasForeignKey(fts => fts.SaveEntityId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DiceCombination>()
                .HasOne(d => d.EffectToSave)
                .WithMany(e => e.DiceCombinations)
                .HasForeignKey(d => d.EffectToSaveId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
