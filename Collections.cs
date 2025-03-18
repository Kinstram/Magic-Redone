using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Magic_Redone
{
    public class Collections
    {
        // Коллекции для привязки данных в ComboBox и ListBox в MainWindow.xaml. Дают списки данных для выбора в интерфейсе
        public ObservableCollection<Construct> Elements { get; set; }
        public ObservableCollection<Construct> Methods {get; set; }
        public ObservableCollection<Construct> Forms {get; set; } 
        public ObservableCollection<Construct> Components { get; set; }
        public ObservableCollection<Int16> Scalations { get; set; }
        public ObservableCollection<String> Time {  get; set; }
    }
}
