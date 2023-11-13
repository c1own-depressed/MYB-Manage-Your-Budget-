using BLL;
using DAL;
using MYB_NEW;
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

namespace OtherPages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class SettingsPageView : Page
    {
        private int userId;
        public SettingsPageView()
        {

            InitializeComponent();
            InnerUser currentUser = UserManager.Instance.CurrentUser;
            userId = currentUser.UserId;
            User user = UserQueries.GetUser(userId);
            LanguageComboBox.Text = (user.Language == "ua") ? "Ukrainian" : (user.Language == "en") ? "English" : "Unknown";
            ThemeComboBox.SelectedIndex = user.LightTheme? 1 : 0;
            CurrencyComboBox.Text = user.Currency.ToUpper();

        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedLanguage = (ComboBoxItem)LanguageComboBox.SelectedItem;
            string language = selectedLanguage.Content.ToString();
            string dblanguage = (language == "Ukrainian") ? "ua" : (language == "English") ? "en" : "Unknown";
            ComboBoxItem selectedTheme = (ComboBoxItem)ThemeComboBox.SelectedItem;
            string theme = selectedTheme.Content.ToString();
            bool isLight = theme == "Light";



            ComboBoxItem selectedCurrency = (ComboBoxItem)CurrencyComboBox.SelectedItem;
            string currency = selectedCurrency.Content.ToString().ToLower();
            UserQueries.UpdateUser(userId, dblanguage, isLight, currency);
            MessageBox.Show("Success!");
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Main main = new Main();
            Window.GetWindow(this).Content = main;
        }

       
    }
}
