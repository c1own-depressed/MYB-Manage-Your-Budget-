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
    public class Queries
    {
        private readonly AppDBContext _context;

        public Queries()
        {
            _context = new AppDBContext();
        }
        public Queries(AppDBContext context)
        {
            _context = context;
        }

        public User GetUserById(int userId)
        {
            //return _context.Users.FirstOrDefault(u => u.Id == userId);
            return (from user in _context.Users
                   where user.Id == userId
                   select user).FirstOrDefault();
        }

        public List<ExpenseCategory> GetExpenseCategoriesByUserId(int userID)
        {
            return (from expenseCat in _context.ExpenseCategories
                   where expenseCat.UserId == userID
                   select expenseCat).ToList();
        }

        public List<Expense> GetExpensesByExpenseCategory(int expenseCategoryID)
        {
            return (from expense in _context.Expenses
                   where expense.ExpenseCategoryId == expenseCategoryID
                   select expense).ToList();
        }

        public List<Transaction> GetTransactionsByExpenseId(int expenseID)
        {
            return (from transaction in _context.Transactions
                    where transaction.ExpenseId == expenseID
                    select transaction).ToList();
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}

