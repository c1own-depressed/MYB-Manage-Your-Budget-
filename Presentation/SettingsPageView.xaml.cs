namespace OtherPages
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;
    using BLL;
    using DAL;
    using MYB_NEW;
    using static System.Net.Mime.MediaTypeNames;

    /// <summary>
    /// Interaction logic for Page1.xaml.
    /// </summary>
    public partial class SettingsPageView : Page
    {
        private int userId;
        private string userLangage;

        public SettingsPageView()
        {
            if (UserManager.CurrentUser.Language == "ua")
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("uk-UA");
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            }
            this.userLangage = UserManager.CurrentUser.Language;
            this.InitializeComponent();
            this.userId = UserManager.CurrentUser.Id;
            User user = SettingsLogic.GetUser(this.userId);
            this.LanguageComboBox.Text = (user.Language == "ua") ? "Ukrainian" : (user.Language == "en") ? "English" : "Unknown";
            this.ThemeComboBox.SelectedIndex = user.LightTheme? 1 : 0;
            this.CurrencyComboBox.Text = user.Currency.ToUpper();

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedLanguage = (ComboBoxItem)this.LanguageComboBox.SelectedItem;
            string language = selectedLanguage?.Content?.ToString() ?? "Unknown";
            string dblanguage = (language == "Ukrainian") ? "ua" : (language == "English") ? "en" : "Unknown";
            ComboBoxItem selectedTheme = (ComboBoxItem)this.ThemeComboBox.SelectedItem;
            string theme = selectedTheme?.Content?.ToString() ?? "Unknown";
            bool isLight = theme == "Light";
            ComboBoxItem selectedCurrency = (ComboBoxItem)this.CurrencyComboBox.SelectedItem;
            string currency = (selectedCurrency?.Content?.ToString() ?? "Unknown").ToLower();
            SettingsLogic.UpdateUser(this.userId, dblanguage, isLight, currency);
            SettingsPageView settingsPage = new SettingsPageView();
            Window.GetWindow(this).Content = settingsPage;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Main main = new Main();
            Window.GetWindow(this).Content = main;
        }

    }
}
