using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
            //IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //.Build();
            //string connectionString = configuration.GetConnectionString("DefaultConnection"); // ConfigurationBuilder is not found

            var connectionString = "server=localhost;database=mybdb;user=root;password=uTnw0PIh65_!;";


            //var connectionString = "server=localhost;user=root;password=1234;database=mybdb";
            //var connectionString = "server=localhost;user=root;password=sireza42;database=mybdb2";

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 32));

            optionsBuilder.UseMySql(connectionString, serverVersion);
        }
    }
}