using BLL;
using DAL;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MYB_NEW
{
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            InnerUser currentUser = UserManager.Instance.CurrentUser;
            int userId = currentUser.UserId;

            List<Income> incomes = IncomeQueries.GetIncomesByUserId(userId);
            List<Saving> savings = SavingQueries.GetSavingsByUserId(userId);
            List<ExpenseCategoryWithExpenses> expenses = ExpenseQueries.GetCategoriesAndExpensesByUserId(userId);
        }

        private void AddIncome_Click(object sender, RoutedEventArgs e)
        {
            AddIncomePage addIncomePage = new AddIncomePage(IncomeListView); 
            addIncomePage.ShowDialog();
        }

        private void AddSavings_Click(object sender, RoutedEventArgs e)
        {
            AddSavingsPage addSavingsPage = new AddSavingsPage(SavingsListView); 
            addSavingsPage.ShowDialog();
        }


        public void AddExpense_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender; // Отримайте посилання на кнопку, яка спричинила подію

            if (clickedButton == AddExpenseCategory1)
            {
                AddExpensePage addExpensePage = new AddExpensePage(Category1ExpensesListView);
                addExpensePage.ShowDialog();
            }
            else if (clickedButton == AddExpenseCategory2)
            {
                AddExpensePage addExpensePage = new AddExpensePage(Category2ExpensesListView);
                addExpensePage.ShowDialog();
            }
        }


        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryPage addCategoryPage = new AddCategoryPage(this); // Pass 'this' as the 'mainPage' argument
            addCategoryPage.ShowDialog();
        }

        public void AddCategory(Border newCategory)
        {
            // Додайте нову категорію на сторінку "Main" над кнопкою "Add Category"
            CategoriesListView.Children.Insert(CategoriesListView.Children.Count - 1, newCategory);
        }

        private void AddTransaction_Click(object sender, RoutedEventArgs e)
        {
            // Тут ви можете додати функціональність для кнопки "Add Transaction"
        }
        private bool buttonsVisible = false;

        private void Toggle_Click(object sender, RoutedEventArgs e)
        {
            if (buttonsVisible)
            {
                // Приховати кнопки "Statistic", "Data export", "Tips and Tricks", "Settings" і "Sign out"
                StatisticButton.Visibility = Visibility.Collapsed;
                DataExportButton.Visibility = Visibility.Collapsed;
                TipsAndTricksButton.Visibility = Visibility.Collapsed;
                SettingsButton.Visibility = Visibility.Collapsed;
                SignOutButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Показати кнопки "Statistic", "Data export", "Tips and Tricks", "Settings" і "Sign out"
                StatisticButton.Visibility = Visibility.Visible;
                DataExportButton.Visibility = Visibility.Visible;
                TipsAndTricksButton.Visibility = Visibility.Visible;
                SettingsButton.Visibility = Visibility.Visible;
                SignOutButton.Visibility = Visibility.Visible;
            }

            buttonsVisible = !buttonsVisible;
        }
        private void EditIncome_Click(object sender, RoutedEventArgs e)
        {
            // Додайте реалізацію для обробки події редагування доходів
        }

        private void DeleteIncome_Click(object sender, RoutedEventArgs e)
        {
            // Додайте реалізацію для обробки події редагування доходів
        }
        private void EditSavings_Click(object sender, RoutedEventArgs e)
        {
            // Додайте реалізацію для обробки події редагування доходів
        }
        private void DeleteSavings_Click(object sender, RoutedEventArgs e)
        {
            // Додайте реалізацію для обробки події редагування доходів
        }
        private void EditCategory_Click(object sender, RoutedEventArgs e)
        {
            // Додайте реалізацію для обробки події редагування доходів
        }
        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            // Додайте реалізацію для обробки події редагування доходів
        }
        private void EditExpense_Click(object sender, RoutedEventArgs e)
        {
            // Додайте реалізацію для обробки події редагування доходів
        }
        private void DeleteExpense_Click(object sender, RoutedEventArgs e)
        {
            // Додайте реалізацію для обробки події редагування доходів
        }

    }
}
