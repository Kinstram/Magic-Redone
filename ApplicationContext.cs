using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magic_Redone
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Construct> Constructs => Set<Construct>();
        public DbSet<Effect> EffectList => Set<Effect>();
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=magic.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Construct>()
                        .HasOne(c => c.TiedEffect)
                        .WithOne(e => e.Construct)
                        .HasForeignKey<Effect>(e => e.ConstructId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Construct>().HasData(
                    new Construct { Id = 1, Kind = 0, Name = "Аркан", ValueExt = 1m, ValueInt = 1m, ValueMP = 1m, Description = " " },
                    new Construct { Id = 2, Kind = 0, Name = "Огонь", ValueExt = 1.2m, ValueInt = 0.5m, ValueMP = 0.8m, Description = " " },
                    new Construct { Id = 3, Kind = 0, Name = "Вода", ValueExt = 0.9m, ValueInt = 1.2m, ValueMP = 1m, Description = " " },
                    new Construct { Id = 4, Kind = 0, Name = "Воздух", ValueExt = 0.8m, ValueInt = 0.8m, ValueMP = 0.5m, Description = " " },
                    new Construct { Id = 5, Kind = 0, Name = "Земля", ValueExt = 1m, ValueInt = 1.5m, ValueMP = 1.2m, Description = " " },

                    new Construct { Id = 6, Kind = 1, Name = "Касание", ValueExt = 1.2m, ValueInt = 1m, ValueMP = 0.8m, Description = " " },
                    new Construct { Id = 7, Kind = 1, Name = "Точка", ValueExt = 1m, ValueInt = 0.8m, ValueMP = 1.2m, Description = " " },
                    new Construct { Id = 8, Kind = 1, Name = "Снаряд", ValueExt = 1.4m, ValueInt = 1.2m, ValueMP = 1.4m, Description = " " },
                    new Construct { Id = 9, Kind = 1, Name = "Излучение", ValueExt = 1.2m, ValueInt = 0.5m, ValueMP = 1.2m, Description = " " },
                    new Construct { Id = 10, Kind = 1, Name = "Выпуск", ValueExt = 1.1m, ValueInt = 0.8m, ValueMP = 1.4m, Description = " " },
                    new Construct { Id = 11, Kind = 1, Name = "Насыщение", ValueExt = 1m, ValueInt = 2m, ValueMP = 0.2m, Description = " " },
                    new Construct { Id = 12, Kind = 1, Name = "Воплощение", ValueExt = 1m, ValueInt = 1m, ValueMP = 4m, Description = " " },
                    new Construct { Id = 13, Kind = 1, Name = "Луч", ValueExt = 1.2m, ValueInt = 1.2m, ValueMP = 1m, Description = " " },
                    new Construct { Id = 14, Kind = 1, Name = "Дождь", ValueExt = 0.8m, ValueInt = 1m, ValueMP = 2m, Description = " " },
                    new Construct { Id = 15, Kind = 1, Name = "Самопоток", ValueExt = 1m, ValueInt = 1.2m, ValueMP = 0.8m, Description = " " },
                    new Construct { Id = 16, Kind = 1, Name = "Покров", ValueExt = 0.8m, ValueInt = 1m, ValueMP = 1.2m, Description = " " },

                    new Construct { Id = 17, Kind = 2, Name = "Круг", ValueExt = 1m, ValueInt = 1.2m, ValueMP = 1.2m, Description = " " },
                    new Construct { Id = 18, Kind = 2, Name = "Овал", ValueExt = 1m, ValueInt = 1m, ValueMP = 1.3m, Description = " " },
                    new Construct { Id = 19, Kind = 2, Name = "Треугольник", ValueExt = 1m, ValueInt = 1m, ValueMP = 1.1m, Description = " " },
                    new Construct { Id = 20, Kind = 2, Name = "Полусфера", ValueExt = 1m, ValueInt = 1.25m, ValueMP = 1.1m, Description = " " },
                    new Construct { Id = 21, Kind = 2, Name = "Сфера", ValueExt = 1m, ValueInt = 1.5m, ValueMP = 1m, Description = " " },
                    new Construct { Id = 22, Kind = 2, Name = "Конус", ValueExt = 1m, ValueInt = 0.8m, ValueMP = 1.3m, Description = " " },
                    new Construct { Id = 23, Kind = 2, Name = "Куб", ValueExt = 1m, ValueInt = 1m, ValueMP = 1.4m, Description = " " },
                    new Construct { Id = 24, Kind = 2, Name = "Труба", ValueExt = 1m, ValueInt = 1m, ValueMP = 1.2m, Description = " " },
                    new Construct { Id = 25, Kind = 2, Name = "Вихрь", ValueExt = 1m, ValueInt = 0.8m, ValueMP = 2m, Description = " " },

                    new Construct { Id = 26, Kind = 3, Name = "Жало", ValueExt = 0m, ValueInt = -0.1m, ValueMP = 0.5m, Description = " " },
                    new Construct { Id = 27, Kind = 3, Name = "Сокрытие", ValueExt = 0m, ValueInt = 0m, ValueMP = 4m, Description = " " },
                    new Construct { Id = 28, Kind = 3, Name = "Связь", ValueExt = 0m, ValueInt = 0.2m, ValueMP = 1m, Description = " " },
                    new Construct { Id = 29, Kind = 3, Name = "Кость", ValueExt = 0.2m, ValueInt = -0.2m, ValueMP = 0.4m, Description = " " },
                    new Construct { Id = 30, Kind = 3, Name = "Ребро", ValueExt = 0m, ValueInt = 0.2m, ValueMP = 0.2m, Description = " " },
                    new Construct { Id = 31, Kind = 3, Name = "Наконечник", ValueExt = 0m, ValueInt = 0m, ValueMP = 3m, Description = " " },
                    new Construct { Id = 32, Kind = 3, Name = "Взрыв", ValueExt = 0m, ValueInt = -0.5m, ValueMP = 2m, Description = " " },
                    new Construct { Id = 33, Kind = 3, Name = "Мир", ValueExt = 0m, ValueInt = 0.1m, ValueMP = 0.1m, Description = " " },
                    new Construct { Id = 34, Kind = 3, Name = "Длань", ValueExt = 0m, ValueInt = -0.1m, ValueMP = 0.5m, Description = " " },
                    new Construct { Id = 35, Kind = 3, Name = "Лех'сар", ValueExt = 0m, ValueInt = 0m, ValueMP = 1m, Description = " " },
                    new Construct { Id = 36, Kind = 3, Name = "Метка", ValueExt = 0m, ValueInt = 0m, ValueMP = 0.1m, Description = " " },
                    new Construct { Id = 37, Kind = 3, Name = "Щит", ValueExt = -0.2m, ValueInt = 0.2m, ValueMP = 1m, Description = " " },
                    new Construct { Id = 38, Kind = 3, Name = "Пузырь", ValueExt = 0m, ValueInt = 0.4m, ValueMP = 0.4m, Description = " " },
                    new Construct { Id = 39, Kind = 3, Name = "Панцирь", ValueExt = -0.4m, ValueInt = 0.2m, ValueMP = 2m, Description = " " },
                    new Construct { Id = 40, Kind = 3, Name = "Цепь", ValueExt = 0m, ValueInt = 0m, ValueMP = 0.5m, Description = " " },
                    new Construct { Id = 41, Kind = 3, Name = "Гончая", ValueExt = 0m, ValueInt = 0m, ValueMP = 0.2m, Description = " " },
                    new Construct { Id = 42, Kind = 3, Name = "Вихрь", ValueExt = 0m, ValueInt = -0.5m, ValueMP = 2m, Description = " " },
                    new Construct { Id = 43, Kind = 3, Name = "Фантом", ValueExt = -0.5m, ValueInt = -0.5m, ValueMP = 2m, Description = " " },
                    new Construct { Id = 44, Kind = 3, Name = "Концентрация", ValueExt = 0m, ValueInt = 0.5m, ValueMP = 0.5m, Description = " " },
                    new Construct { Id = 45, Kind = 3, Name = "Клеть", ValueExt = 0m, ValueInt = 0.2m, ValueMP = 1.5m, Description = " " },
                    new Construct { Id = 46, Kind = 3, Name = "Путы", ValueExt = 0m, ValueInt = 0m, ValueMP = 1m, Description = " " },
                    new Construct { Id = 47, Kind = 3, Name = "Проницаемость", ValueExt = -0.7m, ValueInt = 0m, ValueMP = 4m, Description = " " },
                    new Construct { Id = 48, Kind = 3, Name = "Змей", ValueExt = -0.2m, ValueInt = 0m, ValueMP = 1m, Description = " " },
                    new Construct { Id = 49, Kind = 3, Name = "Дождь", ValueExt = -0.2m, ValueInt = 0m, ValueMP = 2m, Description = " " },
                    new Construct { Id = 50, Kind = 3, Name = "Анализ", ValueExt = 0m, ValueInt = 0m, ValueMP = 3m, Description = " " },
                    new Construct { Id = 51, Kind = 3, Name = "Познание", ValueExt = 0m, ValueInt = 0m, ValueMP = 10m, Description = " " },
                    new Construct { Id = 52, Kind = 3, Name = "Обертка", ValueExt = 0m, ValueInt = 0m, ValueMP = 2m, Description = " " },
                    new Construct { Id = 53, Kind = 3, Name = "Проклятье", ValueExt = -0.2m, ValueInt = 0m, ValueMP = 2m, Description = " " },
                    new Construct { Id = 54, Kind = 3, Name = "Усиление", ValueExt = 0m, ValueInt = 0m, ValueMP = 2m, Description = " " },
                    new Construct { Id = 55, Kind = 3, Name = "Преодоление", ValueExt = 0m, ValueInt = 0m, ValueMP = 6m, Description = " " },
                    new Construct { Id = 56, Kind = 3, Name = "Кин’a’дхаз", ValueExt = 0m, ValueInt = 0m, ValueMP = 2m, Description = " " },
                    new Construct { Id = 57, Kind = 3, Name = "Дестабилизация", ValueExt = 0.2m, ValueInt = -0.4m, ValueMP = 4m, Description = " " },
                    new Construct { Id = 58, Kind = 3, Name = "Линза", ValueExt = 0m, ValueInt = 0m, ValueMP = 1m, Description = " " },
                    new Construct { Id = 59, Kind = 3, Name = "Чувство", ValueExt = 0m, ValueInt = 0m, ValueMP = 2m, Description = " " },
                    new Construct { Id = 60, Kind = 3, Name = "Экран", ValueExt = 0m, ValueInt = 0.2m, ValueMP = 2m, Description = " " },
                    new Construct { Id = 61, Kind = 3, Name = "Мышца", ValueExt = -0.1m, ValueInt = -0.1m, ValueMP = 1m, Description = " " },
                    new Construct { Id = 62, Kind = 3, Name = "Спокойствие", ValueExt = 0m, ValueInt = 0m, ValueMP = 1m, Description = " " },
                    new Construct { Id = 63, Kind = 3, Name = "Поиск", ValueExt = 0m, ValueInt = 0m, ValueMP = 1m, Description = " " },
                    new Construct { Id = 64, Kind = 3, Name = "Область", ValueExt = -0.95m, ValueInt = 0m, ValueMP = 3m, Description = " " }
                );

            modelBuilder.Entity<Effect>().HasData(
                new { Id = 1, ConstructId = 26, Type = EffectType.Damage, Quantity = 1, DiceSides = 4, EffectDesc = " " },
                new { Id = 2, ConstructId = 34, Type = EffectType.Heal, Quantity = 1, DiceSides = 4, EffectDesc = " " },
                new { Id = 3, ConstructId = 37, Type = EffectType.Protection, Quantity = 3, DiceSides = 0, EffectDesc = " " },
                new { Id = 4, ConstructId = 27, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Прячет заклинание в одном из спектров восприятия (зрение /слух /обоняние/ осязание/ ощущение магии/другое)" }
                );
        }
    }
}
