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
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
        }
        public void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Переключаемся на MainWindow
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.BackToMainWindow();
        }
        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LanguageComboBox.SelectedItem is ComboBoxItem selectedItem && selectedItem.Tag is string langCode)
            {
                ChangeLanguage(langCode);
            }
        }

        private void ChangeLanguage(string languageCode)
        {
            var dict = new ResourceDictionary();
            switch (languageCode)
            {
                case "pl":
                    dict.Source = new System.Uri("/Resources/StringResources.pl.xaml", System.UriKind.Relative);
                    break;
                case "en":
                default:
                    dict.Source = new System.Uri("/Resources/StringResources.en.xaml", System.UriKind.Relative);
                    break;
            }

            // Удаляем старые словари и добавляем новый
            var oldDict = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("StringResources"));

            if (oldDict != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(oldDict);
            }

            Application.Current.Resources.MergedDictionaries.Add(dict);
        }


        private bool isDarkTheme = false;

        private void SwitchTheme_Click(object sender, RoutedEventArgs e)
        {
            string themePath = isDarkTheme ?
                "Resources/LightTheme.xaml" :
                "Resources/DarkTheme.xaml";

            var newTheme = new ResourceDictionary
            {
                Source = new Uri(themePath, UriKind.Relative)
            };

            // Убираем старую тему
            var existingTheme = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null &&
                    (d.Source.OriginalString.Contains("LightTheme.xaml") || d.Source.OriginalString.Contains("DarkTheme.xaml")));

            if (existingTheme != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(existingTheme);
            }

            // Подключаем новую тему
            Application.Current.Resources.MergedDictionaries.Add(newTheme);

            isDarkTheme = !isDarkTheme;
        }
    }
}
