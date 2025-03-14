using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic_Redone
{
    public class EffectResult
    {
        public EffectType Type { get; set; }
        public List<(int Quantity, int DiceSides)> DiceCombinations { get; set; } = new();
        public List<string> EffectDescs { get; set; } = new();

        public override string ToString()
        {
            // Фильтруем некорректные комбинации (Quantity или DiceSides <= 0)
            var validCombinations = DiceCombinations
                .Where(d => d.Quantity > 0 && d.DiceSides > 0)
                .ToList();

            // Группируем и сортируем только валидные комбинации
            var grouped = validCombinations
                .GroupBy(d => d.DiceSides)
                .Select(g => (Quantity: g.Sum(x => x.Quantity), DiceSides: g.Key))
                .OrderBy(d => d.DiceSides)
                .ToList();

            // Формируем части строки
            var diceString = grouped.Any()
                ? string.Join(" + ", grouped.Select(d => $"{d.Quantity}d{d.DiceSides}"))
                : "";

            var desc = EffectDescs
                .Where(d => !string.IsNullOrWhiteSpace(d))
                .Distinct()
                .ToList();

            var descString = desc.Any()
                ? string.Join(", ", desc)
                : "";

            // Комбинируем результат
            if (grouped.Any() && desc.Any())
                return $"{diceString} {TypeToString()} ({descString})";

            else if (grouped.Any())
                return $"{diceString} {TypeToString()}";

            else if (desc.Any()) 
                return descString;

            else
            return "Нет эффектов"; // Fallback на случай пустого результата

        }

        private string TypeToString() => Type switch
        {
            EffectType.Damage => "урона",
            EffectType.Heal => "лечения",
            EffectType.Protection => "DR",
            EffectType.HP => "HP",
            _ => string.Empty
        };
    }
}
