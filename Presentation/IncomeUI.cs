using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB_NEW
{
    public class IncomeUI
    {
        public string Title { get; set; }
        public double ProjectedIncome { get; set; }

        public IncomeUI(string title, double projectedIncome)
        {
            Title = title;
            ProjectedIncome = projectedIncome;
        }
    }

}

