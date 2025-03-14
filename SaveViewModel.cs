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
        private readonly SaveContext _context;
        public ObservableCollection<SaveEntity> Saves { get; set; }
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
        
        public SaveViewModel()
        {
            _context = new SaveContext();
            LoadSavesAsync();
        }

        private async void LoadSavesAsync()
        {
            var saves = await _context.Saves
                    .Include(s => s.SavedComponents) // ConstructToSave
                    .Include(s => s.SavedScalations
                        .OrderBy(sc => sc.Id)) // Сортировка ScalationToSave
                    .Include(s => s.SavedEffects) // EffectToSave
                        .ThenInclude(e => e.DiceCombinations) // Загружаем DiceCombination
                    .AsNoTracking()
                    .ToListAsync();

            Saves = new ObservableCollection<SaveEntity>(saves);
            SavesView = CollectionViewSource.GetDefaultView(Saves);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}