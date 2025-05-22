using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Project.Models;

namespace Project.Views
{
    public partial class AddListView : UserControl
    {
        private readonly JsonDatabase _database;

        public AddListView()
        {
            InitializeComponent();
            _database = new JsonDatabase();
            LoadItems();
        }

        private void LoadItems()
        {
            var items = _database.GetAllItems();
            ItemsListBox.ItemsSource = null;
            ItemsListBox.ItemsSource = items;
        }

        public void AddList_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainContent.Content = new CreateListView();
            ButtonPanel.Visibility = Visibility.Collapsed;
        }

        public void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.BackToMainWindow();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string id)
            {
                var result = MessageBox.Show(
                    "Are you sure you want to delete this list?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _database.DeleteItem(id);
                        RefreshList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            $"Error deleting list: {ex.Message}",
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
            }
        }

        public void RefreshList()
        {
            LoadItems();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string id)
            {
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                var addProductView = new AddProductToList(id);
                mainWindow.MainContent.Content = addProductView;
                ButtonPanel.Visibility = Visibility.Collapsed;
            }
        }
    }
}