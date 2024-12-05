using System.Collections.ObjectModel;
using System.Windows;

namespace Magic_Redone
{
    public class Results
    {
        //конечная отправка в интерфейс
        public Collections Collections { get; set; } = new();
        public Getter Getter { get; set; } = new();
        public Results()
        {
            Back.LoadElements(Collections);

        }
    }
}
