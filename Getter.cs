using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Magic_Redone
{
    //приём из интерфейса
    public class Getter : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //инициализация коллекции, для записи SelectedComponents 1..6, для передачи в Back и дальнейший подсчёт итогов 
        public ObservableCollection<Construct> SelectedComponents { get; set; }

        //инициализация коллекции, для записи SelectedElement, SelectedMethod, SelectedForm, для передачи в Back и дальнейший подсчёт итогов 
        public ObservableCollection<Construct> SelectedTrio { get; set; }

        public ObservableCollection<Int16> SelectedScalations { get; set; }
       
        public Getter()
        { 
            SelectedComponents = new ObservableCollection<Construct>();
            SelectedScalations = new ObservableCollection<Int16>();
            for (int i = 0; i < 6; i++)
            {
                SelectedComponents.Add(ZeroDefaultConstruct());
                SelectedScalations.Add(1);
            }//запись пустых ячеек в коллекции

            SelectedTrio = new ObservableCollection<Construct>();
            for (int i = 0; i < 3; i++)
            {
                SelectedTrio.Add(DefaultConstruct());
            }//запись пустых ячеек в коллекции
        }
        public static Construct DefaultConstruct()
        {
            return new Construct { ValueExt = 1m, ValueInt = 1m, ValueMP = 1m };
        } //"пустой" конструкт для стихии, метода, формы
        public static Construct ZeroDefaultConstruct()
        {
            return new Construct { Name = " ", ValueExt = 0m, ValueInt = 0m, ValueMP = 0m };
        } //"пустой" конструкт для компонентов

        //магическая хуета, благодаря которой работает скаляция. Иначе при выборе двух компонентов с одним именем приводит к одинаковым данных в обоих.
        //по идее создаёт копию данных. Зачем? Видимо чтобы они не переписывали друг-друга. Как это происходит? Я бы хотел знать.
        private Construct CreateConstructCopy(Construct original)
        {
            if (original == null) return ZeroDefaultConstruct(); // Обработка null
            return new Construct
            {
                Id = original.Id,
                Kind = original.Kind,
                Name = original.Name,
                ValueExt = original.ValueExt,
                ValueInt = original.ValueInt,
                ValueMP = original.ValueMP
            };
        }

        #region Инициализация свойств для привязки
        private Construct _selectedElement;
        public Construct SelectedElement
        {
            get => _selectedElement ?? DefaultConstruct();
            set
            {
                _selectedElement = value;
                SelectedTrio[0] = SelectedElement;
                Back.ResultCount(this);
                OnPropertyChanged();
            }
        } //выбранная стихия

        private Construct _selectedMethod;
        public Construct SelectedMethod
        {
            get => _selectedMethod ?? DefaultConstruct();
            set
            {
                _selectedMethod = value;
                SelectedTrio[1] = SelectedMethod;
                Back.ResultCount(this);
                OnPropertyChanged();
            }
        } //выбранный метод

        private Construct _selectedForm;
        public Construct SelectedForm

        {
            get => _selectedForm ?? DefaultConstruct();
            set
            {
                _selectedForm = value;
                SelectedTrio[2] = SelectedForm;
                Back.ResultCount(this);
                OnPropertyChanged();
            }
        } //выбранная форма

        private Construct _selectedComponent1;
        public Construct SelectedComponent1
        {
            get => _selectedComponent1 ?? ZeroDefaultConstruct();
            set
            {
                _selectedComponent1 = CreateConstructCopy(value);
                SelectedComponents[0] = SelectedComponent1; //передача в коллекцию, дальнейший подсчёт в Back
                Back.ResultCount(this);
                OnPropertyChanged();
            }
        }

        private Construct _selectedComponent2;
        public Construct SelectedComponent2
        {
            get => _selectedComponent2 ?? ZeroDefaultConstruct();
            set
            {
                _selectedComponent2 = CreateConstructCopy(value);
                SelectedComponents[1] = SelectedComponent2;
                Back.ResultCount(this);
                OnPropertyChanged();
            }
        }

        private Construct _selectedComponent3;
        public Construct SelectedComponent3
        {
            get => _selectedComponent3 ?? ZeroDefaultConstruct();
            set
            {
                _selectedComponent3 = CreateConstructCopy(value);
                SelectedComponents[2] = SelectedComponent3;
                Back.ResultCount(this);
                OnPropertyChanged();
            }
        }

        private Construct _selectedComponent4;
        public Construct SelectedComponent4
        {
            get => _selectedComponent4 ?? ZeroDefaultConstruct();
            set
            {
                _selectedComponent4 = CreateConstructCopy(value);
                SelectedComponents[3] = SelectedComponent4;
                Back.ResultCount(this);
                OnPropertyChanged();
            }
        }

        private Construct _selectedComponent5;
        public Construct SelectedComponent5
        {
            get => _selectedComponent5 ?? ZeroDefaultConstruct();
            set
            {
                _selectedComponent5 = CreateConstructCopy(value);
                SelectedComponents[4] = SelectedComponent5;
                Back.ResultCount(this);
                OnPropertyChanged();
            }
        }

        private Construct _selectedComponent6;
        public Construct SelectedComponent6
        {
            get => _selectedComponent6 ?? ZeroDefaultConstruct();
            set
            {
                _selectedComponent6 = CreateConstructCopy(value);
                SelectedComponents[5] = SelectedComponent6;
                Back.ResultCount(this);
                OnPropertyChanged();
            }
        }

        private Int16 _selectedScalation1;
        public Int16 SelectedScalation1
        {
            get => _selectedScalation1;
            set
            {
                _selectedScalation1 = value;
                SelectedScalations[0] = value;
                Back.ResultCount(this);
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedComponents));
            }
        }

        private Int16 _selectedScalation2;
        public Int16 SelectedScalation2
        {
            get => _selectedScalation2;
            set
            {
                _selectedScalation2 = value;
                SelectedScalations[1] = value;
                Back.ResultCount(this);
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedComponents));
            }
        }

        private Int16 _selectedScalation3;
        public Int16 SelectedScalation3
        {
            get => _selectedScalation3;
            set
            {
                _selectedScalation3 = value;
                SelectedScalations[2] = SelectedScalation3;
                Back.ResultCount(this);
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedComponents));
            }
        }

        private Int16 _selectedScalation4;
        public Int16 SelectedScalation4
        {
            get => _selectedScalation4;
            set
            {
                _selectedScalation4 = value;
                SelectedScalations[3] = SelectedScalation4;
                Back.ResultCount(this);
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedComponents));
            }
        }

        private Int16 _selectedScalation5;
        public Int16 SelectedScalation5
        {
            get => _selectedScalation5;
            set
            {
                _selectedScalation5 = value;
                SelectedScalations[4] = SelectedScalation5;
                Back.ResultCount(this);
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedComponents));
            }
        }

        private Int16 _selectedScalation6;
        public Int16 SelectedScalation6
        {
            get => _selectedScalation6;
            set
            {
                _selectedScalation6 = value;
                SelectedScalations[5] = SelectedScalation6;
                Back.ResultCount(this);
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedComponents));
            }
        }


        private decimal _countedExt;
        public decimal CountedExt
        {
            get => _countedExt;
            set
            {
                _countedExt = value;
                OnPropertyChanged();
            }
        }

        private decimal _countedInt;
        public decimal CountedInt
        {
            get => _countedInt;
            set
            {
                _countedInt = value;
                OnPropertyChanged();
            }
        }

        private decimal _countedMP;
        public decimal CountedMP
        {
            get => _countedMP;
            set
            {
                _countedMP = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}

