using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
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

        public User()
        {

        }
        public User(int id, string username, string email, string hashedPassword, bool lightTheme, string language, string currency)
        {
            Id = id;
            Username = username;
            Email = email;
            HashedPassword = hashedPassword;
            LightTheme = lightTheme;
            Language = language;
            Currency = currency;
        }
    }

    //enum currencyEnum
    //{
    //    uah,
    //    usd
    //}

    //enum languageEnum
    //{
    //    ua,
    //    en
    //}

    public class Income
    {
        public int Id { get; set; }
        public string IncomeName { get; set; }
        public int Amount { get; set; }
        public int UserId { get; set; } // TODO: foreign key
    }

    public class Saving
    {
        public int Id { get; set; }
        public string SavingName { get; set; }
        public int Amount { get; set; }
        public int UserId { get; set; } // TODO: foreign key
    }

    public class ExpenseCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int UserId { get; set; } // TODO: foreign key
    }

    public class Expense
    {
        public int Id { get; set; }
        public string ExpenseName { get; set; }
        public int Amount { get; set; }
        public int ExpenseCategoryId { get; set; } // TODO: foreign key
    }
    public class Transaction
    {
        public int Id { get; set; }
        public string TransactionName { get; set; }
        public int Amount { get; set; }
        public DateTime date { get; set; }
        public int ExpenseId { get; set; } // TODO: foreign key

    }

    public class Models
    {

    }
}