using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;


namespace Magic_Redone
{
    public class Back
    {
        internal static async void LoadElements(Collections collections) // Загрузка и распределение данных из бд в коллекции для возможности их выбора в интерфейсе и передачи на обработку в Getter
        {
            using (ApplicationContext context = new())
            {
                Int16 requiredKind = 0; //стихии
                List<Construct> elementsLoad = await context.Constructs.AsNoTracking().OrderBy(p => p.Name).Where(c => c.Kind == requiredKind).ToListAsync();
                collections.Elements = new ObservableCollection<Construct>(elementsLoad);

                requiredKind = 1; //способы
                List<Construct> methodsLoad = await context.Constructs.AsNoTracking().OrderBy(p => p.Name).Where(c => c.Kind == requiredKind).ToListAsync();
                collections.Methods = new ObservableCollection<Construct>(methodsLoad);

                requiredKind = 2; //формы
                List<Construct> formsLoad = await context.Constructs.AsNoTracking().OrderBy(p => p.Name).Where(c => c.Kind == requiredKind).ToListAsync();
                collections.Forms = new ObservableCollection<Construct>(formsLoad);

                requiredKind = 3; //компоненты
                List<Construct> componentsLoad = await context.Constructs.AsNoTracking().Where(c => c.Kind == requiredKind).OrderBy(p => p.Name).Include(x => x.TiedEffect).ToListAsync();
                collections.Components = new ObservableCollection<Construct>(componentsLoad);

                List<Effect> effectsLoad = await context.EffectList.AsNoTracking().OrderBy(p => p.ConstructId).ToListAsync();
            }

            collections.Time = ["1 Секунда", "15 Минут", "1 Час", "12 Часов", "1 Сутки", "1 Неделя", "1 Месяц", "6 Месяцев", "1 Год"];
        }

        public static void ResultCount(Getter getter) // Подсчёт Ext, Int, MP и т.п. выбранных в интерфейсе компонентов
        {
            Scalation(getter); // Проверка на скаляции
            EffectGrouper(getter); // Вывод эффектов

            // Инициализация и обнуление переменных для подсчёта Element, Method и Form. Сделано отдельно, чтобы не множить на 0 при пустых компонентах
            decimal CountedTrioExt = 1m;
            decimal CountedTrioInt = 1m;
            decimal CountedTrioMP = 1m;

            // Умножение записей коллекции трио друг на друга
            foreach (Construct el in getter.SelectedTrio.Where(e => e != null))
            {
                CountedTrioExt *= el.ValueExt;
                CountedTrioInt *= el.ValueInt;
                CountedTrioMP *= el.ValueMP;
            }

            // Инициализация и обнуление переменных для подсчёта SelectedComponents 1..6
            decimal CountedComponentsExt = 0m;
            decimal CountedComponentsInt = 0m;
            decimal CountedComponentsMP = 0m;

            // Суммирование записей коллекции с компонентам
            foreach (Construct comp in getter.SelectedComponents.Where(e => e != null))
            {
                CountedComponentsExt += comp.ValueExt;
                CountedComponentsInt += comp.ValueInt;
                CountedComponentsMP += comp.ValueMP;
            }

            Modifiers.TimeDict().TryGetValue(getter.SelectedTime, out decimal timeMod);

            //итоговое суммирование произведения трио и суммы компонентов для дальнейшей передачи в Getter и WPF
            getter.CountedExt = CountedTrioExt + CountedComponentsExt;
            getter.CountedInt = CountedTrioInt + CountedComponentsInt;
            getter.CountedMP = (CountedTrioMP + CountedComponentsMP + AreaMPCount(getter)) * timeMod;

            getter.CountedExt = Math.Round(getter.CountedExt, 2);
            getter.CountedInt = Math.Round(getter.CountedInt, 2);
            getter.CountedMP = Math.Round(getter.CountedMP, 2);

            getter.SelectedTimeValue = Math.Round((getter.CountedMP - (CountedTrioMP + CountedComponentsMP + AreaMPCount(getter))), 2); // Подсчёт стоимости модификатора времени
        }

