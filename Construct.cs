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
        public Construct() { }
        public Construct (string a, decimal b, decimal c, decimal d) { Name = a; ValueExt = b; ValueInt = c; ValueMP = d; }
        public Construct(Int16 a, Int16 b, string c, decimal d, decimal e, decimal f) { Id = a; Kind = b; Name = c; ValueExt = d; ValueInt = e; ValueMP = f; }
    }
}
