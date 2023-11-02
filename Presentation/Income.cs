using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB_NEW
{
    public class Income
    {
        public string Title { get; set; }
        public double ProjectedIncome { get; set; }

        public Income(string title, double projectedIncome)
        {
            Title = title;
            ProjectedIncome = projectedIncome;
        }
    }

}

