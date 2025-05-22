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
    public partial class KKalView : UserControl
    {
        private ProductDatabase productDatabase;
        private List<Models.Product> products;

        public KKalView()
        {
            InitializeComponent();
            productDatabase = new ProductDatabase();
            LoadProducts();
            if (ProductComboBox != null)
            {
                ProductComboBox.SelectionChanged += ProductComboBox_SelectionChanged;
            }
        }

        private void LoadProducts()
        {
            try
            {
                products = productDatabase.GetAllProducts();
                if (ProductComboBox != null)
                {
                    ProductComboBox.ItemsSource = products;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке продуктов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductComboBox?.SelectedItem is Models.Product selectedProduct)
            {
                if (TypeBox != null) TypeBox.Text = selectedProduct.Type;
                if (ProteinsBox != null) ProteinsBox.Text = selectedProduct.Protein.ToString("F1");
                if (FatsBox != null) FatsBox.Text = selectedProduct.Fats.ToString("F1");
                if (CarbsBox != null) CarbsBox.Text = selectedProduct.Carbohydrates.ToString("F1");
                if (CaloriesBox != null) CaloriesBox.Text = selectedProduct.Calories.ToString("F1");
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
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbs { get; set; }
        public double Calories { get; set; }

        public override string ToString() => Name;
    }
}
