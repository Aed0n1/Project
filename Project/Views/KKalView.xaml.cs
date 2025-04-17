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
    public partial class KKalView : UserControl
    {
        private List<Product> products;

        public KKalView()
        {
            InitializeComponent();
            LoadProducts();
            ProductComboBox.ItemsSource = products;
        }

        private void LoadProducts()
        {
            products = new List<Product>
            {
                new Product { Name = "Яблоко", Proteins = 0.4, Fats = 0.4, Carbs = 9.8, Calories = 47 },
                new Product { Name = "Куриная грудка", Proteins = 23.0, Fats = 1.9, Carbs = 0.0, Calories = 113 },
                new Product { Name = "Молоко", Proteins = 3.3, Fats = 3.6, Carbs = 4.8, Calories = 64 },
                new Product { Name = "Огурец", Proteins = 0.8, Fats = 0.1, Carbs = 2.5, Calories = 15 },
                new Product { Name = "Рис варёный", Proteins = 2.2, Fats = 0.5, Carbs = 24.9, Calories = 116 }
            };
        }

        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductComboBox.SelectedItem is Product selectedProduct)
            {
                ProteinsBox.Text = selectedProduct.Proteins.ToString("0.0");
                FatsBox.Text = selectedProduct.Fats.ToString("0.0");
                CarbsBox.Text = selectedProduct.Carbs.ToString("0.0");
                CaloriesBox.Text = selectedProduct.Calories.ToString("0");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.BackToMainWindow();
            }
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbs { get; set; }
        public double Calories { get; set; }

        public override string ToString() => Name;
    }
}
