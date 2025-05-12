using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;


namespace Magic_Redone
{
    public partial class SavePage : Page
    {
        public event Action ReturnRequested;
        SaveViewModel saveViewModel = SaveViewModel.Instance;

        public SavePage()
        {
            InitializeComponent();
            this.DataContext = saveViewModel;
            Loaded += OnPageLoaded;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnRequested?.Invoke();
        }
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new InputDialogWindow("Вы уверены, что хотите удалить сохранения?");
            if (dialog.ShowDialog() == true)
            {
                await SaveModel.DeleteButton_Click(null, null);
                await saveViewModel.LoadSavesAsync(); //Обновление после удаления
            }
        }

        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            saveViewModel.LoadSavesAsync();
        }
    }
}
