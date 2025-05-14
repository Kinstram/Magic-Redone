using System;

namespace Magic_Redone
{
    public class EffectResult
    {
        public EffectType Type { get; set; }
        public List<(int Quantity, int DiceSides)> DiceCombinations { get; set; } = new();
        public List<string> EffectDescs { get; set; } = new();

        public override string ToString()
        {
            string diceString = ""; // Обнуление строки при вызове функции

            // Группировка и фильтрация DiceCombinations. actionCombinations - эффекты, выражаемые в кубах (1d4 и т.п.)
            // sideCombinations - эффекты, выражаемые не в кубах
            List<(int Quantity, int DiceSides)> actionCombinations = DiceCombinations
                .Where(d => d.Quantity > 0 && d.DiceSides > 0)
                .GroupBy(d => d.DiceSides)
                .Select(g => (Quantity: g.Sum(x => x.Quantity), DiceSides: g.Key))
                .OrderBy(d => d.DiceSides)
                .ToList();

            List<(int Quantity, int DiceSides)> sideCombinations = DiceCombinations
                .Where(d => d.Quantity > 0 && d.DiceSides == 0)
                .GroupBy(d => d.Quantity)
                .Select(g => (Quantity: g.Sum(x => x.Quantity), DiceSides: 0))
                .ToList();

            // Суммирование Quantity(цифрового выражения) эффектов, формирование строки и добавление подписи типа ("урона", "HP" и т.д.)
            diceString = actionCombinations.Any()
                ? string.Join(" + ", actionCombinations.Select(d => $"{d.Quantity}d{d.DiceSides} {TypeToString()}"))
                : "";

            diceString += sideCombinations.Any()
                ? string.Join(" ", sideCombinations.Select(d => $"{d.Quantity} {TypeToString()}"))
                : "";

            // Получение и удаление повторяющихся описаний
            List<string> desc = EffectDescs
                .Where(d => !string.IsNullOrWhiteSpace(d))
                .Distinct()
                .ToList();

            // Преобразование описаний в единую строку
            string descString = desc.Any()
                ? string.Join(" ", desc)
                : "";

            return $"{diceString} {descString}";
        }
        private string TypeToString() => Type switch
        {
            EffectType.Damage => "cr урона",
            EffectType.Heal => "лечения",
            EffectType.Protection => "DR",
            EffectType.HP => "HP",
            _ => string.Empty
        };
    }
}
