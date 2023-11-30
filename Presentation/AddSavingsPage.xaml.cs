namespace MYB_NEW
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using BLL;
    using DAL;
    using Presentation;

    public partial class AddSavingsPage : Window
    {
        private StackPanel savingsListView;
        Main main;

        public AddSavingsPage(Main main)
        {
            if (UserManager.CurrentUser.Language == "ua")
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("uk-UA");
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            }
            this.InitializeComponent();
            this.savingsListView = main.SavingsListView;
            this.main = main;
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

            deleteButton.Click += (sender, e) => this.DeleteSaving_Click(sender, e, id);
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
            EditSavingsPage editSavingsPage = new EditSavingsPage(this, id);
            editSavingsPage.ShowDialog();
            this.main.UpdateUIAfterCategoryChange();
        }

        private void DeleteSaving_Click(object sender, RoutedEventArgs e, int id)
        {
            DeleteSavingsPage deleteSavingsPage = new DeleteSavingsPage(this, id);
            deleteSavingsPage.ShowDialog();
            this.main.UpdateUIAfterCategoryChange();
        }
    }
}
