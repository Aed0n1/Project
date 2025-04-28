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

namespace Project.Views
{
    public partial class AddListView : UserControl
    {
        public AddListView()
        {
            InitializeComponent();
        }

        public void AddList_Click(object sender, RoutedEventArgs e)
        {
            // При нажатии на кнопку "To Add a List" показываем AddListView
           
            ButtonPanel.Visibility = Visibility.Collapsed; // Скрыть кнопки
        }

        public void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Переключаемся на MainWindow
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.BackToMainWindow();
        }
    }
}