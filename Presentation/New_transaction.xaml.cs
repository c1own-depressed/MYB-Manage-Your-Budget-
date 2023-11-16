namespace OtherPages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using BLL;
    using MYB_NEW;

    /// <summary>
    /// Interaction logic for New_transaction.xaml.
    /// </summary>
    public partial class New_transaction : Page
    {
        private int userId;
        private ExpenseCategoryWithExpenses? selectedCategoryWithExpenses;

        public New_transaction()
        {
            this.InitializeComponent();

            this.userId = UserManager.CurrentUser.Id;

            List<ExpenseCategoryWithExpenses> categoriesWithExpenses = NewTransactionLogic.GetCategoriesAndExpensesByUserId(this.userId);
            foreach (var categoryWithExpenses in categoriesWithExpenses)
            {
                this.CategoryComboBox.Items.Add(categoryWithExpenses.ExpenseCategory.CategoryName);
            }

            this.CategoryComboBox.SelectionChanged += this.CategoryComboBox_SelectionChanged;
            this.ExpenseComboBox.IsEnabled = false;
            this.CostTextBox.IsEnabled = false;
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ExpenseComboBox.Items.Clear();

            if (this.CategoryComboBox.SelectedItem != null)
            {
                string selectedCategory = this.CategoryComboBox?.SelectedItem?.ToString() ?? "Unknown";
                this.selectedCategoryWithExpenses = NewTransactionLogic.GetCategoriesAndExpensesByUserId(this.userId)
                    .FirstOrDefault(category => category.ExpenseCategory.CategoryName == selectedCategory);
                if (this.selectedCategoryWithExpenses != null)
                {
                    foreach (var expense in this.selectedCategoryWithExpenses.Expenses)
                    {
                        ComboBoxItem comboBoxItem = new ComboBoxItem();
                        comboBoxItem.Content = expense.ExpenseName;
                        this.ExpenseComboBox.Items.Add(comboBoxItem);
                    }
                }

                this.ExpenseComboBox.IsEnabled = true;
                this.CostTextBox.IsEnabled = true;
            }
            else
            {
                this.CostTextBox.IsEnabled = false;
                this.ExpenseComboBox.IsEnabled = false;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Main main = new Main();
            Window.GetWindow(this).Content = main;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem expenseItem = (ComboBoxItem)this.ExpenseComboBox.SelectedItem;
            string expenseName = expenseItem?.Content?.ToString() ?? "Unknown";
            var expense = selectedCategoryWithExpenses.Expenses.FirstOrDefault(expense => expense.ExpenseName == expenseName);
            int cost = Convert.ToInt32(this.CostTextBox.Text);
            string transactionName = this.TransactionTextBox.Text;
            NewTransactionLogic.AddTransaction(transactionName, cost, expense.Id);
        }
    }
}
