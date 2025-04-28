using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Project.Views
{
    public partial class QRScannerView : UserControl
    {
        public QRScannerView()
        {
            InitializeComponent();
        }

        public void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Переключаемся на MainWindow
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.BackToMainWindow();
        }
        private void ProductNumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Разрешаем только цифры, точку и запятую
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ProductTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Разрешаем только латинские буквы
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
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