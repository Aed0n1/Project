using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Project.Models;
using System.Threading.Tasks;

namespace Project.Views
{
    public partial class AddProductToList : UserControl
    {
        private readonly string _listId;
        private readonly JsonDatabase _database;
        private readonly ProductDatabase _productDatabase;
        private Models.Product? _selectedProduct;
        private List<Models.ListProduct> _selectedProducts;
        public string ListTitle { get; private set; } = string.Empty;

        public AddProductToList(string listId)
        {
            InitializeComponent();
            _listId = listId;
            _database = new JsonDatabase();
            _productDatabase = new ProductDatabase();
            _selectedProducts = new List<Models.ListProduct>();
            LoadListTitle();
            LoadProductTypes();
            LoadExistingProducts();
            UpdateNutritionSummary();
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

        private void LoadExistingProducts()
        {
            var list = _database.GetItem(_listId);
            if (list?.Products != null)
            {
                _selectedProducts = list.Products;
                UpdateSelectedProductsList();
                UpdateNutritionSummary();
            }
        }

        private void LoadProductTypes()
        {
            try
            {
                var products = _productDatabase.GetAllProducts();
                var types = products.Select(p => p.Type).Distinct().OrderBy(t => t).ToList();
                
                ProductTypeComboBox.ItemsSource = null;
                ProductTypeComboBox.ItemsSource = types;
                
                if (types.Any())
                {
                    ProductTypeComboBox.SelectedIndex = 0;
                    UpdateProductsList(types[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading product types: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateProductsList(string selectedType)
        {
            try
            {
                var products = _productDatabase.GetAllProducts()
                    .Where(p => p.Type == selectedType)
                    .OrderBy(p => p.Name)
                    .ToList();

                ProductComboBox.ItemsSource = null;
                ProductComboBox.ItemsSource = products;

                if (products.Any())
                {
                    ProductComboBox.SelectedIndex = 0;
                    _selectedProduct = products[0];
                }
                else
                {
                    _selectedProduct = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating products list: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProductTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ProductTypeComboBox.SelectedItem is string selectedType)
                {
                    UpdateProductsList(selectedType);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in product type selection: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ProductComboBox.SelectedItem is Models.Product selectedProduct)
                {
                    _selectedProduct = selectedProduct;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in product selection: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateSelectedProductsList()
        {
            SelectedProductsListView.ItemsSource = null;
            SelectedProductsListView.ItemsSource = _selectedProducts;
        }

        private void UpdateNutritionSummary()
        {
            double totalCalories = 0;
            double totalProtein = 0;
            double totalFats = 0;
            double totalCarbs = 0;

            foreach (var product in _selectedProducts)
            {
                var baseProduct = _productDatabase.GetAllProducts().FirstOrDefault(p => p.Id == product.Id);
                if (baseProduct != null)
                {
                    totalCalories += product.TotalCalories;
                    totalProtein += baseProduct.Protein * product.Quantity;
                    totalFats += baseProduct.Fats * product.Quantity;
                    totalCarbs += baseProduct.Carbohydrates * product.Quantity;
                }
            }

            TotalCaloriesText.Text = totalCalories.ToString("N1");
            TotalProteinText.Text = totalProtein.ToString("N1");
            TotalFatsText.Text = totalFats.ToString("N1");
            TotalCarbsText.Text = totalCarbs.ToString("N1");
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedProduct == null)
            {
                MessageBox.Show("Please select a product", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(QuantityTextBox.Text, out double quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var existingProduct = _selectedProducts.FirstOrDefault(p => p.Id == _selectedProduct.Id);
            if (existingProduct != null)
            {
                existingProduct.Quantity += (int)Math.Round(quantity);
            }
            else
            {
                var listProduct = new Models.ListProduct
                {
                    Id = _selectedProduct.Id,
                    Name = _selectedProduct.Name,
                    Type = _selectedProduct.Type,
                    Quantity = (int)Math.Round(quantity),
                    Calories = _selectedProduct.Calories
                };
                _selectedProducts.Add(listProduct);
            }

            UpdateSelectedProductsList();
            UpdateNutritionSummary();

            // Clear the quantity field after adding
            QuantityTextBox.Text = string.Empty;
        }

        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string id)
            {
                var product = _selectedProducts.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    if (product.Quantity > 1)
                    {
                        product.Quantity--;
                        UpdateSelectedProductsList();
                        UpdateNutritionSummary();
                    }
                    else
                    {
                        _selectedProducts.Remove(product);
                        UpdateSelectedProductsList();
                        UpdateNutritionSummary();
                    }
                }
            }
        }

        private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string id)
            {
                var product = _selectedProducts.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    product.Quantity++;
                    UpdateSelectedProductsList();
                    UpdateNutritionSummary();
                }
            }
        }

        private void RemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string id)
            {
                var product = _selectedProducts.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    _selectedProducts.Remove(product);
                    UpdateSelectedProductsList();
                    UpdateNutritionSummary();
                }
            }
        }

        private async Task SaveListAsync()
        {
            var list = _database.GetItem(_listId);
            if (list != null)
            {
                await _database.UpdateItem(_listId, list.Title, _selectedProducts);
                MessageBox.Show("List saved successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void SaveListButton_Click(object sender, RoutedEventArgs e)
        {
            await SaveListAsync();
        }

        private void ClearListButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear the list?", "Confirmation", 
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _selectedProducts.Clear();
                UpdateSelectedProductsList();
                UpdateNutritionSummary();
            }
        }

        private async void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedProducts.Any())
            {
                if (MessageBox.Show("You have unsaved changes. Do you want to save before leaving?", 
                    "Unsaved Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    await SaveListAsync();
                }
                else if (MessageBox.Show("Do you want to discard changes?", 
                    "Discard Changes", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    return;
                }
            }

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.BackToMainWindow();
        }
    }
}
