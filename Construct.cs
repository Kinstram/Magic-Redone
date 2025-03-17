using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Magic_Redone
{

    public class Construct
    {

        public int Id { get; set; }
        public int Kind { get; set; }

        public string Name { get; set; }

        public decimal ValueExt { get; set; }
        public decimal ValueInt { get; set; }
        public decimal ValueMP { get; set; }

        public string Description { get; set; }

        public Effect TiedEffect { get; set; }

        public Construct() { }
    }

    public class Effect
    {
        public int Id { get; set; }
        public int ConstructId { get; set; }
        public Construct Construct { get; set; }

        public EffectType Type { get; set; }

        public int Quantity { get; set; }

        public int DiceSides { get; set; }

        public string EffectDesc { get; set; }

        public Effect() { }

    }

    public enum EffectType
    {
        None,
        Damage,
        Heal,
        Protection,
        HP,
        Modifier,
        EffectDesc
    }
}
