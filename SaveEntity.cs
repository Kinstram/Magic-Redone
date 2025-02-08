using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Magic_Redone
{
    public class SaveEntity
    {
        [Key]
        public int Id { get; set; }
        public string SaveName { get; set; }
        public ICollection<ConstructToSave> SavedComponents { get; set; } = new List<ConstructToSave>();
        public ICollection<ConstructToSave> SavedTrio { get; set; } = new List<ConstructToSave>();
        public ICollection<ScalationToSave> SavedScalations { get; set; } = new List<ScalationToSave>();
        public decimal CountedExt { get; set; }
        public decimal CountedInt { get; set; }
        public decimal CountedMP { get; set; }

    }
}
