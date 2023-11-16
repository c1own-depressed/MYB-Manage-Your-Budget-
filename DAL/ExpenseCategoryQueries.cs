namespace DAL
{
    using MYB.DAL;

    public static class ExpenseCategoryQueries
    {

        private static readonly AppDBContext context;

        static ExpenseCategoryQueries()
        {
            context = new AppDBContext();
        }

        public static ExpenseCategory GetExpenseCategoryById(int expenseCategoryId)
        {
            return (from expenseCat in context.ExpenseCategories
                    where expenseCat.Id == expenseCategoryId
                    select expenseCat).First();
        }

        public static List<ExpenseCategory> GetExpenseCategoriesByUserId(int userID)
        {
            return (from expenseCat in context.ExpenseCategories
                    where expenseCat.UserId == userID
                    select expenseCat).ToList();
        }

        public static void AddExpenseCategory(ExpenseCategory expenseCategory)
        {
            context.ExpenseCategories.Add(expenseCategory);
            context.SaveChanges();
        }
    }
}
