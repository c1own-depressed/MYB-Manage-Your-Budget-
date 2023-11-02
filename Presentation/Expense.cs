using System;
using System.Windows.Controls;

namespace MYB_NEW
{
    public class Expense
    {
        public string Title { get; set; }
        public double Amount { get; set; }
        public StackPanel Category { get; set; }

        public Expense(string title, double amount, StackPanel category)
        {
            Title = title;
            Amount = amount;
            Category = category;
        }
    }

}
