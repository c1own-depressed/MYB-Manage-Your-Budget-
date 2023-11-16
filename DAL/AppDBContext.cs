using DAL;
using Microsoft.EntityFrameworkCore;
namespace MYB.DAL
{
    public class AppDBContext : DbContext
    {
        public AppDBContext()
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Income> Incomes { get; set; }

        public DbSet<Saving> Savings { get; set; }

        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server=localhost;user=root;password=uTnw0PIh65_!;database=mybdb2";

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 32));

            optionsBuilder.UseMySql(connectionString, serverVersion);
        }
    }
}