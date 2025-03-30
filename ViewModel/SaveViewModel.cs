using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows;

namespace Magic_Redone
{
    public class SaveViewModel : INotifyPropertyChanged
    {
        private static SaveViewModel _instance;
        public static SaveViewModel Instance => _instance ??= new SaveViewModel();

        private ObservableCollection<SaveEntity> _saves;
        public ObservableCollection<SaveEntity> Saves
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

        private SaveViewModel()
        {
            LoadSavesAsync();
        }

        public async void LoadSavesAsync()
        {
            using (var context = new SaveContext())
            {
                List<SaveEntity> saves = await context.Saves
                    .Include(s => s.SavedComponents)
                    .Include(s => s.SavedScalations.OrderBy(sc => sc.Id))
                    .AsNoTracking()
                    .ToListAsync();

                Saves = new ObservableCollection<SaveEntity>(saves);
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