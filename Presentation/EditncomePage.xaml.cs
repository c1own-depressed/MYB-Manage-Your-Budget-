namespace MYB_NEW
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Documents;
    using BLL;
    using DAL;

    /// <summary>
    /// Interaction logic for EditncomePage.xaml
    /// </summary>
    public partial class EditncomePage : Window
    {
        int incomeId;
        List<Income> incomeList;

        public EditncomePage(object main, int incomeId)
        {
            if (UserManager.CurrentUser.Language == "ua")
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("uk-UA");
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            }
            int userId = UserManager.CurrentUser.Id;
            this.InitializeComponent();
            this.incomeId = incomeId;
            this.incomeList = MainPageLogic.GetIncomesByUserId(userId);
            Income? temp = this.incomeList.FirstOrDefault(income => income.Id == this.incomeId);
            this.IncomeTextBox.Text = temp.IncomeName;
            this.ProjectedIncomeTextBox.Text = temp.Amount.ToString();
        }
    }
}
