namespace MYB_NEW
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media; // for SolidColorBrush
    using System.Windows.Threading;
    using BLL;
    using DAL;

    /// <summary>
    /// Interaction logic for EditExpensePage.xaml
    /// </summary>
    public partial class EditExpensePage : Window
    {
        private int expenseId;
        private int categoryId;
        private List<ExpenseCategoryWithExpenses> categoryWithExpenses;

        public EditExpensePage(object main,int categoryId,int expenseId)
        {
            if (UserManager.CurrentUser.Language == "ua")
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("uk-UA");
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            }
            int userId = UserManager.CurrentUser.Id;
            this.InitializeComponent();
            this.expenseId= expenseId;
            this.categoryId = categoryId;
            this.categoryWithExpenses = MainPageLogic.GetCategoriesAndExpensesByUserId(userId);
            ExpenseCategoryWithExpenses? temp = this.categoryWithExpenses.FirstOrDefault(category => category.ExpenseCategory.Id == this.categoryId);
            DAL.Expense? temp1=temp.Expenses.FirstOrDefault(x=>x.Id == expenseId);
            this.TitleOfExpense.Text = temp1.ExpenseName;
            this.ExpenseBudget.Text = temp1.Amount.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int cost = Convert.ToInt32(this.ExpenseBudget.Text);
                string transactionName = this.TitleOfExpense.Text;
                MainPageLogic.EditExpense(expenseId, transactionName, cost);

                this.Close();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message, LogLevel.Error);

                this.ExpenseBudget.Background = Brushes.LightPink;

                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(500);
                timer.Tick += (s, args) =>
                {
                    this.ExpenseBudget.Background = Brushes.White;
                    var expense = (from expCategory in UserManager.userExpenseCategoriesWithExpenses
                                   from exp in expCategory.Expenses
                                   where exp.ExpenseName == this.TitleOfExpense.Text
                                   select exp).FirstOrDefault();
                    if (expense != null)
                    {
                        this.ExpenseBudget.Text = expense.Amount.ToString();
                    }

                    timer.Stop();
                };

                timer.Start();
            }
        }
    }
}
