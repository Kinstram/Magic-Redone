using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Magic_Redone
{
    internal class Back //обработка
    {
        internal static async void LoadElements(Collections Collections)
        {
            using (var context = new ApplicationContext())
            {

                Int16 requiredKind = 0; //стихии
                var elementsLoad = await context.Constructs.OrderBy(p => p.Name).Where(c => c.Kind == requiredKind).ToListAsync();
                Collections.Elements = new ObservableCollection<Construct>(elementsLoad);

                requiredKind = 1; //способы
                var methodsLoad = await context.Constructs.OrderBy(p => p.Name).Where(c => c.Kind == requiredKind).ToListAsync();
                Collections.Methods = new ObservableCollection<Construct>(methodsLoad);

                requiredKind = 2; //формы
                var formsLoad = await context.Constructs.OrderBy(p => p.Name).Where(c => c.Kind == requiredKind).ToListAsync();
                Collections.Forms = new ObservableCollection<Construct>(formsLoad);

                requiredKind = 3; //компоненты
                var componentsLoad = await context.Constructs.OrderBy(p => p.Name).Where(c => c.Kind == requiredKind).ToListAsync();
                Collections.Components = new ObservableCollection<Construct>(componentsLoad);
            }

            Collections.Scalations = [1, 2, 3];
        } //загрузка данных в коллекции для вывода в ComboBox
        public static void ResultCount(Getter Getter)
        {
            Scalation(Getter);
            //инициализация и обнуление переменных для подсчёта Element, Method и Form. Сделано отдельно, чтобы не множить на 0 при пустых компонентах
            decimal CountedTrioExt = 1m;
            decimal CountedTrioInt = 1m;
            decimal CountedTrioMP = 1m;

            //умножение записей коллекции трио друг на друга
            foreach (var el in Getter.SelectedTrio.Where(e => e != null))
            {
                CountedTrioExt *= el.ValueExt;
                CountedTrioInt *= el.ValueInt;
                CountedTrioMP *= el.ValueMP;
            }

            //инициализация и обнуление переменных для подсчёта SelectedComponents 1..6
            decimal CountedComponentsExt = 0m;
            decimal CountedComponentsInt = 0m;
            decimal CountedComponentsMP = 0m;

            //суммирование записей коллекции с компонентам
            foreach (var comp in Getter.SelectedComponents.Where(e => e != null))
            {
                CountedComponentsExt += comp.ValueExt;
                CountedComponentsInt += comp.ValueInt;
                CountedComponentsMP += comp.ValueMP;
            }

            //итоговое суммирование произведения трио и суммы компонентов для дальнейшей передачи в Getter и WPF
            Getter.CountedExt = CountedTrioExt + CountedComponentsExt;
            Getter.CountedInt = CountedTrioInt + CountedComponentsInt;
            Getter.CountedMP = CountedTrioMP + CountedComponentsMP;

            Getter.CountedExt = Math.Round(Getter.CountedExt, 2);
            Getter.CountedInt = Math.Round(Getter.CountedInt, 2);
            Getter.CountedMP = Math.Round(Getter.CountedMP, 2);
        }
        internal static void Scalation(Getter Getter)
        {
            var compDict = ScalableComponents.ScalableDict();
            for (Int16 i = 0; i < Getter.SelectedComponents.Count; i++)
            {
                if (compDict.TryGetValue((Getter.SelectedComponents[i].Name, Getter.SelectedScalations[i]), out var data))
                {
                    Getter.SelectedComponents[i].ValueExt = data.ValueExt;
                    Getter.SelectedComponents[i].ValueInt = data.ValueInt;
                    Getter.SelectedComponents[i].ValueMP = data.ValueMP;
                }
            }
        }
    }
}
