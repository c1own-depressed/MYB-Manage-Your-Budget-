﻿using DAL;
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

        public static User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
            //return (from user in _context.Users
            //        where user.Username == username
            //        select user).FirstOrDefault();
        }
        public static User GetUserByEmail(string email)
        {
            return (from user in _context.Users
                    where user.Email == email
                    select user).FirstOrDefault();
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

        public static List<Expense> GetExpensesByExpenseCategoryId(int expenseCategoryID)
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

        public static void AddIncome(Income income)
        {
            _context.Incomes.Add(income);
            _context.SaveChanges();
        }

        public static void AddSaving(Saving saving)
        {
            _context.Savings.Add(saving);
            _context.SaveChanges();
        }

        public static void AddExpenseCategory(ExpenseCategory expenseCategory)
        {
            _context.ExpenseCategories.Add(expenseCategory);
            _context.SaveChanges();
        }

        public static void AddExpense(Expense expense)
        {
            _context.Expenses.Add(expense);
            _context.SaveChanges();
        }

        public static void AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public static User UpdateUser(int userId, string language, bool isLightTheme, string currency)
        {
            var existingUser = _context.Users.Find(userId);

            if (existingUser != null)
            {
                existingUser.LightTheme = isLightTheme;
                existingUser.Language = language;
                existingUser.Currency = currency;

                _context.SaveChanges();
            }

            return GetUserById(userId);
        }

        public static void DeleteUser(int userId)
        {
            var userToDelete = _context.Users.Find(userId);

            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
            }
        }
    }
}

