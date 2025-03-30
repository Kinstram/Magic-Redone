﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;


namespace Magic_Redone
{
    public partial class SavePage : Page
    {
        public event Action ReturnRequested;
        SaveModel saveModel = new();
        SaveViewModel saveViewModel = SaveViewModel.Instance; // Используем Singleton

        public SavePage()
        {
            this.DataContext = saveViewModel;
            InitializeComponent();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Вызываем событие возврата
            ReturnRequested?.Invoke();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var confirmResult = MessageBox.Show(
                "Вы уверены что хотите удалить сохранение?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirmResult == MessageBoxResult.Yes)
            {
                saveModel.Deletion(); // Больше не передаем ViewModel
            }
        }
    }
}
