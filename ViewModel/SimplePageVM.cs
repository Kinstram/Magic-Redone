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
                SimplePageModel.UpdateResult(this);
                OnPropertyChanged();
            }
        }

        private decimal _result;
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

        private decimal _total;
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
        #endregion
    }
}
