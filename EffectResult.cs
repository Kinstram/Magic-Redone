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
        public int Quantity { get; set; }          // Суммарное количество
        public int DiceSides { get; set; }         // Общие грани кубика
        public List<string> EffectDescs { get; set; } // Список уникальных описаний

        public override string ToString()
        {
            var desc = EffectDescs.Any()
                ? $" ({string.Join(", ", EffectDescs)})"
                : "";

            return $"{Quantity}d{DiceSides}{desc} {TypeToString()}";
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
