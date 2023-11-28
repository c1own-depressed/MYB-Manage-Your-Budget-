namespace BLL
{
    using System;
    using System.Collections.Generic;
    using DAL;

    public class ExpenseCategoryWithExpenses
    {
        public ExpenseCategory ExpenseCategory;
        public List<Expense> Expenses;
        public Dictionary<Expense, double> ExpensesWithCost;

        public ExpenseCategoryWithExpenses()
        {
            this.ExpenseCategory = new ExpenseCategory();
            this.Expenses = new List<Expense>();
            this.ExpensesWithCost = new Dictionary<Expense, double>();
        }

        public ExpenseCategoryWithExpenses(ExpenseCategory expenseCategory, List<Expense> expenses, Dictionary<Expense, double> expensesWithCost)
        {
            this.ExpenseCategory = expenseCategory;
            this.Expenses = expenses;
            this.ExpensesWithCost = expensesWithCost;
        }
    }

    public class NewTransactionLogic
    {
        //public static List<ExpenseCategoryWithExpenses> GetCategoriesAndExpensesByUserId(int userId)
        //{
        //    List<ExpenseCategory> expenseCategories = ExpenseCategoryQueries.GetExpenseCategoriesByUserId(userId);

        //    List<ExpenseCategoryWithExpenses> categoriesWithExpenses = new List<ExpenseCategoryWithExpenses>();

        //    foreach (var category in expenseCategories)
        //    {
        //        List<Expense> expenses = ExpenseQueries.GetExpensesByExpenseCategoryId(category.Id);
        //        // TODO: fill the ExpensesWithCost

        //        Dictionary<Expense, double> expensesWithCost = new Dictionary<Expense, double>();
                
        //        int month = DateTime.Now.Month;
        //        int year = DateTime.Now.Year;
        //        foreach (var expense in expenses)
        //        {
        //            var transactions = TransactionQueries.GetTransactionsByExpenseIdYearAndMonth(expense.Id, year, month);

        //            double suma = 0;
        //            foreach (var transaction in transactions)
        //            {
        //                suma += transaction.Amount;
        //            }

        //            expensesWithCost.Add(expense, suma);
        //        }

        //        categoriesWithExpenses.Add(new ExpenseCategoryWithExpenses
        //        {
        //            ExpenseCategory = category,
        //            Expenses = expenses,
        //            ExpensesWithCost = expensesWithCost,
        //        });
        //    }

        //    return categoriesWithExpenses;
        //}

        public static Transaction AddTransaction(string transactionName, int amount, int expenseId)
        {
            Transaction transaction = new Transaction
            {
                TransactionName = transactionName,
                Amount = amount,
                Date = DateTime.Now,
                ExpenseId = expenseId,
            };

            TransactionQueries.AddTransaction(transaction);

            return transaction;
        }
    }
}
