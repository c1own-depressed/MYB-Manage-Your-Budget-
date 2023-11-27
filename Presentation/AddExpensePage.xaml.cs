namespace MYB_NEW
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using BLL;
    using DAL;
    using Presentation;

    /// <summary>
    /// Interaction logic for AddExpensePage.xaml.
    /// </summary>
    public partial class AddExpensePage : Window
    {
        private StackPanel expenseCategory;
        private int categoryId;
        ExpenseCategoryWithExpenses? expenses;

        public AddExpensePage(StackPanel category)
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
            this.expenseCategory = category;

        }

        public AddExpensePage(StackPanel category, int categoryId)
        {
            this.InitializeComponent();
            this.expenseCategory = category;
            this.categoryId = categoryId;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            int userId = UserManager.CurrentUser.Id;
            string expenseTitle = this.ExpenseTitleTextBox.Text;
            double plannedBudget = double.Parse(this.PlannedBudgetTextBox.Text);
            MainPageLogic.AddExpense(this.categoryId, expenseTitle, (int)plannedBudget);
            this.expenses = NewTransactionLogic.GetCategoriesAndExpensesByUserId(userId)
                    .FirstOrDefault(category => category.ExpenseCategory.Id == this.categoryId);

            // Створіть новий об'єкт "Expense"
            Expense newExpense = new Expense(expenseTitle, plannedBudget, this.expenseCategory);

            // Додайте новий "Expense" до категорії витрат (StackPanel)
            if (this.expenseCategory != null)
            {
                StackPanel newExpensePanel = new StackPanel();
                newExpensePanel.Orientation = Orientation.Horizontal;

                // Назва витрати
                TextBlock newExpenseTitle = new TextBlock
                {
                    Foreground = (SolidColorBrush)Application.Current.Resources["Text"],
                    Text = newExpense.Title,
                    FontSize = 16,
                    FontWeight = FontWeights.DemiBold,
                };

                // Пробіл
                TextBlock spaceText = new TextBlock
                {
                    Text = " ",
                    FontSize = 10,
                    FontWeight = FontWeights.DemiBold,
                    Foreground = Brushes.Gray,
                };

                // Бюджет
                TextBlock newExpenseBudget = new TextBlock
                {
                    Text = $"0/{newExpense.Amount} $",
                    FontSize = 12,
                    FontWeight = FontWeights.DemiBold,
                    Foreground = Brushes.Gray,
                    Height = 15,
                    VerticalAlignment = VerticalAlignment.Center,
                };
                Button editExpenseButton = new Button
                {
                    Style = (Style)this.Resources["InvisibleButtonStyle"],
                    Width = 20,
                    Name = $"EditIncomeButton_{Guid.NewGuid():N}",
                    VerticalAlignment = VerticalAlignment.Top,
                    Content = new TextBlock
                    {
                        Text = "E",
                        FontSize = 16,
                        FontWeight = FontWeights.Bold,
                    },
                };
                editExpenseButton.Click += (sender, e) => this.EditExpense_Click(sender, e, expenseTitle);

                Button deleteExpenseButton = new Button
                {
                    Style = (Style)this.Resources["InvisibleButtonStyle"],
                    Width = 20,
                    Name = $"EditIncomeButton_{Guid.NewGuid():N}",
                    VerticalAlignment = VerticalAlignment.Top,
                    Content = new TextBlock
                    {
                        Text = "D",
                        FontSize = 16,
                        FontWeight = FontWeights.Bold,
                    },
                };
                deleteExpenseButton.Click += (sender, e) => this.DeleteExpense_Click(sender, e, expenseTitle);

                // Додайте назву витрати, пробіл і бюджет в StackPanel
                newExpensePanel.Children.Add(newExpenseTitle);
                newExpensePanel.Children.Add(spaceText);
                newExpensePanel.Children.Add(newExpenseBudget);
                newExpensePanel.Children.Add(editExpenseButton);
                newExpensePanel.Children.Add(deleteExpenseButton);
                // Знайдіть кнопку "Add Expense" та вставте нову витрату перед нею
                for (int i = 0; i < this.expenseCategory.Children.Count; i++)
                {
                    if (this.expenseCategory.Children[i] is Button addExpenseButton)
                    {
                        this.expenseCategory.Children.Insert(i, newExpensePanel);
                        break;
                    }
                }
            }

            // Закрийте сторінку "AddExpensePage"
            this.Close();
        }

        private void EditExpense_Click(object sender, RoutedEventArgs e,  string currentIndexExpense)
        {
            int Id = this.expenses.Expenses
                .Where(expense => expense.ExpenseName == currentIndexExpense).Select(expense => expense.Id).FirstOrDefault();
            EditExpensePage editExpensePage = new EditExpensePage(this, this.categoryId, Id);
            editExpensePage.ShowDialog();
        }

        private void DeleteExpense_Click(object sender, RoutedEventArgs e, string currentIndexExpense)
        {
            int Id = this.expenses.Expenses
               .Where(expense => expense.ExpenseName == currentIndexExpense).Select(expense => expense.Id).FirstOrDefault();
            DeleteExpensesPage deleteExpenses = new DeleteExpensesPage(this, this.categoryId, Id);
            deleteExpenses.ShowDialog();
        }
    }
}
