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
                List<Expense> expenses = Queries.GetExpensesByExpenseCategoryId(category.Id);
                categoriesWithExpenses.Add(new ExpenseCategoryWithExpenses
                {
                    expenseCategory = category,
                    expenses = expenses
                });
            }

            return categoriesWithExpenses;
        }

        static public void AddExpenseCategory(int userId, string expenseCategoryName)
        {
            ExpenseCategory expenseCategory = new ExpenseCategory()
            {
                UserId = userId,
                CategoryName = expenseCategoryName
            };
            Queries.AddExpenseCategory(expenseCategory);
        }

        static public void AddExpense(int expenseCategoryId, string expenseName, int amount)
        {
            Expense expense = new Expense()
            {
                ExpenseCategoryId = expenseCategoryId,
                ExpenseName = expenseName,
                Amount = amount
            };
            Queries.AddExpense(expense);
        }

    }
}
