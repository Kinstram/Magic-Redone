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
        public ICollection<Construct> SavedComponents { get; set; } = []; // Components
        public ICollection<Construct> SavedTrio { get; set; } = []; // 0 - Element, 1 - Method, 3 - Form 
        public ICollection<Int16> SavedScalations { get; set; } = [];
        public Int16 SelectedElementId { get; set; }
        public Int16 SelectedMethodId{ get; set; }
        public Int16 SelectedFormId { get; set; }

        public Int16 SelectedComponent1Id { get; set; }
        public Int16 SelectedScalation1 { get; set; }

        public Int16 SelectedComponent2Id { get; set; }
        public Int16 SelectedScalation2 { get; set; }

        public Int16 SelectedComponent3Id { get; set; }
        public Int16 SelectedScalation3 { get; set; }

        public Int16 SelectedComponent4Id { get; set; }
        public Int16 SelectedScalation4 { get; set; }

        public Int16 SelectedComponent5Id { get; set; }
        public Int16 SelectedScalation5 { get; set; }

        public Int16 SelectedComponent6Id { get; set; }
        public Int16 SelectedScalation6 { get; set; }

        public decimal CountedExt { get; set; }
        public decimal CountedInt { get; set; }
        public decimal CountedMP { get; set; }
    }
}
