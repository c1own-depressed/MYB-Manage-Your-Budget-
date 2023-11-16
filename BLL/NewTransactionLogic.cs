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

        public ExpenseCategoryWithExpenses()
        {
            expenseCategory = new ExpenseCategory();
            expenses = new List<Expense>();
        }
        public ExpenseCategoryWithExpenses(ExpenseCategory expenseCategory, List<Expense> expenses)
        {
            this.expenseCategory = expenseCategory;
            this.expenses = expenses;
        } 
    }

    public class NewTransactionLogic
    {
        static public List<ExpenseCategoryWithExpenses> GetCategoriesAndExpensesByUserId(int userId)
        {
            List<ExpenseCategory> expenseCategories = ExpenseCategoryQueries.GetExpenseCategoriesByUserId(userId);

            List<ExpenseCategoryWithExpenses> categoriesWithExpenses = new List<ExpenseCategoryWithExpenses>();

            foreach (var category in expenseCategories)
            {
                List<Expense> expenses = ExpenseQueries.GetExpensesByExpenseCategoryId(category.Id);
                categoriesWithExpenses.Add(new ExpenseCategoryWithExpenses
                {
                    expenseCategory = category,
                    expenses = expenses
                });
            }

            return categoriesWithExpenses;
        }

        static public Transaction AddTransaction(string transactionName, int amount, int expenseId)
        {
            Transaction transaction = new Transaction
            {
                TransactionName = transactionName,
                Amount = amount,
                Date = DateTime.Now,
                ExpenseId = expenseId
            };

            DAL.TransactionQueries.AddTransaction(transaction);

            return transaction;
        }
    }
}
