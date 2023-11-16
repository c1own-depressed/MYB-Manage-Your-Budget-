using Microsoft.EntityFrameworkCore;
using MYB.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class ExpenseCategoryQueries
    {

        private static readonly AppDBContext _context;

        static ExpenseCategoryQueries()
        {
            _context = new AppDBContext();
        }
        public static ExpenseCategory GetExpenseCategoryById(int expenseCategoryId)
        {
            return (from expenseCat in _context.ExpenseCategories
                    where expenseCat.Id == expenseCategoryId
                    select expenseCat).First();
        }

        public static List<ExpenseCategory> GetExpenseCategoriesByUserId(int userID)
        {
            return (from expenseCat in _context.ExpenseCategories
                    where expenseCat.UserId == userID
                    select expenseCat).ToList();
        }

        public static void AddExpenseCategory(ExpenseCategory expenseCategory)
        {
            _context.ExpenseCategories.Add(expenseCategory);
            _context.SaveChanges();
        }
    }
}
