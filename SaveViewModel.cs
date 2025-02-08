﻿using Microsoft.EntityFrameworkCore;
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
            try
            {
                var saves = await _context.Saves
                    .AsNoTracking()
                      .Include(s => s.SavedComponents)
                        .ThenInclude(cts => cts.Construct)
                     .Include(s => s.SavedTrio)
                       .ThenInclude(cts => cts.Construct)
                    .Include(s => s.SavedScalations)
                    .ToListAsync();

                Saves = new ObservableCollection<SaveEntity>(saves);
                SavesView = CollectionViewSource.GetDefaultView(Saves);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}