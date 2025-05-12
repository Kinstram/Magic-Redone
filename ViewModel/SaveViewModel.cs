using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;

namespace Magic_Redone
{
    public class SaveViewModel : INotifyPropertyChanged
    {
        private static SaveViewModel _instance;
        public static SaveViewModel Instance => _instance ??= new SaveViewModel();

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<SaveEntityVM> _saves;
        public ObservableCollection<SaveEntityVM> Saves
        {
            get => _saves;
            set
            {
                _saves = value;
                OnPropertyChanged();
            }
        }

        private ICollectionView _savesView;
        public ICollectionView SavesView
        {
            get => _savesView;
            set
            {
                _savesView = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadSavesAsync()
        {
            using (var context = new SaveContext())
            {
                List<SaveEntity> saves = await context.Saves
                    .Include(s => s.SavedComponents)
                    .Include(s => s.SavedScalations.OrderBy(sc => sc.Id))
                    .ToListAsync();

                Saves = new ObservableCollection<SaveEntityVM>(
                    saves.Select(s => new SaveEntityVM { Entity = s })
                );
                SavesView = CollectionViewSource.GetDefaultView(Saves);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}