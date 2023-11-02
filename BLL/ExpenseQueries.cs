using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ExpenseCategoryWithExpenses
    {
        public ExpenseCategory expenseCategory;
        public List<Expense> expenses;
    }

    public class ExpenseQueries
    {
        //static public Expense AddExpense(string username, string email, string password)
        //{
            
        //}

        static public List<ExpenseCategoryWithExpenses> GetCategoriesAndExpensesByUserId(int userId)
        {
            List<ExpenseCategory> expenseCategoryies = Queries.GetExpenseCategoriesByUserId(userId);

            List<ExpenseCategoryWithExpenses> categoriesWithExpenses = new List<ExpenseCategoryWithExpenses>();

            foreach (var category in expenseCategoryies)
            {
                List<Expense> expenses = Queries.GetExpensesByExpenseCategory(category.Id);
                categoriesWithExpenses.Add(new ExpenseCategoryWithExpenses
                {
                    expenseCategory = category,
                    expenses = expenses
                });
            }

            return categoriesWithExpenses;
        }

    }
}
