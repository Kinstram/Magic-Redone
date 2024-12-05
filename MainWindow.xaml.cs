using Magic_Redone;
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
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var SavePage = new SavePage();
            this.Content = SavePage;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Back.SaveToLists(this);
        }
    }
}