using System.Windows;
using System.Windows.Controls;

namespace Project.Views
{
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void AddListButton_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = (MainWindow)Application.Current.MainWindow;
            parentWindow.MainContent.Content = new AddListView(); // Меняем контент
        }

        private void KKalButton_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = (MainWindow)Application.Current.MainWindow;
            parentWindow.MainContent.Content = new KKalView(); // Меняем контент
        }

        private void QRButton_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = (MainWindow)Application.Current.MainWindow;
            parentWindow.MainContent.Content = new QRScannerView(); // Меняем контент
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = (MainWindow)Application.Current.MainWindow;
            parentWindow.MainContent.Content = new SettingsView(); // Меняем контент
        }
    }
}
