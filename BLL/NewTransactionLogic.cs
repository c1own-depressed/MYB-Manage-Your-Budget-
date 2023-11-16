namespace BLL
{
    using System;
    using System.Collections.Generic;
    using DAL;

    public class ExpenseCategoryWithExpenses
    {
        public ExpenseCategory ExpenseCategory;
        public List<Expense> Expenses;

        public ExpenseCategoryWithExpenses()
        {
            ExpenseCategory = new ExpenseCategory();
            Expenses = new List<Expense>();
        }
        public ExpenseCategoryWithExpenses(ExpenseCategory expenseCategory, List<Expense> expenses)
        {
            this.ExpenseCategory = expenseCategory;
            this.Expenses = expenses;
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
                    ExpenseCategory = category,
                    Expenses = expenses,
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
