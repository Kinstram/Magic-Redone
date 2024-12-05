using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using SQLitePCL;

namespace Magic_Redone
{
    public class SaveViewModel : INotifyPropertyChanged
    {
        private readonly SaveContext _context;
        private SaveContext context = new(); 
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
        public ObservableCollection<Construct> LoadedComponents { get; set; } = new ObservableCollection<Construct>();
        private Construct _construct1;
        public Construct Construct1
        {
            get => _construct1 ?? (_construct1 = new Construct());
            set
            {
                _construct1 = value;
                OnPropertyChanged();
            }
        }

        public SaveViewModel()
        {
            _context = context;
            LoadSavesAsync();
        }
        private async void LoadSavesAsync()
        {
            try
            {

                var saves = await _context.Saves.ToListAsync();
                Saves = new ObservableCollection<SaveEntity>(saves);
                SavesView = CollectionViewSource.GetDefaultView(Saves);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Construct1 = GetConstructById(Saves[0].SelectedComponent1Id);
            LoadedComponents.Add(Construct1);
        }
        private Construct GetConstructById(Int16? id)
        {
                using (var context = new ApplicationContext()) // Ваш DbContext для Construct
                {
                    return context.Constructs.Find(id.Value);
                }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}