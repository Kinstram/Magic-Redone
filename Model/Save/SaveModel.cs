using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Magic_Redone
{
    public class SaveModel
    {
        public async void Deletion(Window owner)
        {
            var inputDialog = new InputDialogWindow("Введите имя сохранения для удаления:");
            inputDialog.Owner = owner; // Устанавливаем владельца
            if (inputDialog.ShowDialog() == true)
            {
                string nameToDelete = inputDialog.Answer;

                try
                {
                    using (var context = new SaveContext())
                    {
                        bool exists = await context.Saves
                            .AnyAsync(s => s.SaveName == nameToDelete);

                        if (!exists)
                        {
                            MessageBox.Show($"Сохранение с именем '{nameToDelete}' не найдено.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        var entityToDelete = await context.Saves
                            .FirstOrDefaultAsync(s => s.SaveName == nameToDelete);

                        if (entityToDelete != null)
                        {
                            context.Saves.Remove(entityToDelete);
                            await context.SaveChangesAsync();

                            // Обновляем данные в ViewModel
                            SaveViewModel.Instance.LoadSavesAsync();

                            MessageBox.Show($"Сохранение '{nameToDelete}' успешно удалено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
