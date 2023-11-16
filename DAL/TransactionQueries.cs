using Microsoft.EntityFrameworkCore;
using MYB.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class TransactionQueries
    {

        private static readonly AppDBContext _context;

        static TransactionQueries()
        {
            _context = new AppDBContext();
        }

        public static List<Transaction> GetTransactionsByExpenseId(int expenseID)
        {
            return (from transaction in _context.Transactions
                    where transaction.ExpenseId == expenseID
                    select transaction).ToList();
        }

        public static void AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }
    }
}
