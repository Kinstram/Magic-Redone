using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace Magic_Redone
{
    public class SaveModel
    {
        public static async Task DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var idList = IdGetter();

            await using var context = new SaveContext();
            await context.Saves
                .Where(e => idList.Contains(e.Id))
                .ExecuteDeleteAsync();

            Debug.WriteLine("Удалено");
        }

        internal static List<int> IdGetter()
        {
            List<int> idList = new();
            idList = SaveViewModel.Instance.Saves.Where(c => c.IsSelected == true).Select(c => c.Entity.Id).ToList();
            return idList;
        }
    }
}
