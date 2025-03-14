using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic_Redone
{
    public class EffectToSave
    {
        public int Id { get; set; }
        public int SaveEntityId { get; set; }
        public SaveEntity SaveEntity { get; set; }
        public EffectType Type { get; set; }
        public List<DiceCombination> DiceCombinations { get; set; } = new();
        public List<string> EffectDescs { get; set; } = new();

        public class DiceCombination
        {
            public int Id { get; set; }
            public int Quantity { get; set; }
            public int DiceSides { get; set; }
            public int EffectToSaveId { get; set; } // Внешний ключ
            public EffectToSave EffectToSave { get; set; } // Навигационное свойство
        }
    }
}
