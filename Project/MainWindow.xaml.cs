using Project.Views;
using System.Windows;

namespace Project
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Устанавливаем начальное содержимое
            //MainContent.Content = new MainWindow();
        }

        public void AddListButton_Click(object sender, RoutedEventArgs e)
        {
            // При нажатии на кнопку "To Add a List" показываем AddListView
            MainContent.Content = new AddListView();
            ButtonPanel.Visibility = Visibility.Collapsed; // Скрыть кнопки
        }

        private void KKalButton_Click(object sender, RoutedEventArgs e)
        {
            // При нажатии на кнопку "KKal" показываем KKalView
            MainContent.Content = new KKalView();
            ButtonPanel.Visibility = Visibility.Collapsed; // Скрыть кнопки
        }

        private void QRButton_Click(object sender, RoutedEventArgs e)
        {
            // При нажатии на кнопку "QR-Code" показываем QRScannerView
            MainContent.Content = new QRScannerView();
            ButtonPanel.Visibility = Visibility.Collapsed; // Скрыть кнопки
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // При нажатии на кнопку "Settings" показываем SettingsView
            MainContent.Content = new SettingsView();
            ButtonPanel.Visibility = Visibility.Collapsed; // Скрыть кнопки
        }

        // Метод для возврата на главную страницу
        public void BackToMainWindow()
        {
            // Возвращаем кнопки обратно
            ButtonPanel.Visibility = Visibility.Visible;
            MainContent.Content = new MainWindow();
        }
    }
}
