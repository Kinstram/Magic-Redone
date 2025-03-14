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
        public List<ConstructToSave> SavedComponents { get; set; } = new List<ConstructToSave>();
        public List<ScalationToSave> SavedScalations { get; set; } = new List<ScalationToSave>();
        public List<EffectToSave> SavedEffects { get; set; } = new List<EffectToSave>();
        public decimal CountedExt { get; set; }
        public decimal CountedInt { get; set; }
        public decimal CountedMP { get; set; }
        public string TimeString {  get; set; }
        public decimal TimeValue { get; set; }
    }
}
