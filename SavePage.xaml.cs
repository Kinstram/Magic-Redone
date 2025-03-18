using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace Magic_Redone
{
    /// <summary>
    /// Логика взаимодействия для SavePage.xaml
    /// </summary>
    public partial class SavePage : Page
    {
        public event Action ReturnRequested;
        public SavePage()
        {
            this.DataContext = new SaveViewModel();
            InitializeComponent();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Вызываем событие возврата
            ReturnRequested?.Invoke();
        }
    }
}
