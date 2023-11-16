namespace MYB_NEW
{
    public class IncomeUI
    {
        public string Title { get; set; }

        public double ProjectedIncome { get; set; }

        public IncomeUI(string title, double projectedIncome)
        {
            this.Title = title;
            this.ProjectedIncome = projectedIncome;
        }
    }

}

