using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows;

namespace Magic_Redone
{
    public class InputDialogWindow : Window
    {
        public string Answer { get; private set; }

        public InputDialogWindow(string question)
        {
            Width = 300;
            Height = 150;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Title = "Удаление сохранения";

            var stackPanel = new StackPanel { Margin = new Thickness(10) };

            var textBlock = new TextBlock { Text = question, Margin = new Thickness(0, 0, 0, 10) };
            var textBox = new TextBox { Height = 23 };
            var button = new Button { Content = "OK", Width = 70, HorizontalAlignment = HorizontalAlignment.Right };

            button.Click += (_, __) =>
            {
                Answer = textBox.Text;
                DialogResult = true;
                Close();
            };

            stackPanel.Children.Add(textBlock);
            stackPanel.Children.Add(textBox);
            stackPanel.Children.Add(button);

            Content = stackPanel;
        }
    }
}
