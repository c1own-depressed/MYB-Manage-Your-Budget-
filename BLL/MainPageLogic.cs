namespace BLL
{
    using DAL;

    public class MainPageLogic
    {
        public static List<Income> GetIncomesByUserId(int userId)
        {
            List<Income> incomes = DAL.IncomeQueries.GetIncomeByUserId(userId);
            return incomes;
        }

        public static void AddIncome(int userId, string incomeName, int amount)
        {
            Income income = new Income()
            {
                UserId = userId,
                IncomeName = incomeName,
                Amount = amount,
            };
            DAL.IncomeQueries.AddIncome(income);
        }

        public static List<Saving> GetSavingsByUserId(int userId)
        {
            List<Saving> savings = DAL.SavingQueries.GetSavingsByUserId(userId);
            return savings;
        }

        public static void AddSavings(int userId, string savingName, int amount)
        {
            Saving saving = new Saving()
            {
                UserId = userId,
                SavingName = savingName,
                Amount = amount,
            };
            DAL.SavingQueries.AddSaving(saving);
        }

        public static List<ExpenseCategoryWithExpenses> GetCategoriesAndExpensesByUserId(int userId)
        {
            List<ExpenseCategory> expenseCategoryies = DAL.ExpenseCategoryQueries.GetExpenseCategoriesByUserId(userId);

            List<ExpenseCategoryWithExpenses> categoriesWithExpenses = new List<ExpenseCategoryWithExpenses>();

            foreach (var category in expenseCategoryies)
            {
                List<Expense> expenses = DAL.ExpenseQueries.GetExpensesByExpenseCategoryId(category.Id);
                categoriesWithExpenses.Add(new ExpenseCategoryWithExpenses
                {
                    ExpenseCategory = category,
                    Expenses = expenses,
                });
            }

            return categoriesWithExpenses;
        }

        public static void AddExpenseCategory(int userId, string expenseCategoryName)
        {
            ExpenseCategory expenseCategory = new ExpenseCategory()
            {
                UserId = userId,
                CategoryName = expenseCategoryName,
            };
            DAL.ExpenseCategoryQueries.AddExpenseCategory(expenseCategory);
        }

        public static void AddExpense(int expenseCategoryId, string expenseName, int amount)
        {
            Expense expense = new Expense()
            {
                ExpenseCategoryId = expenseCategoryId,
                ExpenseName = expenseName,
                Amount = amount,
            };
            DAL.ExpenseQueries.AddExpense(expense);
        }
    }
}
