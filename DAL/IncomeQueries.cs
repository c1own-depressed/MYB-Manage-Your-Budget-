namespace DAL
{
    using System.Collections.Generic;
    using System.Linq;
    using MYB.DAL;

    public static class IncomeQueries
    {
        private static readonly AppDBContext _context;

        static IncomeQueries()
        {
            _context = new AppDBContext();
        }

        public static List<Income> GetIncomeByUserId(int userID)
        {
            return (from income in _context.Incomes
                    where income.UserId == userID
                    select income).ToList();
        }

        public static void AddIncome(Income income)
        {
            _context.Incomes.Add(income);
            _context.SaveChanges();
        }

        public static void DeleteIncome(int incomeID)
        {
            var incomeToDelete = _context.Incomes.SingleOrDefault(e => e.Id == incomeID);

            if (incomeToDelete != null)
            {
                _context.Incomes.Remove(incomeToDelete);
                _context.SaveChanges();
            }
        }

        public static void EditIncome(int incomeId, string incomeName, int amount)
        {
            var dbIncome = _context.Incomes.Find(incomeId);

            if (dbIncome != null)
            {
                dbIncome.IncomeName = incomeName;
                dbIncome.Amount = amount;

                _context.SaveChanges();
            }
        }
    }
}
