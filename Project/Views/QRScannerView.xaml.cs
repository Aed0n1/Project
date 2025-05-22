using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Project.Models;

namespace Project.Views
{
    public partial class QRScannerView : UserControl
    {
        private readonly ProductDatabase _productDatabase;

        public QRScannerView()
        {
            InitializeComponent();
            _productDatabase = new ProductDatabase();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Логика возврата на предыдущий экран
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.BackToMainWindow();
            }
        }

        private void ProductTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Разрешаем только буквы и цифры
            foreach (char c in e.Text)
            {
                if (!char.IsLetterOrDigit(c) && c != ' ')
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void ProductNumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Разрешаем только цифры
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private async void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверяем, что все поля заполнены
                if (string.IsNullOrWhiteSpace(ProductNameTextBox.Text) ||
                    ProductTypeComboBox.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(ProductNumberTextBox.Text) ||
                    string.IsNullOrWhiteSpace(ProductProteinTextBox.Text) ||
                    string.IsNullOrWhiteSpace(ProductFatsTextBox.Text) ||
                    string.IsNullOrWhiteSpace(ProductCarbohydratesTextBox.Text) ||
                    string.IsNullOrWhiteSpace(ProductCaloriesTextBox.Text))
                {
                    MessageBox.Show("Please fill in all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Получаем значения из полей
                string name = ProductNameTextBox.Text;
                string type = ((ComboBoxItem)ProductTypeComboBox.SelectedItem).Content.ToString() ?? string.Empty;
                string barcode = ProductNumberTextBox.Text;

                // Парсим числовые значения
                if (!double.TryParse(ProductProteinTextBox.Text, out double protein) ||
                    !double.TryParse(ProductFatsTextBox.Text, out double fats) ||
                    !double.TryParse(ProductCarbohydratesTextBox.Text, out double carbohydrates) ||
                    !double.TryParse(ProductCaloriesTextBox.Text, out double calories))
                {
                    MessageBox.Show("Please enter valid numbers for nutritional values", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Добавляем продукт в базу данных
                await _productDatabase.AddProduct(name, type, barcode, protein, fats, carbohydrates, calories);

                // Очищаем поля после успешного добавления
                ClearFields();

                MessageBox.Show("Product added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        {
            ProductNameTextBox.Clear();
            ProductTypeComboBox.SelectedIndex = -1;
            ProductNumberTextBox.Clear();
            ProductProteinTextBox.Clear();
            ProductFatsTextBox.Clear();
            ProductCarbohydratesTextBox.Clear();
            ProductCaloriesTextBox.Clear();
        }

        // Показываем Popup при наведении мышки на TextBox
        private void ProductNumberTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            var popup = (Popup)ProductNumberTextBox.Template.FindName("PopupToolTip", ProductNumberTextBox);
            if (popup != null)
            {
                popup.IsOpen = true; // Показываем popup
            }
        }

        // Скрываем Popup при убирании мышки с TextBox
        private void ProductNumberTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            var popup = (Popup)ProductNumberTextBox.Template.FindName("PopupToolTip", ProductNumberTextBox);
            if (popup != null && !IsMouseOverPopup(popup)) // Проверяем, что курсор не над Popup
            {
                popup.IsOpen = false; // Скрываем popup
            }
        }

        // Проверяем, что курсор не находится над Popup
        private bool IsMouseOverPopup(Popup popup)
        {
            var popupContent = (UIElement)popup.Child;
            return popupContent.IsMouseOver || popup.IsMouseOver;
        }
    }
}