namespace OtherPages
{
    using System.Windows;
    using System.Windows.Controls;
    using BLL;
    using DAL;
    using MYB_NEW;

    /// <summary>
    /// Interaction logic for Page1.xaml.
    /// </summary>
    public partial class SettingsPageView : Page
    {
        private int userId;

        public SettingsPageView()
        {

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
            MessageBox.Show("Success!");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Main main = new Main();
            Window.GetWindow(this).Content = main;
        }
    }
}
