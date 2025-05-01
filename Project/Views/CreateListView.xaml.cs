using System.Windows;
using System.Windows.Controls;

namespace Project.Views
{
    public partial class CreateListView : UserControl
    {
        public CreateListView()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Переключаемся на AddListView
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.ShowAddListView();
        }
    }
} 