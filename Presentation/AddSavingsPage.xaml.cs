namespace MYB_NEW
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using BLL;

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
            var temp = MainPageLogic.GetSavingsByUserId(userId).FirstOrDefault(x => x.SavingName == savingsTitle && x.Amount == amount);
            int id = temp.Id;
            // Створіть новий об'єкт "Savings"
            SavingsUI newSavings = new SavingsUI(savingsTitle, amount);
            Button editButton = new Button
            {
                Style = (Style)this.Resources["InvisibleButtonStyle"],
                Width = 20,
                Height = 20,
                Name = $"EditIncomeButton_{Guid.NewGuid():N}",
                Content = new TextBlock
                {
                    Text = "E",
                    FontSize = 14,
                    FontWeight = FontWeights.Bold,
                },
            };

            editButton.Click += (sender, e) => this.EditSaving_Click(sender, e, id);
            Button deleteButton = new Button
            {
                Style = (Style)this.Resources["InvisibleButtonStyle"],
                Width = 20,
                Height = 20,
                Name = $"DeleteIncomeButton_{Guid.NewGuid().ToString("N")}",
                Content = new TextBlock
                {
                    Text = "D",
                    FontSize = 14,
                    FontWeight = FontWeights.Bold,
                },
            };

            deleteButton.Click += this.DeleteSaving_Click;
            // Додайте новий "Savings" до списку на головній сторінці (SavingsListView)
            if (this.savingsListView != null)
            {
                StackPanel savingsPanel = new StackPanel();
                savingsPanel.Orientation = Orientation.Horizontal;
                TextBlock savingTextBlock = new TextBlock
                {
                    Foreground = (SolidColorBrush)Application.Current.Resources["Text"],
                    Text = newSavings.Title,
                    FontSize = 16,
                    FontWeight = FontWeights.DemiBold,
                };
                savingsPanel.Children.Add(savingTextBlock);
                savingsPanel.Children.Add(editButton);
                savingsPanel.Children.Add(deleteButton);
                this.savingsListView.Children.Add(savingsPanel);
            }

            // Закрийте сторінку "AddSavingsPage"
            this.Close();
        }

        private void EditSaving_Click(object sender, RoutedEventArgs e, int id)
        {
            EditSavingsPage addCategoryPage = new EditSavingsPage(this, id);
            addCategoryPage.ShowDialog();
        }

        private void DeleteSaving_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
