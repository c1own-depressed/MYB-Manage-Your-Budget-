namespace DAL
{
    using System.Collections.Generic;
    using System.Linq;
    using MYB.DAL;

    public static class ExpenseQueries
    {
        private static readonly AppDBContext _context;

        static ExpenseQueries()
        {
            _context = new AppDBContext();
        }

        public static List<Expense> GetExpensesByExpenseCategoryId(int expenseCategoryID)
        {
            return (from expense in _context.Expenses
                    where expense.ExpenseCategoryId == expenseCategoryID
                    select expense).ToList();
        }

        public static void AddExpense(Expense expense)
        {
            _context.Expenses.Add(expense);
            _context.SaveChanges();
        }

        public static void EditExpense(int expenseId, string expenseName)
        {
            var dbExpense = _context.Expenses.Find(expenseId);

            if (dbExpense != null)
            {
                dbExpense.ExpenseName = expenseName;

                _context.SaveChanges();
            }
        }

        public static void DeleteExpense(int expenseId)
        {
            var expenseToDelete = _context.Expenses.SingleOrDefault(e => e.Id == expenseId);

            if (expenseToDelete != null)
            {
                _context.Expenses.Remove(expenseToDelete);
                _context.SaveChanges();
            }
        }
    }
}
