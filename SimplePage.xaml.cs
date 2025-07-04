using Magic_Redone.Simple;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Magic_Redone
{
    /// <summary>
    /// Логика взаимодействия для SimplePage.xaml
    /// </summary>
    public partial class SimplePage : Page
    {
        public event Action ReturnRequested;
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnRequested?.Invoke();
        }

        public SimplePage()
        {
            this.DataContext = new SimplePageVM();
            InitializeComponent();
        }

    }
}
