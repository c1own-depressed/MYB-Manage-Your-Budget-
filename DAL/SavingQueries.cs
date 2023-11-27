namespace DAL
{
    using MYB.DAL;

    public static class SavingQueries
    {
        private static readonly AppDBContext _context;

        static SavingQueries()
        {
            _context = new AppDBContext();
        }

        public static List<Saving> GetSavingsByUserId(int userID)
        {
            return (from saving in _context.Savings
                    where saving.UserId == userID
                    select saving).ToList();
        }

        public static void AddSaving(Saving saving)
        {
            _context.Savings.Add(saving);
            _context.SaveChanges();
        }

        public static void DeleteSaving(int savingID)
        {
            var savingToDelete = _context.Savings.SingleOrDefault(e => e.Id == savingID);

            if (savingToDelete != null)
            {
                _context.Savings.Remove(savingToDelete);
                _context.SaveChanges();
            }
        }

        public static void EditSaving(int savingId, string savingName, int amount)
        {
            var dbSaving = _context.Savings.Find(savingId);

            if (dbSaving != null)
            {
                dbSaving.SavingName = savingName;
                dbSaving.Amount = amount;

                _context.SaveChanges();
            }
        }
    }
}
