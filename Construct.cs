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
        public Int16 Id { get; set; }
        public Int16 Kind { get; set; }
        public string Name { get; set; }
        public decimal ValueExt { get; set; }
        public decimal ValueInt { get; set; }
        public decimal ValueMP { get; set; }
        public Effect Effect { get; set; }
        public Construct() { }
    }
}
