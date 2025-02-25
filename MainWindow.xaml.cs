using Magic_Redone;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Magic_Redone
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new Results();

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
            Back.SaveToLists(this);
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
            Debug.WriteLine("LostFocus event triggered");
            // Если TextBox пустой, возвращаем подсказку
            if (string.IsNullOrEmpty(txtSave.Text))
            {
                txtSave.Text = "Введите название сохранения";
            }
        }
    }
}