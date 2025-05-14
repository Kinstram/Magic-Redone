using Microsoft.EntityFrameworkCore;
using System.IO;

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
                    new Construct { Id = 1, Kind = 0, Name = "Аркан", ValueExt = 1m, ValueInt = 1m, ValueMP = 1m, Description = "Рекомендуется использовать Аркан в качестве стихии, иначе воздействие будет произведено именно выбранной стихией" },
                    new Construct { Id = 2, Kind = 0, Name = "Огонь", ValueExt = 1.2m, ValueInt = 0.5m, ValueMP = 0.8m, Description = "Стандартная стихия огня" },
                    new Construct { Id = 3, Kind = 0, Name = "Вода", ValueExt = 0.9m, ValueInt = 1.2m, ValueMP = 1m, Description = "Стандартная стихия воды" },
                    new Construct { Id = 4, Kind = 0, Name = "Воздух", ValueExt = 0.8m, ValueInt = 0.8m, ValueMP = 0.5m, Description = "Стандартная стихия воздуха" },
                    new Construct { Id = 5, Kind = 0, Name = "Земля", ValueExt = 1m, ValueInt = 1.5m, ValueMP = 1.2m, Description = "Стандартная стихия земли" },

                    new Construct { Id = 6, Kind = 1, Name = "Касание", ValueExt = 1.2m, ValueInt = 1m, ValueMP = 0.8m, Description = "Создание заклинания с привязкой к определённому объекту. Требует непосредственного контакта мага и объекта" },
                    new Construct { Id = 7, Kind = 1, Name = "Точка", ValueExt = 1m, ValueInt = 0.8m, ValueMP = 1.2m, Description = "Создание заклинания в точке пространства. Маг должен видеть точку создания, либо иметь точную точку" },
                    new Construct { Id = 8, Kind = 1, Name = "Снаряд", ValueExt = 1.4m, ValueInt = 1.2m, ValueMP = 1.4m, Description = "Создание автономного снаряда. По умолчанию двигается только в заданном направлении до соприкосновения с твёрдым телом" },
                    new Construct { Id = 9, Kind = 1, Name = "Излучение", ValueExt = 1.2m, ValueInt = 0.5m, ValueMP = 1.2m, Description = "Источник излучения - маг. Распределяется сферически. Невозможно придать какую-либо форму." },
                    new Construct { Id = 10, Kind = 1, Name = "Выпуск", ValueExt = 1.1m, ValueInt = 0.8m, ValueMP = 1.4m, Description = "Контролируемый вариант излучения." },
                    new Construct { Id = 11, Kind = 1, Name = "Насыщение", ValueExt = 1m, ValueInt = 2m, ValueMP = 0.2m, Description = " " },
                    new Construct { Id = 12, Kind = 1, Name = "Воплощение", ValueExt = 1m, ValueInt = 1m, ValueMP = 4m, Description = "Создание постоянных или полупостоянных объектов" },
                    new Construct { Id = 13, Kind = 1, Name = "Луч", ValueExt = 1.2m, ValueInt = 1.2m, ValueMP = 1m, Description = "Заклинание в виде луча движется до ближайшего объекта. Невозможно придать какую-либо форму" },
                    new Construct { Id = 14, Kind = 1, Name = "Дождь", ValueExt = 0.8m, ValueInt = 1m, ValueMP = 2m, Description = "Радиус = Ћ*2 метров (считается именно Ћ этого компонента). Аналогично дождю падает с неба" },
                    new Construct { Id = 15, Kind = 1, Name = "Самопоток", ValueExt = 1m, ValueInt = 1.2m, ValueMP = 0.8m, Description = "Эффект заклинания инициируется и распространяется на самого мага" },
                    new Construct { Id = 16, Kind = 1, Name = "Покров", ValueExt = 0.8m, ValueInt = 1m, ValueMP = 1.2m, Description = "Заклинание создаëтся плëнкой по фигуре мага" },

                    new Construct { Id = 17, Kind = 2, Name = "Круг", ValueExt = 1m, ValueInt = 1.2m, ValueMP = 1.2m, Description = "Круг, либо плоскость в виде круга" },
                    new Construct { Id = 18, Kind = 2, Name = "Овал", ValueExt = 1m, ValueInt = 1m, ValueMP = 1.3m, Description = "Овал, либо плоскость в виде овала " },
                    new Construct { Id = 19, Kind = 2, Name = "Треугольник", ValueExt = 1m, ValueInt = 1m, ValueMP = 1.1m, Description = "Треугольник, либо плоскость в виде треугольника" },
                    new Construct { Id = 20, Kind = 2, Name = "Полусфера", ValueExt = 1m, ValueInt = 1.25m, ValueMP = 1.1m, Description = "Полусфера" },
                    new Construct { Id = 21, Kind = 2, Name = "Сфера", ValueExt = 1m, ValueInt = 1.5m, ValueMP = 1m, Description = "Сфера" },
                    new Construct { Id = 22, Kind = 2, Name = "Конус", ValueExt = 1m, ValueInt = 0.8m, ValueMP = 1.3m, Description = "Конус" },
                    new Construct { Id = 23, Kind = 2, Name = "Куб", ValueExt = 1m, ValueInt = 1m, ValueMP = 1.4m, Description = "Куб" },
                    new Construct { Id = 24, Kind = 2, Name = "Труба", ValueExt = 1m, ValueInt = 1m, ValueMP = 1.2m, Description = "Прямоугольник, грани которого образуют круглую трубу" },
                    new Construct { Id = 25, Kind = 2, Name = "Дождь", ValueExt = 0.8m, ValueInt = 1m, ValueMP = 2m, Description = "Радиус = Ћ*2 метров (считается именно Ћ этого компонента). Аналогично дождю падает с неба." },
                    new Construct { Id = 26, Kind = 2, Name = "Вихрь", ValueExt = 1m, ValueInt = 0.8m, ValueMP = 2m, Description = "Распространяет эффект заклинания в виде вихря, что расширяется из изначальной позиции со скоростью 2 гекс/секунда. Максимальная область - 6 гексов. Скалируется"  },

                    new Construct { Id = 27, Kind = 3, Name = "Жало", ValueExt = 0m, ValueInt = -0.1m, ValueMP = 0.5m, Description = "Компонент скалируется в два раза и далее. Например: 1d6 - ‡ = -0.2, Ћ = 1; 1d8 - ‡ = -0.4, Ћ = 2" },
                    new Construct { Id = 28, Kind = 3, Name = "Сокрытие", ValueExt = 0m, ValueInt = 0m, ValueMP = 4m, Description = "Прячет заклинание в одном из спектров восприятия (зрение /слух /обоняние/ осязание/ ощущение магии/другое)" },
                    new Construct { Id = 29, Kind = 3, Name = "Связь", ValueExt = 0m, ValueInt = 0.2m, ValueMP = 1m, Description = "Позволяет связывать разные заклинания, что снижает время их создания на 1 секунду. Позволяет комбинировать компоненты, увеличивая скаляцию эффекта" },
                    new Construct { Id = 30, Kind = 3, Name = "Кость", ValueExt = 0.2m, ValueInt = -0.2m, ValueMP = 0.4m, Description = "Скалируется. Просто повышает коэффициент" },
                    new Construct { Id = 31, Kind = 3, Name = "Ребро", ValueExt = 0m, ValueInt = 0.2m, ValueMP = 0.2m, Description = "Просто повышает коэффициент. НЕ скалируется" },
                    new Construct { Id = 32, Kind = 3, Name = "Наконечник", ValueExt = 0m, ValueInt = 0m, ValueMP = 3m, Description = "Даëт уровень пробития DR. Скалируется" },
                    new Construct { Id = 33, Kind = 3, Name = "Взрыв", ValueExt = 0m, ValueInt = -0.5m, ValueMP = 2m, Description = "Даёт урон в половину кубиков осколками и заменяет тип урона на ex. Может быть только один. Пример: 2d4 burn -> 2d4 ex [1d4]" },
                    new Construct { Id = 34, Kind = 3, Name = "Мир", ValueExt = 0m, ValueInt = 0.1m, ValueMP = 0.1m, Description = "Заклинание не наносит вреда." },
                    new Construct { Id = 35, Kind = 3, Name = "Длань", ValueExt = 0m, ValueInt = -0.1m, ValueMP = 0.5m, Description = "Лечебный компонент. Скалируется. Требует успешную проверку навыка 'Врачебное дело'" },
                    new Construct { Id = 36, Kind = 3, Name = "Лех'сар", ValueExt = 0m, ValueInt = 0m, ValueMP = 1m, Description = "Наводит заклинание на цель. Скалируется. Базовая стоимость - отклонение от цели на 1 гекс/покрытие 1 успеха уклонения" },
                    new Construct { Id = 37, Kind = 3, Name = "Метка", ValueExt = 0m, ValueInt = 0m, ValueMP = 0.1m, Description = "Ставит метку на определëнную цель. При соседстве с компонентом 'Мир' снижает стоимость до 10%" },
                    new Construct { Id = 38, Kind = 3, Name = "Щит", ValueExt = -0.2m, ValueInt = 0.2m, ValueMP = 1m, Description = "Скалируется." },
                    new Construct { Id = 39, Kind = 3, Name = "Пузырь", ValueExt = 0m, ValueInt = 0.4m, ValueMP = 0.4m, Description = "Создаёт препятствие для определённого способа восприятия (Запах/звук/свет/иное)" },
                    new Construct { Id = 40, Kind = 3, Name = "Панцирь", ValueExt = -0.4m, ValueInt = 0.2m, ValueMP = 2m, Description = "Скалируется. Развеется после прохождения урона через DR, если нет HP" },
                    new Construct { Id = 41, Kind = 3, Name = "Цепь", ValueExt = 0m, ValueInt = 0m, ValueMP = 0.5m, Description = "Поражает одну дополнительную цель, помеченную меткой" },
                    new Construct { Id = 42, Kind = 3, Name = "Гончая", ValueExt = 0m, ValueInt = 0m, ValueMP = 0.2m, Description = "Следует за целью, помеченной меткой или заклинателем. Скорость - 4 м/сек" },
                    new Construct { Id = 43, Kind = 3, Name = "Вихрь", ValueExt = 0m, ValueInt = -0.5m, ValueMP = 2m, Description = "Распространяет эффект заклинания в виде вихря, расширяющегося со скоростью 2 гекс/секунда. Максимальная область - 6 гексов" },
                    new Construct { Id = 44, Kind = 3, Name = "Фантом", ValueExt = -0.5m, ValueInt = -0.5m, ValueMP = 2m, Description = "Создаëт фантомное изображение. Детализация зависит от умения мага" },
                    new Construct { Id = 45, Kind = 3, Name = "Концентрация", ValueExt = 0m, ValueInt = 0.5m, ValueMP = 0.5m, Description = "Скалируется." },
                    new Construct { Id = 46, Kind = 3, Name = "Клеть", ValueExt = 0m, ValueInt = 0.2m, ValueMP = 1.5m, Description = "Создаëт клеть вокруг объекта. SM>0 требует вдвое больше затрат" },
                    new Construct { Id = 47, Kind = 3, Name = "Путы", ValueExt = 0m, ValueInt = 0m, ValueMP = 1m, Description = "Создаëт путы на всех конечностях объекта. Невозможно развязать" },
                    new Construct { Id = 48, Kind = 3, Name = "Проницаемость", ValueExt = -0.7m, ValueInt = 0m, ValueMP = 4m, Description = "Заклинание не воздействует на объект (камень/человек/меченый объект)" },
                    new Construct { Id = 49, Kind = 3, Name = "Змей", ValueExt = -0.2m, ValueInt = 0m, ValueMP = 1m, Description = "Воздействует на цель с периодичностью в одну секунду, нанося половинный вред. Скалируется по времени" },
                    new Construct { Id = 50, Kind = 3, Name = "Дождь", ValueExt = -0.2m, ValueInt = 0m, ValueMP = 2m, Description = "Радиус = Ћ*2 метров. Аналогично дождю падает с неба" },
                    new Construct { Id = 51, Kind = 3, Name = "Анализ", ValueExt = 0m, ValueInt = 0m, ValueMP = 3m, Description = "Выдаëт доступную и не скрытую информацию об объекте" },
                    new Construct { Id = 52, Kind = 3, Name = "Познание", ValueExt = 0m, ValueInt = 0m, ValueMP = 10m, Description = "Выдаëт информацию об объекте с +5 к результату соревнования" },
                    new Construct { Id = 53, Kind = 3, Name = "Обертка", ValueExt = 0m, ValueInt = 0m, ValueMP = 2m, Description = "Заворачивает следующее заклинание, расположенное правее по линии" },
                    new Construct { Id = 54, Kind = 3, Name = "Проклятье", ValueExt = -0.2m, ValueInt = 0m, ValueMP = 2m, Description = "Переносит эффект заклинания на того, кто прикоснулся к объекту. Скалируется" },
                    new Construct { Id = 55, Kind = 3, Name = "Усиление", ValueExt = 0m, ValueInt = 0m, ValueMP = 2m, Description = "Усиливает определённый физический параметр объекта. До +4" },
                    new Construct { Id = 56, Kind = 3, Name = "Преодоление", ValueExt = 0m, ValueInt = 0m, ValueMP = 6m, Description = "Усиливает атрибут объекта (ST, IQ, DX, HT). До +4" },
                    new Construct { Id = 57, Kind = 3, Name = "Кин’a’дхаз", ValueExt = 0m, ValueInt = 0m, ValueMP = 2m, Description = "Инвертирует действие заклинания. Работает не со всеми параметрами" },
                    new Construct { Id = 58, Kind = 3, Name = "Дестабилизация", ValueExt = 0.2m, ValueInt = -0.4m, ValueMP = 4m, Description = "Соревнование навыков заклинания. Защита и атака получают бонусы за значения ‡ и †" },
                    new Construct { Id = 59, Kind = 3, Name = "Линза", ValueExt = 0m, ValueInt = 0m, ValueMP = 1m, Description = "Перенаправляет заклинание в определëнной точке. Требует установки заранее" },
                    new Construct { Id = 60, Kind = 3, Name = "Чувство", ValueExt = 0m, ValueInt = 0m, ValueMP = 2m, Description = "Позволяет удалëнно почувствовать/увидеть/услышать. Цена за 10 метров" },
                    new Construct { Id = 61, Kind = 3, Name = "Экран", ValueExt = 0m, ValueInt = 0.2m, ValueMP = 2m, Description = "Экранирует область заклинания, оставляя открытой лишь определëнную зону" },
                    new Construct { Id = 62, Kind = 3, Name = "Мышца", ValueExt = -0.1m, ValueInt = -0.1m, ValueMP = 1m, Description = "Заклинание сужается/расширяется. Стандартно - 1гекс/секунду" },
                    new Construct { Id = 63, Kind = 3, Name = "Спокойствие", ValueExt = 0m, ValueInt = 0m, ValueMP = 1m, Description = "Проецирует спокойствие на объект. Снимает дебафф шока" },
                    new Construct { Id = 64, Kind = 3, Name = "Поиск", ValueExt = 0m, ValueInt = 0m, ValueMP = 1m, Description = "Задаёт поиск по определённому критерию" },
                    new Construct { Id = 65, Kind = 3, Name = "Область", ValueExt = -0.95m, ValueInt = 0m, ValueMP = 3m, Description = "Снижает стоимость расширения области заклинания до 1Ћ/10 м/м²" },
                    new Construct { Id = 200, Kind = 0, Name = " ", ValueExt = 1m, ValueInt = 1m, ValueMP = 1m, Description = "Пустота" },
                    new Construct { Id = 201, Kind = 1, Name = " ", ValueExt = 1m, ValueInt = 1m, ValueMP = 1m, Description = "Пустота" },
                    new Construct { Id = 202, Kind = 2, Name = " ", ValueExt = 1m, ValueInt = 1m, ValueMP = 1m, Description = "Пустота" },
                    new Construct { Id = 203, Kind = 3, Name = " ", ValueExt = 0m, ValueInt = 0m, ValueMP = 0m, Description = "Пустота" }
                );

            modelBuilder.Entity<Effect>().HasData(
                new { Id = 1, ConstructId = 27, Type = EffectType.Damage, Quantity = 1, DiceSides = 4, EffectDesc = " " },
                new { Id = 2, ConstructId = 35, Type = EffectType.Heal, Quantity = 1, DiceSides = 4, EffectDesc = " " },
                new { Id = 3, ConstructId = 38, Type = EffectType.HP, Quantity = 4, DiceSides = 0, EffectDesc = " " },
                new { Id = 4, ConstructId = 28, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Заклинание спрятано в одном из спектров восприятия (зрение/слух/обоняние/осязание/ощущение магии/другое)" },
                new { Id = 5, ConstructId = 29, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Позволяет связывать разные заклинания, что снижает время их создания на 1 секунду. Позволяет комбинировать компоненты, увеличивая скаляцию эффекта" },
                new { Id = 6, ConstructId = 30, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Скалируется. Просто повышает коэффициент" },
                new { Id = 7, ConstructId = 31, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Просто повышает коэффициент. НЕ скалируется" },
                new { Id = 8, ConstructId = 32, Type = EffectType.Modifier, Quantity = 0, DiceSides = 0, EffectDesc = "(2)" },
                new { Id = 9, ConstructId = 33, Type = EffectType.Modifier, Quantity = 0, DiceSides = 0, EffectDesc = "Даёт урон в половину кубиков осколками и заменяет тип урона на ex. Пример: 2d4 burn -> 2d4 ex [1d4]" },
                new { Id = 10, ConstructId = 34, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Заклинание не наносит вреда" },
                new { Id = 11, ConstructId = 36, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Наводит заклинание на цель. Скалируется. Базовая стоимость - отклонение от цели на 1 гекс/покрытие 1 успеха уклонения" },
                new { Id = 12, ConstructId = 37, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Ставит метку на определëнную цель. При соседстве с компонентом 'Мир' снижает стоимость до 10%" },
                new { Id = 13, ConstructId = 39, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Создаёт препятствие для определённого способа восприятия (Запах/звук/свет/иное)" },
                new { Id = 14, ConstructId = 40, Type = EffectType.Protection, Quantity = 3, DiceSides = 0, EffectDesc = "Скалируется. Развеется после прохождения урона через DR, если нет HP" },
                new { Id = 15, ConstructId = 41, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Поражает одну дополнительную цель, помеченную меткой" },
                new { Id = 16, ConstructId = 42, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Следует за целью, помеченной меткой или заклинателем. Скорость - 4 м/сек" },
                new { Id = 17, ConstructId = 43, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Распространяет эффект заклинания в виде вихря, расширяющегося со скоростью 2 гекс/секунда. Максимальная область - 6 гексов" },
                new { Id = 18, ConstructId = 44, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Создаëт фантомное изображение. Детализация зависит от умения мага" },
                new { Id = 19, ConstructId = 45, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = " " },
                new { Id = 20, ConstructId = 46, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Создаëт клеть вокруг объекта. SM>0 требует вдвое больше затрат" },
                new { Id = 21, ConstructId = 47, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Создаëт путы на всех конечностях объекта. Невозможно развязать" },
                new { Id = 22, ConstructId = 48, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Заклинание не воздействует на объект (камень/человек/меченый объект)" },
                new { Id = 23, ConstructId = 49, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Воздействует на цель с периодичностью в одну секунду, нанося половинный вред. Скалируется по времени" },
                new { Id = 24, ConstructId = 50, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Радиус = Ћ*2 метров. Аналогично дождю падает с неба" },
                new { Id = 25, ConstructId = 51, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Выдаëт доступную и не скрытую информацию об объекте" },
                new { Id = 26, ConstructId = 52, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Выдаëт информацию об объекте с +5 к результату соревнования" },
                new { Id = 27, ConstructId = 53, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Заворачивает следующее заклинание, расположенное правее по линии" },
                new { Id = 28, ConstructId = 54, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Переносит эффект заклинания на того, кто прикоснулся к объекту. Скалируется" },
                new { Id = 29, ConstructId = 55, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Усиливает определённый физический параметр объекта. До +4" },
                new { Id = 30, ConstructId = 56, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Усиливает атрибут объекта (ST, IQ, DX, HT). До +4" },
                new { Id = 31, ConstructId = 57, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Инвертирует действие заклинания. Работает не со всеми параметрами" },
                new { Id = 32, ConstructId = 58, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Соревнование навыков заклинания. Защита и атака получают бонусы за значения ‡ и †" },
                new { Id = 33, ConstructId = 59, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Перенаправляет заклинание в определëнной точке. Требует установки заранее" },
                new { Id = 34, ConstructId = 60, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Позволяет удалëнно почувствовать/увидеть/услышать. Цена за 10 метров" },
                new { Id = 35, ConstructId = 61, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Экранирует область заклинания, оставляя открытой лишь определëнную зону" },
                new { Id = 36, ConstructId = 62, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Заклинание сужается/расширяется. Стандартно - 1гекс/секунду" },
                new { Id = 37, ConstructId = 63, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Проецирует спокойствие на объект. Снимает дебафф шока" },
                new { Id = 38, ConstructId = 64, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Задаёт поиск по определённому критерию" },
                new { Id = 39, ConstructId = 65, Type = EffectType.EffectDesc, Quantity = 0, DiceSides = 0, EffectDesc = "Снижает стоимость расширения области заклинания до 1Ћ/10 м/м²" }
                );
        }
    }
}
