namespace MYB_NEW
{
    using BLL;
    using DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;

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
            int cost = Convert.ToInt32(this.ExpenseBudget.Text);
            string transactionName = this.TitleOfExpense.Text;
            MainPageLogic.EditExpense(expenseId, transactionName);
            this.Close();
        }
    }
}
