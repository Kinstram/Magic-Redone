using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Magic_Redone.Simple
{
    public class SimplePageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SimplePageVM()
        {
            TimeExist = ModCollections.TimeExistOptions.FirstOrDefault();
            TimeCast = ModCollections.TimeCastOptions.FirstOrDefault();
            Form = ModCollections.FormOptions.FirstOrDefault();
            Method = ModCollections.MethodOptions.FirstOrDefault();
            Size = 1;
        }

        public static decimal GetValue(Dictionary<string, decimal> dict, string key)
        {
            if (key != null && dict.TryGetValue(key, out decimal value))
            {
                return value;
            }
            else return 1m;
        }

        #region Свойства для привязки в WPF
        public ObservableCollection<string> TimeExistColl { get; set; } = ModCollections.TimeExistOptions;
        public ObservableCollection<string> TimeCastColl { get; set; } = ModCollections.TimeCastOptions;
        public ObservableCollection<string> FormColl { get; set; } = ModCollections.FormOptions;
        public ObservableCollection<string> MethodColl { get; set; } = ModCollections.MethodOptions;

        private int _points;
        public int Points
        {
            get => _points;
            set
            {
                _points = value;
                SimplePageModel.PointCount(this);
                OnPropertyChanged();
            }
        }

        private string _method;
        public string Method
        {
            get => _method;
            set
            {
                _method = value;
                MethodCost = GetValue(ModDicts.MethodDict, value); // TODO: Сделать так же для скаляций
                SimplePageModel.UpdateResult(this);
                OnPropertyChanged();
            }
        }

        private string _form;
        public string Form
        {
            get => _form;
            set
            {
                _form = value;
                FormCost = GetValue(ModDicts.FormDict, value);
                SimplePageModel.UpdateResult(this);
                OnPropertyChanged();
            }
        }

        private int _size;
        public int Size
        {
            get => _size;
            set
            {
                _size = value;
                SizeCost = value;
                SimplePageModel.UpdateResult(this);
                OnPropertyChanged();
            }
        }

        private string _timeExist;
        public string TimeExist
        {
            get => _timeExist;
            set
            {
                _timeExist = value;
                TimeExistCost = GetValue(ModDicts.TimeExistDict, value);
                SimplePageModel.UpdateResult(this);
                OnPropertyChanged();
            }
        }

        private string _timeCast;
        public string TimeCast
        {
            get => _timeCast;
            set
            {
                _timeCast = value;
                TimeCastCost = GetValue(ModDicts.TimeCastDict, value);
                SimplePageModel.UpdateResult(this);
                OnPropertyChanged();
            }
        }

        private decimal _result; // Промежуточный итог подсчёта (время, форма и т.д.)
        public decimal Result
        {
            get => _result;
            set
            {
                _result = value;
                SimplePageModel.PointCount(this);
                OnPropertyChanged();
                Debug.WriteLine("Result Final\t" + Result);
            }
        }

        private decimal _total; // Итог с учётом подсчёта начальных очков
        public decimal Total
        {
            get => _total;
            set
            {
                _total = value;
                OnPropertyChanged();
                Debug.WriteLine(Total);
            }
        }

        private decimal _methodCost;

        public decimal MethodCost
        {
            get => _methodCost;
            set
            {
                _methodCost = value;
                OnPropertyChanged();
                Debug.WriteLine("Method Cost " + MethodCost);
            }
        }

        private decimal _formCost;

        public decimal FormCost
        {
            get => _formCost;
            set
            {
                _formCost = value;
                OnPropertyChanged();
            }
        }

        private decimal _sizeCost;

        public decimal SizeCost
        {
            get => _sizeCost;
            set
            {
                _sizeCost = value;
                OnPropertyChanged();
            }
        }

        private decimal _timeExistCost;

        public decimal TimeExistCost
        {
            get => _timeExistCost;
            set
            {
                _timeExistCost = value;
                OnPropertyChanged();
            }
        }

        private decimal _timeCastCost;

        public decimal TimeCastCost
        {
            get => _timeCastCost;
            set
            {
                _timeCastCost = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
