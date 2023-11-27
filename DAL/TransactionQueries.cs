namespace DAL
{
    using System.Collections.Generic;
    using System.Linq;
    using MYB.DAL;

    public static class TransactionQueries
    {

        private static readonly AppDBContext _context;

        static TransactionQueries()
        {
            _context = new AppDBContext();
        }

        public static List<Transaction> GetTransactionsByExpenseIdYearAndMonth(int expenseID, int year, int month)
        {
            return (from transaction in _context.Transactions
                    where transaction.ExpenseId == expenseID && transaction.Date.Month == month && transaction.Date.Year == year
                    select transaction).ToList();
        }

        public static void AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }
    }
}
