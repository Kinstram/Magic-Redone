using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Magic_Redone.Back;

namespace Magic_Redone
{
    public class Modifiers
    {
        public static Dictionary<(string componentName, Int16 scalationLevel), Construct> ScalableDict()
        {
            var componentData = new Dictionary<(string componentName, Int16 scalationLevel), Construct>()
             {
                { ("Жало", 1), new Construct { ValueExt = 0m, ValueInt = -0.1m, ValueMP = 0.5m} },
                { ("Жало", 2), new Construct { ValueExt = 0m, ValueInt = -0.2m, ValueMP = 1m} },
                { ("Жало", 3), new Construct { ValueExt = 0m, ValueInt = -0.4m, ValueMP = 2m } },

                { ("Кость", 1), new Construct { ValueExt = 0.2m, ValueInt = -0.1m, ValueMP = 0.2m} },
                { ("Кость", 2), new Construct { ValueExt = 0.4m, ValueInt = -0.2m, ValueMP = 0.4m} },
                { ("Кость", 3), new Construct { ValueExt = 0.8m, ValueInt = -0.4m, ValueMP = 0.8m} },

                { ("Наконечник", 1), new Construct { ValueExt = 0m, ValueInt = 0m, ValueMP = 3m} },
                { ("Наконечник", 2), new Construct { ValueExt = 0m, ValueInt = 0m, ValueMP = 6m} },
                { ("Наконечник", 3), new Construct { ValueExt = 0m, ValueInt = 0m, ValueMP = 12m} },

                { ("Длань", 1), new Construct { ValueExt = 0m, ValueInt = -0.1m, ValueMP = 0.5m} },
                { ("Длань", 2), new Construct { ValueExt = 0m, ValueInt = -0.2m, ValueMP = 1m} },
                { ("Длань", 3), new Construct { ValueExt = 0m, ValueInt = -0.4m, ValueMP = 2m} },

                { ("Лех'сар", 1), new Construct { ValueExt = 0m, ValueInt = 0m, ValueMP = 1m} },
                { ("Лех'сар", 2), new Construct { ValueExt = 0m, ValueInt = 0m, ValueMP = 2m} },
                { ("Лех'сар", 3), new Construct { ValueExt = 0m, ValueInt = 0m, ValueMP = 4m} },

                { ("Щит", 1), new Construct { ValueExt = -0.2m, ValueInt = 0.2m, ValueMP = 1m} },
                { ("Щит", 2), new Construct { ValueExt = -0.4m, ValueInt = 0.4m, ValueMP = 2m} },
                { ("Щит", 3), new Construct { ValueExt = -0.8m, ValueInt = 0.8m, ValueMP = 4m} },

                { ("Панцирь", 1), new Construct { ValueExt = -0.4m, ValueInt = 0.2m, ValueMP = 2m} },
                { ("Панцирь", 2), new Construct { ValueExt = -0.8m, ValueInt = 0.4m, ValueMP = 4m} },
                { ("Панцирь", 3), new Construct { ValueExt = -1.2m, ValueInt = 0.8m, ValueMP = 8m} },

                { ("Концентрация", 1), new Construct { ValueExt = 0m, ValueInt = 0.5m, ValueMP = 1m} },
                { ("Концентрация", 2), new Construct { ValueExt = 0m, ValueInt = 1m, ValueMP = 2m} },
                { ("Концентрация", 3), new Construct { ValueExt = 0m, ValueInt = 2m, ValueMP = 4m} },

                { ("Змей", 1), new Construct { ValueExt = -0.2m, ValueInt = 0m, ValueMP = 1m} },
                { ("Змей", 2), new Construct { ValueExt = -0.4m, ValueInt = 0m, ValueMP = 2m} },
                { ("Змей", 3), new Construct { ValueExt = -0.8m, ValueInt = 0m, ValueMP = 4m} },

                { ("Дождь", 1), new Construct { ValueExt = -0.2m, ValueInt = 0m, ValueMP = 2m} },
                { ("Дождь", 2), new Construct { ValueExt = -0.4m, ValueInt = 0m, ValueMP = 4m} },
                { ("Дождь", 3), new Construct { ValueExt = -0.8m, ValueInt = 0m, ValueMP = 8m} },

                { ("Проклятие", 1), new Construct { ValueExt = -0.2m, ValueInt = 0m, ValueMP = 2m} },
                { ("Проклятие", 2), new Construct { ValueExt = -0.4m, ValueInt = 0m, ValueMP = 4m} },
                { ("Проклятие", 3), new Construct { ValueExt = -0.8m, ValueInt = 0m, ValueMP = 8m} },

                { ("Мыщца", 1), new Construct { ValueExt = -0.1m, ValueInt = -0.1m, ValueMP = 1m} },
                { ("Мыщца", 2), new Construct { ValueExt = -0.2m, ValueInt = -0.2m, ValueMP = 2m} },
                { ("Мыщца", 3), new Construct { ValueExt = -0.4m, ValueInt = -0.4m, ValueMP = 4m} },
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

        public static Dictionary<string, (int BaseDiceSize, double Multiplier)> ScalableEffectsData = new()
        {
            { "Жало", (BaseDiceSize: 4, Multiplier: 2) }, // d4 → d8 → d16
            { "Щит", (BaseDiceSize: 3, Multiplier: 2) },  // DR: 3 → 6 → 12 (не кубики, но для примера)
        };
    }
}
