using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB_NEW
{
    public class SavingsUI
    {
        public string Title { get; set; }
        public double Amount { get; set; }

        public SavingsUI(string title, double amount)
        {
            Title = title;
            Amount = amount;
        }
    }
}