        internal static void Scalation(Getter getter) // Скаляция и время
        {
            var compDict = Modifiers.ScalableDict();
            var effectDict = Modifiers.EffectScalation();
            List<int> validBoundings = new();
            List<int> boundingsScalations = new();
            List<int> scalationsDouble = getter.SelectedScalations.ToList();
            int scalationModifier = 0;

            // Проверка и перезапись данных в SelectedComponents с учётом скаляции
            for (Int16 i = 0; i < getter.SelectedComponents.Count; i++)
            {
                Construct component = getter.SelectedComponents[i];
                var array = getter.SelectedComponents;

                if (component.Name == "Связь")
                {
                    bool isBetween = false;

                    if ((i != 0 && i != 5) && array[i - 1].Name == array[i + 1].Name)
                    {
                        isBetween = true;
                    }

                    if (isBetween)
                    {
                        validBoundings.Add(i);
                        boundingsScalations.Add(i - 1);
                        boundingsScalations.Add(i + 1);
                    }

                    if (validBoundings.Count > 1 && (validBoundings[0] == (validBoundings[1] - 2)))
                    {
                        scalationModifier = 2;
                    }
                    else scalationModifier = 1;
                }
            }

            boundingsScalations = boundingsScalations.Distinct().ToList();
            foreach (int n in boundingsScalations)
            {
                scalationsDouble[n] += scalationModifier;
            }

            for (int i = 0; i < getter.SelectedComponents.Count; i++)
            {
                Construct component = getter.SelectedComponents[i];
                int scalationLevel = scalationsDouble[i];
                Effect effect = component.TiedEffect;

                if (compDict.TryGetValue((component.Name, scalationLevel), out Construct data))
                {
                    component.ValueExt = data.ValueExt;
                    component.ValueInt = data.ValueInt;
                    component.ValueMP = data.ValueMP;
                }

                if (effectDict.TryGetValue((component.Name, scalationLevel), out Effect effectData))
                {
                    // Создаем новый объект эффекта вместо существующего во избежание перезаписи
                    component.TiedEffect = new Effect
                    {
                        Type = effect.Type,
                        Quantity = effectData.Quantity != 0 ? effectData.Quantity : effect.Quantity,
                        DiceSides = effectData.DiceSides != 0 ? effectData.DiceSides : effect.DiceSides,
                        EffectDesc = !string.IsNullOrWhiteSpace(effectData.EffectDesc) ? effectData.EffectDesc : effect.EffectDesc,
                        ConstructId = effect.ConstructId
                    };
                }
            }

            getter.OnPropertyChanged(nameof(getter.SelectedComponents));
        }

