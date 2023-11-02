using DAL;
using MYB.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace DAL
{
    public static class Queries
    {
        private static readonly AppDBContext _context;

        static Queries()
        {
            _context = new AppDBContext();
        }


        public static User GetUserById(int userId)
        {
            //return _context.Users.FirstOrDefault(u => u.Id == userId);
            return (from user in _context.Users
                   where user.Id == userId
                   select user).FirstOrDefault();
        }

        public static List<ExpenseCategory> GetExpenseCategoriesByUserId(int userID)
        {
            return (from expenseCat in _context.ExpenseCategories
                   where expenseCat.UserId == userID
                   select expenseCat).ToList();
        }

        public static List<Expense> GetExpensesByExpenseCategory(int expenseCategoryID)
        {
            return (from expense in _context.Expenses
                   where expense.ExpenseCategoryId == expenseCategoryID
                   select expense).ToList();
        }

        public static List<Transaction> GetTransactionsByExpenseId(int expenseID)
        {
            return (from transaction in _context.Transactions
                    where transaction.ExpenseId == expenseID
                    select transaction).ToList();
        }

        public static List<Saving> GetSavingsByUserId(int userID)
        {
            return (from saving in _context.Savings
                    where saving.UserId == userID
                    select saving).ToList();
        }

        public static List<Income> GetIncomeByUserId(int userID)
        {
            return (from income in _context.Incomes
                    where income.UserId == userID
                    select income).ToList();
        }

        public static void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}

