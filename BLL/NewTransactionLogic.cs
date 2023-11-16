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

    public class NewTransactionLogic
    {
        static public List<ExpenseCategoryWithExpenses> GetCategoriesAndExpensesByUserId(int userId)
        {
            List<ExpenseCategory> expenseCategoryies = DAL.ExpenseCategoryQueries.GetExpenseCategoriesByUserId(userId);

            List<ExpenseCategoryWithExpenses> categoriesWithExpenses = new List<ExpenseCategoryWithExpenses>();

            foreach (var category in expenseCategoryies)
            {
                List<Expense> expenses = DAL.ExpenseQueries.GetExpensesByExpenseCategoryId(category.Id);
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
