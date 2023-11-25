namespace MYB_NEW
{
    using BLL;
    using DAL;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;

    /// <summary>
    /// Interaction logic for EditCategoryPage.xaml.
    /// </summary>
    public partial class EditCategoryPage : Window
    {
        int expenseCategoryId;
        List<ExpenseCategoryWithExpenses> categoryWithExpenses;

        public EditCategoryPage(Main main, int expenseCategoryId)
        {
            int userId = UserManager.CurrentUser.Id;
            this.InitializeComponent();
            this.expenseCategoryId = expenseCategoryId;
            this.categoryWithExpenses = MainPageLogic.GetCategoriesAndExpensesByUserId(userId);
            ExpenseCategoryWithExpenses? temp = this.categoryWithExpenses.FirstOrDefault(category => category.ExpenseCategory.Id == this.expenseCategoryId);
            this.TitleOfCategoryTextBox.Text = temp.ExpenseCategory.CategoryName;
        }
    }
}
