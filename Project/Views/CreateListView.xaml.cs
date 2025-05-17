using System.Windows;
using System.Windows.Controls;
using Project.Models;

namespace Project.Views
{
    public partial class CreateListView : UserControl
    {
        private readonly JsonDatabase _database;

        public CreateListView()
        {
            InitializeComponent();
            _database = new JsonDatabase();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Переключаемся на AddListView
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.ShowAddListView();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string title = ListNameTextBox.Text.Trim();
            string description = ListDescriptionTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                return;
            }

            try
            {
                await _database.AddItem(title, description);
                
                // Очищаем поля после сохранения
                ListNameTextBox.Clear();
                ListDescriptionTextBox.Clear();
                
                // Возвращаемся на предыдущий экран
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.ShowAddListView();
            }
            catch
            {
                // В случае ошибки просто возвращаемся на предыдущий экран
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.ShowAddListView();
            }
        }
    }
} 