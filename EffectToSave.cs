using System;

namespace Magic_Redone
{
    public class EffectToSave
    {
        public int Id { get; set; }
        public int SaveEntityId { get; set; }
        public SaveEntity SaveEntity { get; set; }
        public string EffectString { get; set; }
    }
}
