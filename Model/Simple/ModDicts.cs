using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.ObjectModel;

namespace Magic_Redone.Simple
{
    internal class ModDicts
    {
        public static Dictionary<string, decimal> TimeExistDict { get; } = new Dictionary<string, decimal>
            {
                {"1 секунда", 1m},
                {"15 минут", 1.25m},
                {"Час", 2m},
                {"12 часов", 2.5m},
                {"Сутки", 3m},
                {"Неделя", 63m},
                {"Месяц", 90m},
                {"6 Месяцев", 270m},
                {"Год", 1095m}
            };

        public static Dictionary<string, decimal> FormDict { get; } = new Dictionary<string, decimal>
            {
                {" ", 1m},
                {"Вихрь", 2m},
                {"Дождь", 2m},
                {"Конус", 1.3m},
                {"Круг", 1.2m},
                {"Куб", 1.4m},
                {"Овал", 1.3m},
                {"Полусфера", 1.1m},
                {"Сфера", 1m},
                {"Треугольник", 1.1m}
            };

        public static Dictionary<string, decimal> MethodDict { get; } = new Dictionary<string, decimal>
            {
                {" ", 1m},
                {"Воплощение", 5.0m},
                {"Выпуск", 1.4m},
                {"Дестабилизация", 1.3m},
                {"Излучение", 1.2m},
                {"Касание", 0.8m},
                {"Луч", 1m},
                {"Насыщение", 0.2m},
                {"Самопоток", 0.8m},
                {"Снаряд", 1.4m},
                {"Точка", 1.2m},
                {"Покров", 1.2m}
            };

        public static Dictionary<string, decimal> TimeCastDict { get; } = new Dictionary<string, decimal>
            {
                {"Защитное", 1.5m},
                {"1 секунда", 1m},
                {"2 секунды", 0.95m},
                {"3 секунды", 0.9m},
                {"5 секунд", 0.85m},
                {"Минута", 0.8m},
                {"15 минут", 0.78m},
                {"30 минут", 0.75m},
                {"Час", 0.72m},
                {"2 часа", 0.7m},
                {"12 часов", 0.65m},
                {"24 часа", 0.6m},
                {"3 суток", 0.5m}
            };
    }
}
