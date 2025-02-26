using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Magic_Redone.Back;

namespace Magic_Redone
{
    [NotMapped]
    public class Effect
    {
        public EffectType Type { get; set; }
        public int Quantity { get; set; } // Количество кубиков
        public int DiceSize { get; set; } // Размер кубика (4, 8, 16 и т.д.)

        public string Display => $"{Quantity}d{DiceSize}";
    }
}
