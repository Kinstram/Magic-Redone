using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace Magic_Redone
{
    public class Back //обработка
    {
        internal static async void LoadElements(Collections Collections)
        {
            using (var context = new ApplicationContext())
            {

                Int16 requiredKind = 0; //стихии
                var elementsLoad = await context.Constructs.AsNoTracking().OrderBy(p => p.Name).Where(c => c.Kind == requiredKind).ToListAsync();
                Collections.Elements = new ObservableCollection<Construct>(elementsLoad);

                requiredKind = 1; //способы
                var methodsLoad = await context.Constructs.AsNoTracking().OrderBy(p => p.Name).Where(c => c.Kind == requiredKind).ToListAsync();
                Collections.Methods = new ObservableCollection<Construct>(methodsLoad);

                requiredKind = 2; //формы
                var formsLoad = await context.Constructs.AsNoTracking().OrderBy(p => p.Name).Where(c => c.Kind == requiredKind).ToListAsync();
                Collections.Forms = new ObservableCollection<Construct>(formsLoad);

                requiredKind = 3; //компоненты
                var componentsLoad = await context.Constructs.AsNoTracking().OrderBy(p => p.Name).Where(c => c.Kind == requiredKind).ToListAsync();
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
        public static void SaveToLists(MainWindow main)
        {
            var Results = (Results)main.DataContext;
            var Getter = Results.Getter;

            List<Construct> componentsToSave = new List<Construct>();
            List<Construct> trioToSave = new List<Construct>();
            List<Int16> scalationsToSave = new List<Int16>();

            componentsToSave.Add(Getter.SelectedComponent1);
            componentsToSave.Add(Getter.SelectedComponent2);
            componentsToSave.Add(Getter.SelectedComponent3);
            componentsToSave.Add(Getter.SelectedComponent4);
            componentsToSave.Add(Getter.SelectedComponent5);
            componentsToSave.Add(Getter.SelectedComponent6);


            scalationsToSave.Add(Getter.SelectedScalation1);
            scalationsToSave.Add(Getter.SelectedScalation2);
            scalationsToSave.Add(Getter.SelectedScalation3);
            scalationsToSave.Add(Getter.SelectedScalation4);
            scalationsToSave.Add(Getter.SelectedScalation5);
            scalationsToSave.Add(Getter.SelectedScalation6);


            trioToSave.Add(Getter.SelectedElement);
            trioToSave.Add(Getter.SelectedMethod);
            trioToSave.Add(Getter.SelectedForm);
            if (string.IsNullOrEmpty(main.txtSave.Text))
            {
                MessageBox.Show("Введите название сохранения!");
                return;
            }


            using (var context = new SaveContext())
            {
                try
                {
                    var saveData = new SaveEntity
                    {
                        SaveName = main.txtSave.Text,
                        CountedExt = Getter.CountedExt,
                        CountedInt = Getter.CountedInt,
                        CountedMP = Getter.CountedMP,
                    };

                    context.Saves.Add(saveData);
                    context.SaveChanges();

                    foreach (var component in componentsToSave)
                    {
                        var existingConstruct = context.Constructs.Find(component.Id);
                        if (existingConstruct == null)
                        {
                            context.Constructs.Add(component);
                            context.SaveChanges();
                            existingConstruct = component;
                        }

                        var constructToSave = new ConstructToSave()
                        {
                            ConstructId = existingConstruct.Id,
                            SaveEntityId = saveData.Id,
                            SaveName = saveData.SaveName
                        };
                        context.ConstructsToSave.Add(constructToSave);

                    }

                    foreach (var element in trioToSave)
                    {
                        var existingConstruct = context.Constructs.Find(element.Id);
                        if (existingConstruct == null)
                        {
                            context.Constructs.Add(element);
                            context.SaveChanges();
                            existingConstruct = element;
                        }
                        var constructToSave = new ConstructToSave()
                        {
                            ConstructId = existingConstruct.Id,
                            SaveEntityId = saveData.Id,
                            SaveName = saveData.SaveName
                        };
                        context.ConstructsToSave.Add(constructToSave);
                    }
                    foreach (var scalation in scalationsToSave)
                    {
                        var scalationToSave = new ScalationToSave()
                        {
                            Value = scalation,
                            SaveEntityId = saveData.Id,
                        };
                        context.ScalationsToSave.Add(scalationToSave);
                    }

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }
            }

            MessageBox.Show("Успешно сохранено");
            main.txtSave.Clear();
        }
    }
}

