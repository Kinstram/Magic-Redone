using System.Windows;
using System.Windows.Input;


namespace Magic_Redone
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new Getter();

            txtSave.GotFocus += TxtSave_GotFocus;
            txtSave.LostFocus += TxtSave_LostFocus;
        }

        private void BtnSavePage_Click(object sender, RoutedEventArgs e)
        {
            SavePage savePage = new SavePage();
            
            // Подписываемся на событие возврата
            savePage.ReturnRequested += () =>
            {
                MainFrame.Visibility = Visibility.Collapsed;
                MainContent.Visibility = Visibility.Visible;
            };

            // Показываем Frame и скрываем основной контент
            MainFrame.Visibility = Visibility.Visible;
            MainContent.Visibility = Visibility.Collapsed;

            // Переходим на страницу
            MainFrame.Navigate(savePage);
        }
        private void BtnSimplePage_Click(object sender, RoutedEventArgs e)
        {
            SimplePage simplePage = new SimplePage();

            // Подписываемся на событие возврата
            simplePage.ReturnRequested += () =>
            {
                MainFrame.Visibility = Visibility.Collapsed;
                MainContent.Visibility = Visibility.Visible;
            };

            // Показываем Frame и скрываем основной контент
            MainFrame.Visibility = Visibility.Visible;
            MainContent.Visibility = Visibility.Collapsed;

            // Переходим на страницу
            MainFrame.Navigate(simplePage);
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            Back.Reset(this);
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Back.SpellSave(this);
        }

        private void TxtSave_GotFocus(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox равен подсказке, очищаем его
            if (txtSave.Text == "Введите название сохранения")
            {
                txtSave.Text = "";
            }
        }

        private void TxtSave_LostFocus(object sender, RoutedEventArgs e)
        {
            // Если TextBox пустой, возвращаем подсказку
            if (string.IsNullOrWhiteSpace(txtSave.Text))
            {
                txtSave.Text = "Введите название сохранения";
            }
        }

    }
}