using System.Windows;

namespace Project
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddListButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Views.AddListView();  // Загружаем AddListView в ContentControl
        }

        private void KKalButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Views.KKalView();  // Загружаем KKalView в ContentControl
        }

        private void QRScannerButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Views.QRScannerView();  // Загружаем QRScannerView в ContentControl
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Views.SettingsView();  // Загружаем SettingsView в ContentControl
        }
    }
}

