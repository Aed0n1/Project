using System.Windows.Controls;
using System.Windows;

namespace Project.Views
{
    public partial class SettingsOfList : UserControl
    {
        public SettingsOfList()
        {
            InitializeComponent();
        }


        public void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Переключаемся на MainWindow
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.BackToMainWindow();
        }
    }
}