        public static void SpellSave(MainWindow main)
        {
            Getter Getter = (Getter)main.DataContext;

            List<Construct> componentsToSave = new List<Construct>();
            List<Construct> trioToSave = new List<Construct>();
            List<Int16> scalationsToSave = new List<Int16>();

            // Запись каждого выбранного компонента
            componentsToSave.Add(Getter.SelectedComponent1);
            componentsToSave.Add(Getter.SelectedComponent2);
            componentsToSave.Add(Getter.SelectedComponent3);
            componentsToSave.Add(Getter.SelectedComponent4);
            componentsToSave.Add(Getter.SelectedComponent5);
            componentsToSave.Add(Getter.SelectedComponent6);

            // Запись выбранных скаляций
            scalationsToSave.Add(Getter.SelectedScalation1);
            scalationsToSave.Add(Getter.SelectedScalation2);
            scalationsToSave.Add(Getter.SelectedScalation3);
            scalationsToSave.Add(Getter.SelectedScalation4);
            scalationsToSave.Add(Getter.SelectedScalation5);
            scalationsToSave.Add(Getter.SelectedScalation6);

            trioToSave.Add(Getter.SelectedElement);
            trioToSave.Add(Getter.SelectedMethod);
            trioToSave.Add(Getter.SelectedForm);

            using (SaveContext context = new())
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

                    // Проверка названия сохранения на уникальность
                    bool nameExists = context.Saves.Any(s => s.SaveName == saveName);

                    if (nameExists)
                    {
                        MessageBox.Show("Сохранение с таким названием уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Создание новой записи, если проверки пройдены
                    SaveEntity saveData = new SaveEntity
                    {
                        SaveName = saveName,
                        CountedExt = Getter.CountedExt,
                        CountedInt = Getter.CountedInt,
                        CountedMP = Getter.CountedMP,
                        TimeString = Getter.SelectedTime,
                        TimeValue = Getter.SelectedTimeValue,
                        SavedEffect = Getter.EffectLine
                    };

                    context.Saves.Add(saveData);
                    context.SaveChanges();

                    // Сохраняем трио
                    foreach (Construct element in trioToSave.Where(c => c != null))
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
                    foreach (Construct component in componentsToSave.Where(c => c != null))
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
                    foreach (Int16 scalation in scalationsToSave)
                    {
                        context.ScalationsToSave.Add(new ScalationToSave
                        {
                            Value = scalation,
                            SaveEntityId = saveData.Id
                        });
                    }
                    // Запись в бд UserSaves
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
        } // Запись данных и их сохранение в SaveContext
        internal static void EffectGrouper(Getter getter)
        {
            // Получение выбранных эффектов
            List<EffectResult> effects = getter.SelectedComponents
            .Where(c => c != null && c.TiedEffect != null)
            .Select(c => c.TiedEffect)
            .GroupBy(e => e.Type)
            .Select(g => new EffectResult
            {
                Type = g.Key,
                DiceCombinations = g.Select(e => (e.Quantity, e.DiceSides)).ToList(),
                EffectDescs = g.Select(e => e.EffectDesc)
                              .Where(d => !string.IsNullOrWhiteSpace(d))
                              .ToList()
            })
            .OrderBy(e => e.Type)
            .ToList();

            // Запись всех эффектов в одну строку для дальнейшей передачи в FormatEffectLine
            string effectToString = string.Join("  \n", effects.Where(e => e != null).Select(e => e.ToString())).Trim();
            effectToString += "\nРазмер: " + Back.AreaCount(getter).ToString();
            getter.EffectLine = FormatEffectLine(effectToString);
        } // Группировка и запись эффектов в EffectLine для дальнейшей обработки и вывода в WPF
        internal static string FormatEffectLine(string formatingString)
        {

            // Разделяем строку на эффект и описание
            int index = formatingString.IndexOf("  \n");
            if (index == -1) return formatingString;

            string effectLine = formatingString.Substring(0, index);
            string description = formatingString.Substring(index);

            // Ищем кубы ТОЛЬКО в части с эффектом
            MatchCollection diceMatches = Regex.Matches(effectLine, @"\d+d\d+");
            if (diceMatches.Count == 0) return formatingString;
            Match damageTypeMatch = Regex.Match(effectLine, @"\b(cut|burn|cr|ex)\b");

            if (damageTypeMatch.Success)
            {
                // Извлекаем модификаторы
                string damageType = damageTypeMatch.Success ? damageTypeMatch.Value : "cr"; // cr по умолчанию
                bool hasExpl = description.Contains("ex");
                bool hasPen = Regex.IsMatch(description, @"\(\d\)");

                // Обрабатываем кубы
                List<string> mainDice = new();
                List<string> explDice = new();

                foreach (Match match in diceMatches)
                {
                    string[] parts = match.Value.Split('d');
                    int qty = int.Parse(parts[0]);
                    int dice = int.Parse(parts[1]);

                    mainDice.Add(match.Value);

                    if (hasExpl)
                    {
                        int explQty = qty / 2 > 0 ? qty / 2 : 1;
                        explDice.Add($"{explQty}d{dice}");
                    }
                }

                // Собираем результат
                StringBuilder result = new StringBuilder(string.Join(" + ", mainDice));

                if (hasPen)
                {
                    string pen = Regex.Match(description, @"\(\d\)").Value;
                    result.Append($" {pen}");
                    description = Regex.Replace(description, @"\(\d\)", "");
                }
                result.Append($" {(hasExpl ? "ex" : damageType)}");
                if (hasExpl)
                {
                    result.Append($" [{string.Join(" + ", explDice)}]");
                }

                result.Append(" урона");
                result.Append($"\n{description}");
                return result.ToString();
            }
            else
            {
                return formatingString;
            }
        }
        internal static void Reset(MainWindow main)
        {
            Getter Getter = (Getter)main.DataContext;

            Getter.SelectedElement = Getter.Collections.Elements[0];
            Getter.SelectedForm = Getter.Collections.Forms[0];
            Getter.SelectedMethod = Getter.Collections.Methods[0];

            Getter.SelectedComponent1 = Getter.Collections.Components[0];
            Getter.SelectedComponent2 = Getter.Collections.Components[0];
            Getter.SelectedComponent3 = Getter.Collections.Components[0];
            Getter.SelectedComponent4 = Getter.Collections.Components[0];
            Getter.SelectedComponent5 = Getter.Collections.Components[0];
            Getter.SelectedComponent6 = Getter.Collections.Components[0];

            main.ComponentsListBox1.Text = "";
            main.ComponentsListBox2.Text = "";
            main.ComponentsListBox3.Text = "";
            main.ComponentsListBox4.Text = "";
            main.ComponentsListBox5.Text = "";
            main.ComponentsListBox6.Text = "";

            Getter.SelectedScalation1 = 0;
            Getter.SelectedScalation2 = 0;
            Getter.SelectedScalation3 = 0;
            Getter.SelectedScalation4 = 0;
            Getter.SelectedScalation5 = 0;
            Getter.SelectedScalation6 = 0;

            Getter.AreaCostForm = 0;
            Getter.AreaCostMethod = 0;
            Getter.AreaCostGeneral = 0;
            Getter.AreaCostComponent1 = 0;
            Getter.AreaCostComponent2 = 0;
            Getter.AreaCostComponent3 = 0;
            Getter.AreaCostComponent4 = 0;
            Getter.AreaCostComponent5 = 0;
            Getter.AreaCostComponent6 = 0;

            Getter.SelectedTime = Getter.Collections.Time[0];
            Debug.WriteLine(Getter.SelectedTime);
        }
        internal static int AreaMPCount(Getter getter)
        {
            int countMP = 0;

            foreach (int e in getter.selectedAreaMods)
            {
                countMP += e;
            }

            return countMP;
        }
        internal static int AreaCount(Getter getter)
        {
            /*int rainChk = AreaMPCount(getter) - getter.AreaCostGeneric; //ha-ha
            int result = 0;

            rainChk *= 2;

            return rainChk;*/
            int finalCount = 0;
            finalCount = getter.AreaCostMethod > 0 ? getter.AreaCostMethod : (getter.AreaCostForm > 0 ? getter.AreaCostForm : 0);
            finalCount *= 2;
            finalCount += getter.AreaCostGeneral;
            Debug.WriteLine(finalCount);
            return finalCount;
        }
    }
}