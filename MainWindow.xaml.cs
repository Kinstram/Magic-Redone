using System.Windows;


namespace Magic_Redone
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new Getter();

            txtSave.GotFocus += txtSave_GotFocus;
            txtSave.LostFocus += txtSave_LostFocus;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Back.SpellSave(this);
        }

        private void txtSave_GotFocus(object sender, RoutedEventArgs e)
        {
            // Если текст в TextBox равен подсказке, очищаем его
            if (txtSave.Text == "Введите название сохранения")
            {
                txtSave.Text = "";
            }
        }

        private void txtSave_LostFocus(object sender, RoutedEventArgs e)
        {
            // Если TextBox пустой, возвращаем подсказку
            if (string.IsNullOrWhiteSpace(txtSave.Text))
            {
                txtSave.Text = "Введите название сохранения";
            }
        }
    }
}