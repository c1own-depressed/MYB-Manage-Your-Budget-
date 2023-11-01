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
using MySql.Data.MySqlClient;

namespace OtherPages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            string connectionString = "server=127.0.0.1;uid=root;pwd=1234;database=mybdb";

        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedLanguage = (ComboBoxItem)LanguageComboBox.SelectedItem;
            string language = selectedLanguage.Content.ToString();


            ComboBoxItem selectedTheme = (ComboBoxItem)ThemeComboBox.SelectedItem;
            string theme = selectedTheme.Content.ToString();

            ComboBoxItem selectedCurrency = (ComboBoxItem)CurrencyComboBox.SelectedItem;
            string currency = selectedCurrency.Content.ToString();
            MessageBox.Show(language + theme + currency);
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
