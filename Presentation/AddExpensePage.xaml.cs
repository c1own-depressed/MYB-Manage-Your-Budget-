namespace MYB_NEW
{
    using BLL;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for AddExpensePage.xaml.
    /// </summary>
    public partial class AddExpensePage : Window
    {
        private StackPanel expenseCategory;
        private int categoryId;

        public AddExpensePage(StackPanel category)
        {
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
            string expenseTitle = this.ExpenseTitleTextBox.Text;
            double plannedBudget = double.Parse(this.PlannedBudgetTextBox.Text);
            MainPageLogic.AddExpense(this.categoryId, expenseTitle, (int)plannedBudget);

            // Створіть новий об'єкт "Expense"
            Expense newExpense = new Expense(expenseTitle, plannedBudget, expenseCategory);

            // Додайте новий "Expense" до категорії витрат (StackPanel)
            if (expenseCategory != null)
            {
                StackPanel newExpensePanel = new StackPanel();
                newExpensePanel.Orientation = Orientation.Horizontal;

                // Назва витрати
                TextBlock newExpenseTitle = new TextBlock
                {
                    Text = newExpense.Title,
                    FontSize = 30,
                    FontWeight = FontWeights.DemiBold
                };

                // Пробіл
                TextBlock spaceText = new TextBlock
                {
                    Text = " ",
                    FontSize = 10,
                    FontWeight = FontWeights.DemiBold,
                    Foreground = Brushes.Gray
                };

                // Бюджет
                TextBlock newExpenseBudget = new TextBlock
                {
                    Text = $"0/{newExpense.Amount} $",
                    FontSize = 24,
                    FontWeight = FontWeights.DemiBold,
                    Foreground = Brushes.Gray,
                    Height = 35
                };

                // Додайте назву витрати, пробіл і бюджет в StackPanel
                newExpensePanel.Children.Add(newExpenseTitle);
                newExpensePanel.Children.Add(spaceText);
                newExpensePanel.Children.Add(newExpenseBudget);

                // Знайдіть кнопку "Add Expense" та вставте нову витрату перед нею
                for (int i = 0; i < expenseCategory.Children.Count; i++)
                {
                    if (expenseCategory.Children[i] is Button addExpenseButton)
                    {
                        expenseCategory.Children.Insert(i, newExpensePanel);
                        break;
                    }
                }
            }

            // Закрийте сторінку "AddExpensePage"
            Close();
        }
    }
}