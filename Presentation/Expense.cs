namespace MYB_NEW
{
    using System.Windows.Controls;

    public class Expense
    {
        public string Title { get; set; }

        public double Amount { get; set; }

        public StackPanel Category { get; set; }

        public Expense(string title, double amount, StackPanel category)
        {
            this.Title = title;
            this.Amount = amount;
            this.Category = category;
        }
    }

}
