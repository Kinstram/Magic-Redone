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
        public int Quantity { get; set; }
        public int DiceSides { get; set; }
        public string EffectDesc { get; set; }

        public override string ToString() => Type switch
        {
            EffectType.Damage => $"{Quantity}d{DiceSides} урона",
            EffectType.Heal => $"{Quantity}d{DiceSides} лечения",
            EffectType.Protection => $"{Quantity} DR",
            EffectType.HP => $"{Quantity} HP",
            EffectType.EffectDesc => $"{EffectDesc}",
            _ => string.Empty
        };
    }
}
