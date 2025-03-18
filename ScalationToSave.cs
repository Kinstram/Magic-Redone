using System.Collections.Generic;

namespace Magic_Redone
{
    public class ScalationToSave
    {
        public int Id { get; set; }
        public Int16 Value { get; set; }
        public int SaveEntityId { get; set; }  // Внешний ключ к SaveEntity
        public SaveEntity SaveEntity { get; set; }  // Навигационное свойство к SaveEntity
    }
}
