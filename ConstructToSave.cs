using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic_Redone
{
    public class ConstructToSave
    {
        public int Id { get; set; } // Новый уникальный ID
        public Int16 ConstructId { get; set; } // Внешний ключ к Construct
        public Construct Construct { get; set; } // Навигационное свойство к Construct
        public int SaveEntityId { get; set; }  // Внешний ключ к SaveEntity
        public SaveEntity SaveEntity { get; set; }  // Навигационное свойство к SaveEntity
        public string SaveName { get; set; } // Для уникального поиска
    }
}