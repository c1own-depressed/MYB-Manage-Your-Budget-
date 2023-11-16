namespace MYB_NEW
{
    using BLL;
    using System.Windows;
    using System.Windows.Controls;

    public partial class AddSavingsPage : Window
    {
        private StackPanel savingsListView;

        public AddSavingsPage(StackPanel listView)
        {
            this.InitializeComponent();
            this.savingsListView = listView;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string savingsTitle = this.SavingsTitleTextBox.Text;
            double amount = double.Parse(this.AmountTextBox.Text);

            int userId = UserManager.CurrentUser.Id;
            MainPageLogic.AddSavings(userId, savingsTitle, (int)amount);

            // Створіть новий об'єкт "Savings"
            SavingsUI newSavings = new SavingsUI(savingsTitle, amount);

            // Додайте новий "Savings" до списку на головній сторінці (SavingsListView)
            if (this.savingsListView != null)
            {
                this.savingsListView.Children.Add(new TextBlock() { Text = newSavings.Title, FontSize = 30, FontWeight = FontWeights.DemiBold });
            }

            // Закрийте сторінку "AddSavingsPage"
            this.Close();
        }
    }
}
