using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BLL;

namespace UnitTest.BLLTests
{
    public class MainPageUnitTests
    {
        [Fact]
        public void GetIncomesByUserId_ReturnsListOfIncomes()
        {
            // Arrange
            int userId = 1;
            List<Income> fakeIncomes = new List<Income> { new Income(), new Income() };

            var mockIncomeQueries = new Mock<IIncomeQueries>();
            mockIncomeQueries.Setup(x => x.GetIncomeByUserId(userId)).Returns(fakeIncomes);

            // Act
            List<Income> result = MainPageLogic.GetIncomesByUserId(userId, mockIncomeQueries.Object);

            // Assert
            Assert.Equal(fakeIncomes, result);
        }

        [Fact]
        public void AddIncome_CallsAddIncomeMethod()
        {
            // Arrange
            int userId = 1;
            string incomeName = "Salary";
            int amount = 1000;

            var mockIncomeQueries = new Mock<IIncomeQueries>();

            // Act
            MainPageLogic.AddIncome(userId, incomeName, amount, mockIncomeQueries.Object);

            // Assert
            mockIncomeQueries.Verify(x => x.AddIncome(It.IsAny<Income>()), Times.Once);
        }

        [Fact]
        public void GetSavingsByUserId_ReturnsListOfSavings()
        {
            // Arrange
            int userId = 1;
            List<Saving> fakeSavings = new List<Saving> { new Saving(), new Saving() };

            var mockSavingQueries = new Mock<ISavingQueries>();
            mockSavingQueries.Setup(x => x.GetSavingsByUserId(userId)).Returns(fakeSavings);

            // Act
            List<Saving> result = MainPageLogic.GetSavingsByUserId(userId, mockSavingQueries.Object);

            // Assert
            Assert.Equal(fakeSavings, result);
        }

        [Fact]
        public void AddSavings_CallsAddSavingMethod()
        {
            // Arrange
            int userId = 1;
            string savingName = "Emergency Fund";
            int amount = 500;

            var mockSavingQueries = new Mock<ISavingQueries>();

            // Act
            MainPageLogic.AddSavings(userId, savingName, amount, mockSavingQueries.Object);

            // Assert
            mockSavingQueries.Verify(x => x.AddSaving(It.IsAny<Saving>()), Times.Once);
        }

        [Fact]
        public void GetCategoriesAndExpensesByUserId_ReturnsListOfCategoriesWithExpenses()
        {
            // Arrange
            int userId = 1;
            List<ExpenseCategory> fakeCategories = new List<ExpenseCategory> { new ExpenseCategory(), new ExpenseCategory() };
            List<Expense> fakeExpenses = new List<Expense> { new Expense(), new Expense() };

            var mockExpenseCategoryQueries = new Mock<IExpenseCategoryQueries>();
            var mockExpenseQueries = new Mock<IExpenseQueries>();

            mockExpenseCategoryQueries.Setup(x => x.GetExpenseCategoriesByUserId(userId)).Returns(fakeCategories);
            mockExpenseQueries.Setup(x => x.GetExpensesByExpenseCategoryId(It.IsAny<int>())).Returns(fakeExpenses);

            // Act
            List<ExpenseCategoryWithExpenses> result = MainPageLogic.GetCategoriesAndExpensesByUserId(userId, mockExpenseCategoryQueries.Object, mockExpenseQueries.Object);

            // Assert
            Assert.Equal(fakeCategories.Count, result.Count);
            Assert.Equal(fakeExpenses.Count, result[0].expenses.Count);
        }

        [Fact]
        public void AddExpenseCategory_CallsAddExpenseCategoryMethod()
        {
            // Arrange
            int userId = 1;
            string expenseCategoryName = "Groceries";

            var mockExpenseCategoryQueries = new Mock<IExpenseCategoryQueries>();

            // Act
            MainPageLogic.AddExpenseCategory(userId, expenseCategoryName, mockExpenseCategoryQueries.Object);

            // Assert
            mockExpenseCategoryQueries.Verify(x => x.AddExpenseCategory(It.IsAny<ExpenseCategory>()), Times.Once);
        }

        [Fact]
        public void AddExpense_CallsAddExpenseMethod()
        {
            // Arrange
            int expenseCategoryId = 1;
            string expenseName = "Dinner";
            int amount = 50;

            var mockExpenseQueries = new Mock<IExpenseQueries>();

            // Act
            MainPageLogic.AddExpense(expenseCategoryId, expenseName, amount, mockExpenseQueries.Object);
            // Assert
            mockExpenseQueries.Verify(x => x.AddExpense(It.IsAny<Expense>()), Times.Once);
        }
    }
}
