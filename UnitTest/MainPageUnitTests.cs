using System.Collections.Generic;
using BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MainPageTest
{
    [TestClass]
    public class MainPageUnitTests
    {
        [TestMethod]
        public void GetIncomeByUserId_ShouldReturnListOfSavings()
        {
            DAL.User user = new DAL.User("User210", "user210@gmail.com", "password", true, "uk", "uah");
            DAL.UserQueries.AddUser(user);
            DAL.User userWithId = DAL.UserQueries.GetUserByUsername("User210");

            DAL.Income income = new DAL.Income
            {
                IncomeName = "",
                Amount = 300,
                UserId = userWithId.Id,
            };
            DAL.IncomeQueries.AddIncome(income);

            List<DAL.Income> incomesFromDb = MainPageLogic.GetIncomesByUserId(userWithId.Id);

            CollectionAssert.AreEqual(incomesFromDb, new List<DAL.Income>(){ income });
        }

        [TestMethod]
        public void GetSavingsByUserId_ShouldReturnListOfSavings()
        {
            DAL.User user = new DAL.User("User211", "user211@gmail.com", "password", true, "uk", "uah");
            DAL.UserQueries.AddUser(user);
            DAL.User userWithId = DAL.UserQueries.GetUserByUsername("User211");

            DAL.Saving saving = new DAL.Saving
            {
                SavingName = "Emergency Fund",
                Amount = 500,
                UserId = userWithId.Id,
            };
            DAL.SavingQueries.AddSaving(saving);

            List<DAL.Saving> savingsFromDb = MainPageLogic.GetSavingsByUserId(userWithId.Id);

            CollectionAssert.AreEqual(savingsFromDb, new List<DAL.Saving> { saving });
        }

        [TestMethod]
        public void GetExpensesByUserId_ShouldReturnListOfExpenses()
        {
            DAL.User user = new DAL.User("User213", "user213@gmail.com", "password", true, "uk", "uah");
            DAL.UserQueries.AddUser(user);
            DAL.User userWithId = DAL.UserQueries.GetUserByUsername("User213");

            DAL.ExpenseCategory expenseCategory = new DAL.ExpenseCategory
            {
                CategoryName = "Groceries",
                UserId = userWithId.Id,
            };
            DAL.ExpenseCategoryQueries.AddExpenseCategory(expenseCategory);

            DAL.Expense expense = new DAL.Expense
            {
                ExpenseName = "Food",
                Amount = 100,
                ExpenseCategoryId = expenseCategory.Id,
            };
            DAL.ExpenseQueries.AddExpense(expense);

            List<ExpenseCategoryWithExpenses> expensesWithCatFromDb = MainPageLogic.GetCategoriesAndExpensesByUserId(userWithId.Id);
            List<ExpenseCategoryWithExpenses> expensesWithCat = new List<ExpenseCategoryWithExpenses>
            {
                new ExpenseCategoryWithExpenses
                {
                    ExpenseCategory = expenseCategory,
                    Expenses = new List<DAL.Expense>() { expense },
                }
            };

            Assert.AreEqual(expensesWithCatFromDb[0].ExpenseCategory, expensesWithCat[0].ExpenseCategory);
        }

        [TestMethod]
        public void GetExpensesByUserId2_ShouldReturnListOfExpenses()
        {
            DAL.User user = new DAL.User("User214", "user214@gmail.com", "password", true, "uk", "uah");
            DAL.UserQueries.AddUser(user);
            DAL.User userWithId = DAL.UserQueries.GetUserByUsername("User214");

            DAL.ExpenseCategory expenseCategory = new DAL.ExpenseCategory
            {
                CategoryName = "Mobile phones",
                UserId = userWithId.Id,
            };
            DAL.ExpenseCategoryQueries.AddExpenseCategory(expenseCategory);

            DAL.Expense expense1 = new DAL.Expense
            {
                ExpenseName = "Asus",
                Amount = 8000,
                ExpenseCategoryId = expenseCategory.Id,
            };
            DAL.Expense expense2 = new DAL.Expense
            {
                ExpenseName = "Xiaomi",
                Amount = 6000,
                ExpenseCategoryId = expenseCategory.Id,
            };
            DAL.Expense expense3 = new DAL.Expense
            {
                ExpenseName = "Nokia",
                Amount = 7000,
                ExpenseCategoryId = expenseCategory.Id,
            };

            DAL.ExpenseQueries.AddExpense(expense1);
            DAL.ExpenseQueries.AddExpense(expense2);
            DAL.ExpenseQueries.AddExpense(expense3);

            List<ExpenseCategoryWithExpenses> expensesWithCatFromDb = MainPageLogic.GetCategoriesAndExpensesByUserId(userWithId.Id);
            List<ExpenseCategoryWithExpenses> expensesWithCat = new List<ExpenseCategoryWithExpenses>
            {
                new ExpenseCategoryWithExpenses
                {
                    ExpenseCategory = expenseCategory,
                    Expenses = new List<DAL.Expense>() { expense1, expense2, expense3 },
                }
            };

            CollectionAssert.AreEqual(expensesWithCatFromDb[0].Expenses, expensesWithCat[0].Expenses);
        }
    }

}