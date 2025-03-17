using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            var validCombinations = DiceCombinations
                .Where(d => d.Quantity > 0 && d.DiceSides > 0)
                .ToList();

            var grouped = validCombinations
                .GroupBy(d => d.DiceSides)
                .Select(g => (Quantity: g.Sum(x => x.Quantity), DiceSides: g.Key))
                .OrderBy(d => d.DiceSides)
                .ToList();

            var diceString = grouped.Any()
                ? string.Join(" + ", grouped.Select(d => $"{d.Quantity}d{d.DiceSides}"))
                : "";

            // Извлекаем числовое значение из EffectDescs (например, " (2)")
            var numberPart = EffectDescs
                .SelectMany(d => Regex.Matches(d, @"\((\d+)\)").Cast<Match>())
                .LastOrDefault()? // Берём последнее совпадение
                .Groups[1].Value;

            // Добавляем число к diceString, если найдено
            if (!string.IsNullOrEmpty(numberPart))
            {
                diceString += $" ({numberPart})";
            }

            // Формируем остальные описания (исключая числовые части в скобках)
            var desc = EffectDescs
                .Where(d => !string.IsNullOrWhiteSpace(d) && !Regex.IsMatch(d, @"\(\d+\)"))
                .Distinct()
                .ToList();

            var descString = desc.Any()
                ? string.Join(" ", desc)
                : "";

            // Комбинируем результат
            if (!string.IsNullOrEmpty(diceString) && !string.IsNullOrEmpty(descString))
                return $"{diceString} {TypeToString()} ({descString})";
            else if (!string.IsNullOrEmpty(diceString))
                return $"{diceString} {TypeToString()}";
            else if (!string.IsNullOrEmpty(descString))
                return descString;

            return "Нет эффектов";
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
