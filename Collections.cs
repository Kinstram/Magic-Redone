using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic_Redone
{
    public class Collections
    {
        public ObservableCollection<Construct> Elements { get; set; }
        public ObservableCollection<Construct> Methods {get; set; }
        public ObservableCollection<Construct> Forms {get; set; } 
        public ObservableCollection<Construct> Components { get; set; }
        public ObservableCollection<Int16> Scalations { get; set; }
    }
}
