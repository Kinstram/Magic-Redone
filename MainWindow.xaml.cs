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
            var Results = (Results)this.DataContext;
            var Getter = Results.Getter;
            List<Construct> constructsToSave = new List<Construct>();

            if (string.IsNullOrEmpty(txtSave.Text))
            {
                MessageBox.Show("Введите название сохранения!");
                return;
            }
            constructsToSave.Add(Getter.SelectedComponent1);
            constructsToSave.Add(Getter.SelectedComponent2);
            var saveData = new SaveEntity
            {
                Id = Guid.NewGuid().GetHashCode(),
                SaveName = txtSave.Text,
                SelectedElementId = Getter.SelectedElement.Id,
                SelectedMethodId = Getter.SelectedMethod.Id,
                SelectedFormId = Getter.SelectedForm.Id,
                SelectedComponent1Id = Getter.SelectedComponent1.Id,
                SelectedScalation1 = Getter.SelectedScalation1,
                SelectedComponent2Id = Getter.SelectedComponent2.Id,
                SelectedScalation2 = Getter.SelectedScalation2,
                SelectedComponent3Id = Getter.SelectedComponent3.Id,
                SelectedScalation3 = Getter.SelectedScalation3,
                SelectedComponent4Id = Getter.SelectedComponent4.Id,
                SelectedScalation4 = Getter.SelectedScalation4,
                SelectedComponent5Id = Getter.SelectedComponent5.Id,
                SelectedScalation5 = Getter.SelectedScalation5,
                SelectedComponent6Id = Getter.SelectedComponent6.Id,
                SelectedScalation6 = Getter.SelectedScalation6,
                Constructs = constructsToSave,

                CountedExt = Getter.CountedExt,
                CountedInt = Getter.CountedInt,
                CountedMP = Getter.CountedMP
            };

            using (var context = new SaveContext())
            {
                context.Saves.Add(saveData);
                context.SaveChanges();
            }

            MessageBox.Show("Успешно сохранено");
            txtSave.Clear(); // Очищаем поле ввода
        }
    }
}