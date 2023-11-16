using BLL;
using Microsoft.VisualBasic;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MYB_NEW
{
    public partial class AddSavingsPage : Window
    {
        private StackPanel savingsListView;

        public AddSavingsPage(StackPanel listView)
        {
            InitializeComponent();
            this.savingsListView = listView;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string savingsTitle = SavingsTitleTextBox.Text;
            double amount = double.Parse(AmountTextBox.Text);

            InnerUser currentUser = UserManager.Instance.CurrentUser;
            int userId = currentUser.UserId;
            MainPageLogic.AddSavings(userId, savingsTitle, (int)amount);

            // Створіть новий об'єкт "Savings"
            SavingsUI newSavings = new SavingsUI(savingsTitle, amount);

            // Додайте новий "Savings" до списку на головній сторінці (SavingsListView)
            if (savingsListView != null)
            {
                savingsListView.Children.Add(new TextBlock() { Text = newSavings.Title, FontSize = 30, FontWeight = FontWeights.DemiBold });
            }

            // Закрийте сторінку "AddSavingsPage"
            this.Close();
        }
    }
}
