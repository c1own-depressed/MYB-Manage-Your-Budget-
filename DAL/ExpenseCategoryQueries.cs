namespace DAL
{
    using MYB.DAL;

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

        public static void DeleteExpenseCategory(int expenseCategoryId)
        {
            var expenseCategoryToDelete = _context.ExpenseCategories.SingleOrDefault(eC => eC.Id == expenseCategoryId);

            if (expenseCategoryToDelete != null)
            {
                _context.ExpenseCategories.Remove(expenseCategoryToDelete);
                _context.SaveChanges();
            }
        }

        public static void EditExpenseCategory(int expenseCategoryId, string expenseCategoryName)
        {
            var dbExpenseCategory = _context.ExpenseCategories.Find(expenseCategoryId);

            if (dbExpenseCategory != null)
            {
                dbExpenseCategory.CategoryName = expenseCategoryName;

                _context.SaveChanges();
            }
        }
    }
}
