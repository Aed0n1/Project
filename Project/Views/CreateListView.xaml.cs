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
                MessageBox.Show("Please enter a list name", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                await _database.AddItem(title, description);
                
                // Очищаем поля после сохранения
                ListNameTextBox.Clear();
                ListDescriptionTextBox.Clear();
                
                // Возвращаемся на предыдущий экран и обновляем список
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.ShowAddListView();
                
                // Обновляем список в AddListView
                if (mainWindow.MainContent.Content is AddListView addListView)
                {
                    addListView.RefreshList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving list: {ex.Message}", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
} 