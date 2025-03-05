using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Printing;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Effects;

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
                var componentsLoad = await context.Constructs.AsNoTracking().Where(c => c.Kind == requiredKind).Include(c => c.EffectList).OrderBy(p => p.Name).ToListAsync();
                Collections.Components = new ObservableCollection<Construct>(componentsLoad);
            }

            Collections.Scalations = [1, 2, 3];
            Collections.Time = ["1 Секунда", "15 Минут", "1 Час", "12 Часов", "1 Сутки", "1 Неделя", "1 Месяц", "6 Месяцев", "1 Год"];
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

            Modifiers.TimeDict().TryGetValue(Getter.SelectedTime, out var number);

            //итоговое суммирование произведения трио и суммы компонентов для дальнейшей передачи в Getter и WPF
            Getter.CountedExt = CountedTrioExt + CountedComponentsExt;
            Getter.CountedInt = CountedTrioInt + CountedComponentsInt;
            Getter.CountedMP = (CountedTrioMP + CountedComponentsMP) * number;

            Getter.CountedExt = Math.Round(Getter.CountedExt, 2);
            Getter.CountedInt = Math.Round(Getter.CountedInt, 2);
            Getter.CountedMP = Math.Round(Getter.CountedMP, 2);

            Getter.SelectedTimeValue = Math.Round((Getter.CountedMP - (CountedTrioMP + CountedComponentsMP)), 2);
        }

        internal static void Scalation(Getter Getter)
        {
            var compDict = Modifiers.ScalableDict();
            var effectDict = Modifiers.EffectScalation();
            for (Int16 i = 0; i < Getter.SelectedComponents.Count; i++)
            {
                if (compDict.TryGetValue((Getter.SelectedComponents[i].Name, Getter.SelectedScalations[i]), out var data))
                {
                    Getter.SelectedComponents[i].ValueExt = data.ValueExt;
                    Getter.SelectedComponents[i].ValueInt = data.ValueInt;
                    Getter.SelectedComponents[i].ValueMP = data.ValueMP;
                }
            }

            for (int i = 0; i < Getter.SelectedComponents.Count; i++)
            {
                var component = Getter.SelectedComponents[i];
                if (component == null) continue;

                // Получаем уровень скаляции ТОЛЬКО для текущего компонента
                var scalationLevel = Getter.SelectedScalations[i];

                // Применяем модификаторы строго к текущему компоненту
                foreach (var effect in component.EffectList)
                {
                    if (effectDict.TryGetValue((component.Name, scalationLevel), out var effectData))
                    {
                        // Обновление параметров с проверкой на "неактивные" значения
                        effect.Quantity = effectData.Quantity != 0 ? effectData.Quantity : effect.Quantity;

                        effect.DiceSides = effectData.DiceSides != 0 ? effectData.DiceSides : effect.DiceSides;

                        effect.EffectDesc = !string.IsNullOrWhiteSpace(effectData.EffectDesc) ? effectData.EffectDesc : effect.EffectDesc;
                    }
                }
            }

            EffectCounter(Getter);
        }
        public static void SpellSave(MainWindow main)
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


            scalationsToSave.Add((Getter.SelectedScalation1 == null || Getter.SelectedScalation1 == 0) ? (short)1 : Getter.SelectedScalation1);
            scalationsToSave.Add((Getter.SelectedScalation2 == null || Getter.SelectedScalation2 == 0) ? (short)1 : Getter.SelectedScalation2);
            scalationsToSave.Add((Getter.SelectedScalation3 == null || Getter.SelectedScalation3 == 0) ? (short)1 : Getter.SelectedScalation3);
            scalationsToSave.Add((Getter.SelectedScalation4 == null || Getter.SelectedScalation4 == 0) ? (short)1 : Getter.SelectedScalation4);
            scalationsToSave.Add((Getter.SelectedScalation5 == null || Getter.SelectedScalation5 == 0) ? (short)1 : Getter.SelectedScalation5);
            scalationsToSave.Add((Getter.SelectedScalation6 == null || Getter.SelectedScalation6 == 0) ? (short)1 : Getter.SelectedScalation6);


            trioToSave.Add(Getter.SelectedElement);
            trioToSave.Add(Getter.SelectedMethod);
            trioToSave.Add(Getter.SelectedForm);

            using (var context = new SaveContext())
            {
                try
                {
                    string saveName = main.txtSave.Text.Trim();

                    // Проверка на пустое название
                    if (string.IsNullOrWhiteSpace(saveName))
                    {
                        MessageBox.Show("Название сохранения не может быть пустым!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Проверка на уникальность
                    bool nameExists = context.Saves
                        .Any(s => s.SaveName == saveName);

                    if (nameExists)
                    {
                        MessageBox.Show("Сохранение с таким названием уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Создание новой записи, если проверки пройдены
                    var saveData = new SaveEntity
                    {
                        SaveName = saveName,
                        CountedExt = Getter.CountedExt,
                        CountedInt = Getter.CountedInt,
                        CountedMP = Getter.CountedMP,
                        TimeString = Getter.SelectedTime,
                        TimeValue = Getter.SelectedTimeValue,
                    };

                    context.Saves.Add(saveData);
                    context.SaveChanges();

                    // Сохраняем трио
                    foreach (var element in trioToSave.Where(c => c != null))
                    {
                        context.ConstructsToSave.Add(new ConstructToSave
                        {
                            OriginalId = element.Id,
                            Name = element.Name,
                            ValueExt = element.ValueExt,
                            ValueInt = element.ValueInt,
                            ValueMP = element.ValueMP,
                            SaveEntityId = saveData.Id
                        });
                    }

                    // Сохраняем компоненты
                    foreach (var component in componentsToSave.Where(c => c != null))
                    {
                        context.ConstructsToSave.Add(new ConstructToSave
                        {
                            OriginalId = component.Id,
                            Name = component.Name,
                            ValueExt = component.ValueExt,
                            ValueInt = component.ValueInt,
                            ValueMP = component.ValueMP,
                            SaveEntityId = saveData.Id
                        });
                    }

                    // Сохраняем скаляции
                    foreach (var scalation in scalationsToSave)
                    {
                        context.ScalationsToSave.Add(new ScalationToSave
                        {
                            Value = scalation,
                            SaveEntityId = saveData.Id
                        });
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
            main.txtSave.Text = "Введите название сохранения";
        }
        internal static void EffectCounter(Getter Getter)
        {
            var effects = Getter.SelectedComponents
            .Where(c => c != null)
            .SelectMany(c => c.EffectList.Select(e => new
            {
                Effect = e,
                Component = c
            }))
            // Группируем по ComponentId и Type
            .GroupBy(x => new
            {
                ComponentId = x.Component.Id,
                x.Effect.Type
            })
            // Для каждой группы ComponentId+Type создаем подгруппы по DiceSides
            .SelectMany(mainGroup => mainGroup
                .GroupBy(x => x.Effect.DiceSides)
                .Select(subGroup => new EffectResult
                {
                    Type = mainGroup.Key.Type,
                    Quantity = subGroup.Sum(e => e.Effect.Quantity), // Суммируем Quantity в подгруппе
                    DiceSides = subGroup.Key, // Берем DiceSides из ключа подгруппы
                    EffectDescs = subGroup
                        .Select(e => e.Effect.EffectDesc)
                        .Where(d => !string.IsNullOrWhiteSpace(d))
                        .Distinct()
                        .ToList()
                }))
            .Where(e => e.Type != EffectType.None)
                    .ToList();

            Getter.Effects = new ObservableCollection<EffectResult>(effects);
        }

    }
}

