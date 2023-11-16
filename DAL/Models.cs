namespace DAL
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string HashedPassword { get; set; }

        public bool LightTheme { get; set; }

        public string Language { get; set; }

        public string Currency { get; set; }

        public User(string username, string email, string hashedPassword, bool lightTheme, string language, string currency)
        {
            this.Username = username;
            this.Email = email;
            this.HashedPassword = hashedPassword;
            this.LightTheme = lightTheme;
            this.Language = language;
            this.Currency = currency;
        }
    }

    public class Income
    {
        public int Id { get; set; }

        public string IncomeName { get; set; } = string.Empty;

        public int Amount { get; set; }

        public int UserId { get; set; }
    }

    public class Saving
    {
        public int Id { get; set; }

        public string SavingName { get; set; } = string.Empty;

        public int Amount { get; set; }

        public int UserId { get; set; }
    }

    public class ExpenseCategory
    {
        public int Id { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public int UserId { get; set; }
    }

    public class Expense
    {
        public int Id { get; set; }

        public string ExpenseName { get; set; } = string.Empty;

        public int Amount { get; set; }

        public int ExpenseCategoryId { get; set; }
    }

    public class Transaction
    {
        public int Id { get; set; }

        public string TransactionName { get; set; } = string.Empty;

        public int Amount { get; set; }

        public DateTime Date { get; set; }

        public int ExpenseId { get; set; }
    }

    public class Models
    {
    }
}