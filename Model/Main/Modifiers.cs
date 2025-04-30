using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic_Redone
{
    public class Modifiers
    {
        // Словари для модификации выбранных компонентов. (Скаляция, время)
        public static Dictionary<(string componentName, int scalationLevel), Construct> ScalableDict()
        {
            var componentData = new Dictionary<(string componentName, int scalationLevel), Construct>()
             {
                { ("Жало", 0), new Construct { ValueExt = 0m, ValueInt = -0.1m, ValueMP = 0.5m } },
                { ("Жало", 1), new Construct { ValueExt = 0m, ValueInt = -0.2m, ValueMP = 1m} },
                { ("Жало", 2), new Construct { ValueExt = 0m, ValueInt = -0.4m, ValueMP = 2m } },
                { ("Жало", 3), new Construct { ValueExt = 0m, ValueInt = -0.8m, ValueMP = 4m } },
                { ("Жало", 4), new Construct { ValueExt = 0m, ValueInt = -1.6m, ValueMP = 8m} },
                { ("Жало", 5), new Construct { ValueExt = 0m, ValueInt = -3.2m, ValueMP = 16m } },

                { ("Кость", 0), new Construct { ValueExt = 0.2m, ValueInt = -0.1m, ValueMP = 0.2m} },
                { ("Кость", 1), new Construct { ValueExt = 0.4m, ValueInt = -0.2m, ValueMP = 0.4m} },
                { ("Кость", 2), new Construct { ValueExt = 0.8m, ValueInt = -0.4m, ValueMP = 0.8m} },
                { ("Кость", 3), new Construct { ValueExt = 1.6m, ValueInt = -0.8m, ValueMP = 1.6m} },
                { ("Кость", 4), new Construct { ValueExt = 3.2m, ValueInt = -1.6m, ValueMP = 3.2m} },
                { ("Кость", 5), new Construct { ValueExt = 6.4m, ValueInt = -3.2m, ValueMP = 6.4m} },

                { ("Наконечник", 0), new Construct { ValueExt = 0m, ValueInt = 0m, ValueMP = 3m} },
                { ("Наконечник", 1), new Construct { ValueExt = 0m, ValueInt = 0m, ValueMP = 6m} },
                { ("Наконечник", 2), new Construct { ValueExt = 0m, ValueInt = 0m, ValueMP = 12m} },
                { ("Наконечник", 3), new Construct { ValueExt = 0m, ValueInt = 0m, ValueMP = 24m} },
                { ("Наконечник", 4), new Construct { ValueExt = 0m, ValueInt = 0m, ValueMP = 48m} },
                { ("Наконечник", 5), new Construct { ValueExt = 0m, ValueInt = 0m, ValueMP = 96m} },

                { ("Длань", 0), new Construct { ValueExt = 0m, ValueInt = -0.1m, ValueMP = 0.5m} },
                { ("Длань", 1), new Construct { ValueExt = 0m, ValueInt = -0.2m, ValueMP = 1m} },
                { ("Длань", 2), new Construct { ValueExt = 0m, ValueInt = -0.4m, ValueMP = 2m} },
                { ("Длань", 3), new Construct { ValueExt = 0m, ValueInt = -0.8m, ValueMP = 4m} },
                { ("Длань", 4), new Construct { ValueExt = 0m, ValueInt = -1.6m, ValueMP = 8m} },
                { ("Длань", 5), new Construct { ValueExt = 0m, ValueInt = -3.2m, ValueMP = 16m} },

                { ("Лех'сар", 0), new Construct { ValueExt = 0m, ValueInt = 0m, ValueMP = 1m} },
                { ("Лех'сар", 1), new Construct { ValueExt = 0m, ValueInt = 0m, ValueMP = 2m} },
                { ("Лех'сар", 2), new Construct { ValueExt = 0m, ValueInt = 0m, ValueMP = 4m} },
                { ("Лех'сар", 3), new Construct { ValueExt = 0m, ValueInt = 0m, ValueMP = 8m} },
                { ("Лех'сар", 4), new Construct { ValueExt = 0m, ValueInt = 0m, ValueMP = 16m} },
                { ("Лех'сар", 5), new Construct { ValueExt = 0m, ValueInt = 0m, ValueMP = 32m} },

                { ("Щит", 0), new Construct { ValueExt = -0.2m, ValueInt = 0.2m, ValueMP = 1m} },
                { ("Щит", 1), new Construct { ValueExt = -0.4m, ValueInt = 0.4m, ValueMP = 2m} },
                { ("Щит", 2), new Construct { ValueExt = -0.8m, ValueInt = 0.8m, ValueMP = 4m} },
                { ("Щит", 3), new Construct { ValueExt = -1.6m, ValueInt = 1.6m, ValueMP = 8m} },
                { ("Щит", 4), new Construct { ValueExt = -3.2m, ValueInt = 3.2m, ValueMP = 16m} },
                { ("Щит", 5), new Construct { ValueExt = -6.4m, ValueInt = 6.4m, ValueMP = 32m} },

                { ("Панцирь", 0), new Construct { ValueExt = -0.4m, ValueInt = 0.2m, ValueMP = 2m} },
                { ("Панцирь", 1), new Construct { ValueExt = -0.8m, ValueInt = 0.4m, ValueMP = 4m} },
                { ("Панцирь", 2), new Construct { ValueExt = -1.2m, ValueInt = 0.8m, ValueMP = 8m} },
                { ("Панцирь", 3), new Construct { ValueExt = -2.4m, ValueInt = 1.6m, ValueMP = 16m} },
                { ("Панцирь", 4), new Construct { ValueExt = -4.8m, ValueInt = 3.2m, ValueMP = 32m} },
                { ("Панцирь", 5), new Construct { ValueExt = -9.6m, ValueInt = 6.4m, ValueMP = 64m} },

                { ("Концентрация", 0), new Construct { ValueExt = 0m, ValueInt = 0.5m, ValueMP = 1m} },
                { ("Концентрация", 1), new Construct { ValueExt = 0m, ValueInt = 1m, ValueMP = 2m} },
                { ("Концентрация", 2), new Construct { ValueExt = 0m, ValueInt = 2m, ValueMP = 4m} },
                { ("Концентрация", 3), new Construct { ValueExt = 0m, ValueInt = 4m, ValueMP = 8m} },
                { ("Концентрация", 4), new Construct { ValueExt = 0m, ValueInt = 8m, ValueMP = 16m} },
                { ("Концентрация", 5), new Construct { ValueExt = 0m, ValueInt = 16m, ValueMP = 32m} },

                { ("Змей", 0), new Construct { ValueExt = -0.2m, ValueInt = 0m, ValueMP = 1m} },
                { ("Змей", 1), new Construct { ValueExt = -0.4m, ValueInt = 0m, ValueMP = 2m} },
                { ("Змей", 2), new Construct { ValueExt = -0.8m, ValueInt = 0m, ValueMP = 4m} },
                { ("Змей", 3), new Construct { ValueExt = -1.6m, ValueInt = 0m, ValueMP = 8m} },
                { ("Змей", 4), new Construct { ValueExt = -3.2m, ValueInt = 0m, ValueMP = 16m} },
                { ("Змей", 5), new Construct { ValueExt = -6.4m, ValueInt = 0m, ValueMP = 32m} },

                { ("Дождь", 0), new Construct { ValueExt = -0.2m, ValueInt = 0m, ValueMP = 2m} },
                { ("Дождь", 1), new Construct { ValueExt = -0.4m, ValueInt = 0m, ValueMP = 4m} },
                { ("Дождь", 2), new Construct { ValueExt = -0.8m, ValueInt = 0m, ValueMP = 8m} },
                { ("Дождь", 3), new Construct { ValueExt = -1.6m, ValueInt = 0m, ValueMP = 16m} },
                { ("Дождь", 4), new Construct { ValueExt = -3.2m, ValueInt = 0m, ValueMP = 32m} },
                { ("Дождь", 5), new Construct { ValueExt = -6.4m, ValueInt = 0m, ValueMP = 64m} },

                { ("Проклятие", 0), new Construct { ValueExt = -0.2m, ValueInt = 0m, ValueMP = 2m} },
                { ("Проклятие", 1), new Construct { ValueExt = -0.4m, ValueInt = 0m, ValueMP = 4m} },
                { ("Проклятие", 2), new Construct { ValueExt = -0.8m, ValueInt = 0m, ValueMP = 8m} },
                { ("Проклятие", 3), new Construct { ValueExt = -1.6m, ValueInt = 0m, ValueMP = 16m} },
                { ("Проклятие", 4), new Construct { ValueExt = -3.2m, ValueInt = 0m, ValueMP = 32m} },
                { ("Проклятие", 5), new Construct { ValueExt = -6.4m, ValueInt = 0m, ValueMP = 64m} },

                { ("Мыщца", 0), new Construct { ValueExt = -0.1m, ValueInt = -0.1m, ValueMP = 1m} },
                { ("Мыщца", 1), new Construct { ValueExt = -0.2m, ValueInt = -0.2m, ValueMP = 2m} },
                { ("Мыщца", 2), new Construct { ValueExt = -0.4m, ValueInt = -0.4m, ValueMP = 4m} },
                { ("Мыщца", 3), new Construct { ValueExt = -0.8m, ValueInt = -0.8m, ValueMP = 8m} },
                { ("Мыщца", 4), new Construct { ValueExt = -1.6m, ValueInt = -1.6m, ValueMP = 16m} },
                { ("Мыщца", 5), new Construct { ValueExt = -3.2m, ValueInt = -3.2m, ValueMP = 32m} },
             };
            return componentData;
        }

        public static Dictionary<string, decimal> TimeDict()
        {
            var data = new Dictionary<string, decimal>()
            {
                {"1 Секунда", 1m},
                {"15 Минут", 1.25m},
                {"1 Час", 2m},
                {"12 Часов", 2.5m},
                {"1 Сутки", 3m},
                {"1 Неделя", 63m},
                {"1 Месяц", 90m},
                {"6 Месяцев", 270m},
                {"1 Год", 1095m},
            };
            return data;
        }

        public static Dictionary<(string componentName, int scalationLevel), Effect> EffectScalation()
        {
            var effectData = new Dictionary<(string componentName, int scalationLevel), Effect>()
             {
                { ("Жало", 0), new Effect { DiceSides = 4 } },
                { ("Жало", 1), new Effect { DiceSides = 6 } },
                { ("Жало", 2), new Effect { DiceSides = 8 } },
                { ("Жало", 3), new Effect { DiceSides = 10 } },
                { ("Жало", 4), new Effect { DiceSides = 12 } },
                { ("Жало", 5), new Effect { DiceSides = 14 } },

                { ("Наконечник", 0), new Effect { EffectDesc = "(2)" } },
                { ("Наконечник", 1), new Effect { EffectDesc = "(3)" } },
                { ("Наконечник", 2), new Effect { EffectDesc = "(5)" } },
                { ("Наконечник", 3), new Effect { EffectDesc = "(10)" } },
                { ("Наконечник", 4), new Effect { EffectDesc = "(20)" } },
                { ("Наконечник", 5), new Effect { EffectDesc = "(∞)" } },

                { ("Длань", 0), new Effect { DiceSides = 4 } },
                { ("Длань", 1), new Effect { DiceSides = 6 } },
                { ("Длань", 2), new Effect { DiceSides = 8 } },
                { ("Длань", 3), new Effect { DiceSides = 16 } },
                { ("Длань", 4), new Effect { DiceSides = 32 } },
                { ("Длань", 5), new Effect { DiceSides = 64 } },

                { ("Лех'сар", 0), new Effect { EffectDesc = "Отклонение от цели на 1 гекса/покрытие 1 успеха уклонения." } },
                { ("Лех'сар", 1), new Effect { EffectDesc = "Отклонение от цели на 2 гекса/покрытие 2 успеха уклонения." } },
                { ("Лех'сар", 2), new Effect { EffectDesc = "Отклонение от цели на 4 гекса/покрытие 4 успеха уклонения." } },
                { ("Лех'сар", 3), new Effect { EffectDesc = "Отклонение от цели на 8 гекса/покрытие 8 успеха уклонения." } },
                { ("Лех'сар", 4), new Effect { EffectDesc = "Отклонение от цели на 16 гекса/покрытие 16 успеха уклонения." } },
                { ("Лех'сар", 5), new Effect { EffectDesc = "Отклонение от цели на 32 гекса/покрытие 32 успеха уклонения." } },

                { ("Щит", 0), new Effect { Quantity = 4 } },
                { ("Щит", 1), new Effect { Quantity = 8 } },
                { ("Щит", 2), new Effect { Quantity = 16 } },
                { ("Щит", 3), new Effect { Quantity = 32 } },
                { ("Щит", 4), new Effect { Quantity = 64 } },
                { ("Щит", 5), new Effect { Quantity = 128 } },

                { ("Панцирь", 0), new Effect { Quantity = 3 } },
                { ("Панцирь", 1), new Effect { Quantity = 6 } },
                { ("Панцирь", 2), new Effect { Quantity = 12 } },
                { ("Панцирь", 3), new Effect { Quantity = 24 } },
                { ("Панцирь", 4), new Effect { Quantity = 48 } },
                { ("Панцирь", 5), new Effect { Quantity = 96 } },

                { ("Змей", 0), new Effect { EffectDesc = "Следующую секунду, раз в секунду, наносит половинный вред заклинания." } },
                { ("Змей", 1), new Effect { EffectDesc = "Следующие две секунды, раз в секунду, наносит половинный вред заклинания." } },
                { ("Змей", 2), new Effect { EffectDesc = "Следующие четыре секунды, раз в секунду, наносит половинный вред заклинания." } },
                { ("Змей", 3), new Effect { EffectDesc = "Следующие восемь секунд, раз в секунду, наносит половинный вред заклинания." } },
                { ("Змей", 4), new Effect { EffectDesc = "Следующие шестнадцать секунды, раз в секунду, наносит половинный вред заклинания." } },
                { ("Змей", 5), new Effect { EffectDesc = "Следующие тридцать две секунды, раз в секунду, наносит половинный вред заклинания." } },

                { ("Дождь", 0), new Effect { EffectDesc = "Радиус 4 метра." } },
                { ("Дождь", 1), new Effect { EffectDesc = "Радиус 8 метров." } },
                { ("Дождь", 2), new Effect { EffectDesc = "Радиус 16 метров." } },
                { ("Дождь", 3), new Effect { EffectDesc = "Радиус 32 метра." } },
                { ("Дождь", 4), new Effect { EffectDesc = "Радиус 64 метров." } },
                { ("Дождь", 5), new Effect { EffectDesc = "Радиус 128 метров." } },

                { ("Проклятие", 0), new Effect { EffectDesc = "Переносит эффект заклинания на прикоснувшегося к объекту заклинания." } },
                { ("Проклятие", 1), new Effect { EffectDesc = "Дважды переносит эффект заклинания на прикоснувшегося к объекту заклинания." } },
                { ("Проклятие", 2), new Effect { EffectDesc = "Четырежды переносит эффект заклинания на прикоснувшегося к объекту заклинания." } },
                { ("Проклятие", 3), new Effect { EffectDesc = "Восемь раз переносит эффект заклинания на прикоснувшегося к объекту заклинания." } },
                { ("Проклятие", 4), new Effect { EffectDesc = "Шестнадцать раз переносит эффект заклинания на прикоснувшегося к объекту заклинания." } },
                { ("Проклятие", 5), new Effect { EffectDesc = "Тридцать два раза переносит эффект заклинания на прикоснувшегося к объекту заклинания." } },

                { ("Мышца", 0), new Effect { EffectDesc = "Заклинание сжимается/разжимается на 1 гекс в секунду" } },
                { ("Мышца", 1), new Effect { EffectDesc = "Заклинание сжимается/разжимается на 2 гекса в секунду." } },
                { ("Мышца", 2), new Effect { EffectDesc = "Заклинание сжимается/разжимается на 4 гекса в секунду" } },
                { ("Мышца", 3), new Effect { EffectDesc = "Заклинание сжимается/разжимается на 8 гекс в секунду" } },
                { ("Мышца", 4), new Effect { EffectDesc = "Заклинание сжимается/разжимается на 16 гекса в секунду." } },
                { ("Мышца", 5), new Effect { EffectDesc = "Заклинание сжимается/разжимается на 32 гекса в секунду" } },
             };
            return effectData;
        }
    }
}
