namespace MYB_NEW
{
    public class SavingsUI
    {
        public string Title { get; set; }

        public double Amount { get; set; }

        public SavingsUI(string title, double amount)
        {
            this.Title = title;
            this.Amount = amount;
        }
    }
}
