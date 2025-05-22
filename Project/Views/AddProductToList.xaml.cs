using System;
using System.Windows;
using System.Windows.Controls;
using Project.Models;

namespace Project.Views
{
    public partial class AddProductToList : UserControl
    {
        private readonly string _listId;
        private readonly JsonDatabase _database;
        public string ListTitle { get; private set; }

        public AddProductToList(string listId)
        {
            InitializeComponent();
            _listId = listId;
            _database = new JsonDatabase();
            LoadListTitle();
        }

        private void LoadListTitle()
        {
            var list = _database.GetItem(_listId);
            if (list != null)
            {
                ListTitle = list.Title;
                ListTitleTextBlock.Text = ListTitle;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.BackToMainWindow();
        }
    }
}
