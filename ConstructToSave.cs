using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic_Redone
{
    public class ConstructToSave
    {
        public int Id { get; set; }
        public int OriginalId { get; set; } // ID из основной базы (только для справки)
        public string Name { get; set; }
        public decimal ValueExt { get; set; }
        public decimal ValueInt { get; set; }
        public decimal ValueMP { get; set; }
        public int SaveEntityId { get; set; }
        public SaveEntity SaveEntity { get; set; }
    }
}